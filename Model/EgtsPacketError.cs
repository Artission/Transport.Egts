
namespace Transport.EGTS.Model
{
	/// <summary>
	/// ошибки возникающие при парсинге, не все они критичные и по мере парсинга пишутся к пакету
	/// </summary>
	public class EgtsPacketError
	{
		/// <summary>
		/// Gets or sets the packet id.
		/// </summary>
		/// <value>
		/// The pid.
		/// </value>
		public ushort PID { get; set; }

		/* по идее должен быть тип ошибки, или код, но пока в исходниках везде только Unknown
		public ErrorType ErrorType { get; set; }
		*/

		/// <summary>
		/// Gets or sets the error text.
		/// </summary>
		/// <value>
		/// The error text.
		/// </value>
		public string ErrorText { get; set; }
	}
}
