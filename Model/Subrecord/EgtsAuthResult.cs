
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// результат запроса авторизации
	/// chandler_AUTH_RESULT_CODE
	/// </summary>
	public class EgtsAuthResult : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.ResultCode; }
		}
		/// <summary>
		/// ResultCode
		/// TODO приложение, из которого нужно брать результат обработки, отстусвует в имеющейся документации, оставляю байт, как узнаем что-нить, так впишу
		/// </summary>
		/// <value>
		///   <c>true</c> if RCD; otherwise, <c>false</c>.
		/// </value>
		public byte RCD { get; set; }
	}
}
