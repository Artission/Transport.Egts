
using Transport.EGTS.Enum;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Model
{
	/// <summary>.
	/// сабрекорды
	/// egts_service_data_subrecord_t
	/// </summary>
	public class EgtsPacketSubRecord
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		public EgtsSubrecordType SRT { get; set; }

		/// <summary>
		/// Subrecord Length
		/// </summary>
		public ushort SRL { get; set; }

		/// <summary>
		/// Данные пока хз чё именно будет
		/// </summary>
		/// <value>
		/// The SRD.
		/// </value>
		public ISubrecordData SRD { get; set; }
	}
}
