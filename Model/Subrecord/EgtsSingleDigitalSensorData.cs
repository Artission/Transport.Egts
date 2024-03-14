
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// EGTS_SR_ABS_DIG_SENS_DATA
	/// </summary>
	public class EgtsSingleDigitalSensorData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.SingleDigitalSensorData; }
		}
		/// <summary>
		/// номер дискретного входа;
		/// </summary>
		/// <value>
		/// The DSN.
		/// </value>
		public ushort DSN { get; set; }

		/// <summary>
		/// состояние дискретного входа
		/// </summary>
		/// <value>
		///   <c>true</c> if DSST; otherwise, <c>false</c>.
		/// </value>
		public bool DSST { get; set; }
	}
}
