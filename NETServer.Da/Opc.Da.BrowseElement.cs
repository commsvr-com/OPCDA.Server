//<summary>
//  Title   : BrowseElement
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
//============================================================================
// TITLE: BrowseElement.cs
//
// CONTENTS:
// 
// A class which represents a node in the server address space.
//
// (c) Copyright 2003 The OPC Foundation
// ALL RIGHTS RESERVED.
//
// DISCLAIMER:
//  This code is provided by the OPC Foundation solely to assist in 
//  understanding and use of the appropriate OPC Specification(s) and may be 
//  used as set forth in the License Grant section of the OPC Specification.
//  This code is provided as-is and without warranty or support of any sort
//  and is subject to the Warranty and Liability Disclaimers which appear
//  in the printed OPC Specification.
//
// MODIFICATION LOG:
//
// Date       By    Notes
// ---------- ---   -----
// 2003/03/26 RSA   Initial implementation.

using System;
using System.Xml;
using System.Net;
using System.Threading;
using System.Collections;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Opc;
using Opc.Da;

namespace CAS.OpcSvr.Da.NETServer
{	
	/// <summary>
	/// A class which represents a node in the server address space.
	/// </summary>
	internal class BrowseElement
	{
		#region Public Members
		/// <summary>
		/// Initializes the element with its parent.
		/// </summary>
		public BrowseElement(BrowseElement parent, string name)
		{
			m_parent = parent;
			m_name   = name;
		}

		/// <summary>
		/// Initializes the element with its parent and item id.
		/// </summary>
		public BrowseElement(BrowseElement parent, string name, string itemID)
		{
			m_parent = parent;
			m_name   = name;
			m_itemID = itemID;
		}

		/// <summary>
		/// The name of the browse element.
		/// </summary>
		public string Name
		{
			get { return m_name; }
		}

		/// <summary>
		/// The fully qualified item id for the element.
		/// </summary>
		public string ItemID
		{
			get 
			{  
				StringBuilder itemID = new StringBuilder(256);
				BuildItemID(itemID);
				return itemID.ToString();
			}
		}

		/// <summary>
		/// The fully qualified browse path for the element.
		/// </summary>
		public string BrowsePath
		{
			get 
			{  
				StringBuilder browsePath = new StringBuilder(256);
				BuildBrowsePath(browsePath);
				return browsePath.ToString();
			}
		}

		/// <summary>
		/// The parent browse element.
		/// </summary>
		public BrowseElement Parent
		{
			get { return m_parent; }
		}

		/// <summary>
		/// The separator between names in the browse path.
		/// </summary>
		public string Separator
		{
			get { return "/"; }
		}

		/// <summary>
		/// Returns the child element at the specified index.
		/// </summary>
		public BrowseElement Child(int index)
		{
			if (m_children != null && index >= 0 && index < m_children.Count)
			{
				return (BrowseElement)m_children[index];
			}

			return null;
		}

		/// <summary>
		/// Returns the number of child elements.
		/// </summary>
		public int Count
		{
			get { return (m_children != null)?m_children.Count:0; }
		}

		/// <summary>
		/// Adds the names of the element children to an array.
		/// </summary>
		public void Browse(string browsePath, bool flat, ArrayList children)
		{
			if (m_children == null || m_children.Count == 0)
			{
				return;
			}

			foreach (BrowseElement child in m_children)
			{
				if (!flat)
				{
					children.Add(child.Name);
				}
				else
				{
					children.Add(browsePath + child.Name);
					child.Browse(browsePath + child.Name, flat, children);
				}
			}
		}

