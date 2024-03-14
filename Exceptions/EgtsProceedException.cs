using System;
using System.Runtime.Serialization;
using Transport.EGTS.Enum;
using Transport.EGTS.Model;

namespace Transport.EGTS.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class EgtsProceedException:Exception
	{
		/// <summary>
		/// Gets or sets the type of the failed to proceed subrecord.
		/// </summary>
		/// <value>
		/// The type of the failed to proceed subrecord.
		/// </value>
		public EgtsSubrecordType FailedToProceedSubrecordType { get; set; }

		// TODO TEMPORARY
		public EgtsPacket Packet { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsRecieveException"/> class.
		/// </summary>
		public EgtsProceedException()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsRecieveException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public EgtsProceedException(string message, Exception innerException):base(message,innerException)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsRecieveException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public EgtsProceedException(string message)
			: base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsProceedException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="failedType">Type of the failed.</param>
		public EgtsProceedException(string message, EgtsSubrecordType failedType)
			: base(message)
		{
			FailedToProceedSubrecordType = failedType;
		}

		#region Overrides of Exception

		/// <summary>
		/// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param><exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}
}
