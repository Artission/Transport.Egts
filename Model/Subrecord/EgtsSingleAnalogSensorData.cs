
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// EGTS_SR_ABS_DIG_SENS_DATA
	/// </summary>
	public class EgtsSingleAnalogSensorData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.SingleAnalogSensorData; }
		}
		/// <summary>
		/// номер аналогового входа;
		/// </summary>
		public byte ASN { get; set; }

		/// <summary>
		/// значение
		/// </summary>
		public uint ASV { get; set; }
	}
}
