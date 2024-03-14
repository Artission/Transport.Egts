
namespace Transport.EGTS.Model
{
	/// <summary>
	/// egts_responce_header_t
	/// </summary>
	public class EgtsResponceHeader
	{
		/// <summary>
		/// Response Packet ID
		/// </summary>
		/// <value>
		/// The rpid.
		/// </value>
		public ushort RPID { get; set; }

		/// <summary>
		/// Processing Result
		/// </summary>
		/// <value>
		/// The pr.
		/// </value>
		public byte PR { get; set; }
	}
}
