
namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// запись данных акселерометра
	/// egts_ACCEL_t
	/// </summary>
	public class EgtsAccelerometerItem
	{
		/// <summary>
		/// Relative Time
		/// </summary>
		/// <value>
		/// The RTM.
		/// </value>
		public ushort RTM { get; set; }

		/// <summary>
		/// X Axis Acceleration Value
		/// </summary>
		/// <value>
		/// The xaav.
		/// </value>
		public short XAAV { get; set; }

		/// <summary>
		/// Y Axis Acceleration Value
		/// </summary>
		/// <value>
		/// The yaav.
		/// </value>
		public short YAAV { get; set; }

		/// <summary>
		/// Z Axis Acceleration Value
		/// </summary>
		/// <value>
		/// The zaav.
		/// </value>
		public short ZAAV { get; set; }
	}
}
