using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// данные акселерометра
	/// egts_ECALL_ACCEL_DATA_t
	/// </summary>
	public class EgtsAccelerometerData : ISubrecordData
	{

		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.AccelData; } }

		/// <summary>
		/// Structures Amount
		/// </summary>
		/// <value>
		/// The sa.
		/// </value>
		public byte SA { get; set; }

		/// <summary>
		/// AbsoluteTime
		/// </summary>
		/// <value>
		/// The atm.
		/// </value>
		public uint ATM { get; set; }

		/// <summary>
		/// данные акселерометра
		/// ADS
		/// </summary>
		/// <value>
		/// The accelerometer records.
		/// </value>
		public List<EgtsAccelerometerItem> AccelerometerRecords
		{
			get { return _accRecs; }
		}
		private readonly List<EgtsAccelerometerItem> _accRecs = new List<EgtsAccelerometerItem>();
		
	}
}
