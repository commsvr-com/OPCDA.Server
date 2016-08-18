//<summary>
//  Title   : NetworkConfig.Exceptions
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    <Author> Tomek Siwecki - 26.12.2006 - Created <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace NetworkConfig.HMI.Exceptions
{
  /// <summary>
  /// Exception class for exceptions occurring when cannot create child object.
  /// </summary>
  [Serializable()]
  public class CreateChildObjectException: ApplicationException
  {
    #region Properties

    private string _parent;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new <see cref="CreateChildObjectException"/> object.  
    /// </summary>
    public CreateChildObjectException()
    {
    }

    /// <summary>
    /// Creates a new <see cref="CreateChildObjectException"/> object. 
    /// </summary>
    /// <param name="message">Messege</param>
    public CreateChildObjectException( string message )
      : base( message )
    {
    }

    /// <summary>
    /// Creates a new <see cref="CreateChildObjectException"/> object. 
    /// </summary>
    /// <param name="message">Massege</param>
    /// <param name="innerException">Inner Exception</param>
    public CreateChildObjectException( string message, Exception innerException )
      :
       base( message, innerException )
    {
    }
    /// <summary>
    /// Creates a new <see cref="CreateChildObjectException"/> object. 
    /// </summary>
    /// <param name="info">Serialization Informations</param>
    /// <param name="context">Constext</param>
    protected CreateChildObjectException( SerializationInfo info,
       StreamingContext context )
      : base( info, context )
    {
    }

    /// <summary>
    /// Creates a new <see cref="CreateChildObjectException"/> object.
    /// </summary>
    /// <param name="parent">Name of the parent object</param>
    /// <param name="msg">Messege</param>
    public CreateChildObjectException( string parent, string msg )
      : base( msg )
    {
      _parent = parent;
    }
    #endregion

    #region Fields

    /// <summary>
    /// Gets the parent object
    /// </summary>
    public string Parent
    {
      get
      {
        return _parent;
      }
    }

    #endregion
  }
}
