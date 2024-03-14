
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// состояние терминала
	/// egts_TELEDATA_STATE_DATA_t
	/// </summary>
	public class EgtsTerminalStateData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.TerminalState; }
		}
		/// <summary>
		///  State 
		/// </summary>
		public EgtsTerminalState ST { get; set; }

		/// <summary>
		///  Main Power Source Voltage, in 0.1V 
		/// </summary>
		public byte MPSV { get; set; }

		/// <summary>
		///  Back Up Battery Voltage, in 0.1V 
		/// </summary>
		public byte BBV { get; set; }

		/// <summary>
		///  Internal Battery Voltage, in 0.1V 
		/// </summary>
		public byte IBV { get; set; }


		/// <summary>
		/// :1 Navigation Module State 
		/// </summary>
		public bool NMS { get; set; }

		/// <summary>
		/// :1 Internal Battery Used 
		/// </summary>
		public bool IBU { get; set; }

		/// <summary>
		/// :1 Back Up Battery Used 
		/// </summary>
		public bool BBU { get; set; }
	}
}
