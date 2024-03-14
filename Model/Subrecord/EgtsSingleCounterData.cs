
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// EGTS_SR_ABS_CNTR_DATA
	/// </summary>
	public class EgtsSingleCounterData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.SingleCounterData; }
		}
		/// <summary>
		/// номер
		/// </summary>
		public byte CN { get; set; }

		/// <summary>
		/// значение
		/// </summary>
		public uint CNV { get; set; }
	}
}