		/// <summary>
		/// Finds the element in the hierarchy referenced by the specified browse path.
		/// </summary>
		public BrowseElement Find(string browsePath)
		{
			string localPath = browsePath;
			
			// remove leading separator - if it exists.
			while (localPath.StartsWith(Separator))
			{
				localPath = localPath.Substring(Separator.Length);
			}

			// recursively search children.
			if (m_children != null)
			{
				foreach (BrowseElement child in m_children)
				{
					// check for a child with an exact name match.
					if (localPath == child.Name)
					{
						return child;
					}

					// check if the path starts with the child name.
					string prefix = child.Name + Separator;

					// check for a child with an exact name match plus trailing separator.
					if (localPath == prefix)
					{
						return child;
					}

					// search the child node if there is a match to the child name.
					if (localPath.StartsWith(prefix))
					{
						return child.Find(localPath.Substring(prefix.Length));
					}  
				}
			}

			// path not found.
			return null;
		}

		/// <summary>
		/// Inserts a child into the correct place in the hierarchy.
		/// </summary>
		public BrowseElement Insert(string browsePath)
		{
			if (browsePath == null) throw new ArgumentNullException("browsePath");

			// extract the first path element from the browse path.
			string name    = browsePath;
			string subpath = "";

			do
			{
				int index = name.IndexOf(Separator);

				if (index == -1)
				{
					break;
				}

				subpath = name.Substring(index + Separator.Length);
				name    = name.Substring(0, index);

				// name may be empty if multiple separators defined.
				if (name.Length > 0)
				{
					break;
				}

				name = subpath;
			}
			while (subpath.Length > 0);

			// check for valid path.
			if (name.Length == 0)
			{
				return null;
			}

			// find out if child element already exists.
			if (m_children != null)
			{
				foreach (BrowseElement child in m_children)
				{
					if (child.Name == name)
					{
						// insert new element in child.
						if (subpath.Length > 0)
						{
							return child.Insert(subpath);
						}

						// return existing child.
						return child;
					}
				}
			}

			// create a new child element.
			BrowseElement element = new BrowseElement(this, name);

			// add new child to end of list.
			if (m_children == null)
			{
				m_children = new ArrayList();
			}

			// add any children to the new element.
			if (subpath.Length == 0)
			{				
				m_children.Add(element);
				return element;
			}

			// add any sub elements.
			BrowseElement parent = element;

			element = parent.Insert(subpath);

			if (element != null)
			{
				m_children.Add(parent);
			}		

			return element;
		}

		/// <summary>
		/// Inserts a child with a specific item id into the correct place in the hierarchy. 
		/// </summary>
		public BrowseElement Insert(string browsePath, string itemID)
		{
			BrowseElement child = Insert(browsePath);

			if (child != null)
			{
				child.m_itemID = itemID;
			}

			return child;
		}

		/// <summary>
		/// Removes the element from the hierarchy.
		/// </summary>
		public void Remove()
		{
			if (m_parent != null)
			{
				m_parent.Remove(m_name);
			}
		}

		/// <summary>
		/// Removes the child with the specified name from the element. 
		/// </summary>
		public bool Remove(string name)
		{
			if (m_children != null)
			{
				for (int ii = 0; ii < m_children.Count; ii++)
				{
					BrowseElement child = (BrowseElement)m_children[ii];

					if (child.Name == name)
					{
						m_children.RemoveAt(ii);
						return true;
					}
				}
			}

			return false;
		}
		#endregion

		#region Private Members
		/// <summary>
		/// Recursively builds the fully qualified browse path.
		/// </summary>
		private void BuildBrowsePath(StringBuilder browsePath)
		{
			if (m_parent != null)
			{
				m_parent.BuildBrowsePath(browsePath);

				if (browsePath.Length > 0)
				{
					browsePath.Append(Separator);
				}
			}

			browsePath.Append(Name);
		}

		/// <summary>
		/// Recursively builds the fully qualified item id.
		/// </summary>
		private void BuildItemID(StringBuilder itemID)
		{
			if (m_itemID != null)
			{
				itemID.Append(itemID);
				return;
			}

			if (m_parent != null)
			{
				m_parent.BuildItemID(itemID);

				if (itemID.Length > 0)
				{
					itemID.Append(Separator);
				}
			}

			itemID.Append(Name);
		}

		BrowseElement m_parent = null;
		string m_itemID = null;
		string m_name = null;
		ArrayList m_children = null;
		#endregion
	}
}
