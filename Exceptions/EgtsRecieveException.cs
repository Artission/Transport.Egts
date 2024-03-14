using System;

namespace Transport.EGTS.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class EgtsRecieveException:Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsRecieveException"/> class.
		/// </summary>
		public EgtsRecieveException()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsRecieveException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public EgtsRecieveException(string message, Exception innerException):base(message,innerException)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsRecieveException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public EgtsRecieveException(string message):base(message)
		{
			
		}
	}
}
