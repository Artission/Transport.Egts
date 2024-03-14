
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// Информация для аутентицикации
	/// egts_AUTH_AUTH_INFO_t
	/// </summary>
	public class EgtsAuthInfo : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.AuthInfo; } }
		/// <summary>
		/// User Name.
		/// </summary>
		/// <value>
		/// The unm.
		/// </value>
		public string UNM { get; set; }

		/// <summary>
		/// User Password
		/// </summary>
		/// <value>
		/// The upsw.
		/// </value>
		public string UPSW { get; set; }

		/// <summary>
		/// Server Sequence (optional)
		/// </summary>
		/// <value>
		/// The ss.
		/// </value>
		public string SS { get; set; }
	}
}
