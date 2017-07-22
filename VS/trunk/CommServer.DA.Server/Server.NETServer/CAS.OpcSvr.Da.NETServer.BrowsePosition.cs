
//<summary>
//  Title   : BrowsePosition
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
// TITLE: Server.cs
//
// CONTENTS:
// 
// An in-process wrapper for a remote OPC XML-DA server (not thread safe).
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

using Opc.Da;
using System;

namespace CAS.CommServer.DA.Server.NETServer
{
  /// <summary>
  /// Implements an object that handles multi-step browse operations.
  /// </summary>
  [Serializable]
    internal class BrowsePosition : Opc.Da.BrowsePosition
	{
		/// <summary>
		/// The index of the next element to be returned.
		/// </summary>
		public int Index
		{
			get { return m_index;  }
			set { m_index = value; }
		}
		/// <summary>
		/// Initializes the base object with the specified parameters.
		/// </summary>
		public BrowsePosition(Opc.ItemIdentifier itemID, BrowseFilters filters)	: base(itemID, filters)
		{
		}

		#region Private Members
		private int m_index = 0;
		#endregion
	}
}
