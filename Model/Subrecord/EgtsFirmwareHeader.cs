
namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// заголовок аппаратной инфы
	/// egts_FIRMWARE_DATA_header_t
	/// </summary>
	public class EgtsFirmwareHeader
	{
		#region bitfields

		/// <summary>
		/// 2  Object Type
		/// </summary>
		public byte OT  { get; set; }

		/// <summary>
		/// 2  Module Type
		/// </summary>
		public byte MT  { get; set; }

		#endregion bitfields

		/// <summary>
		/// Component or Module Identifier
		/// </summary>
		public byte CMI { get; set; }

		/// <summary>
		/// Version
		/// </summary>
		public ushort VER { get; set; }

		/// <summary>
		/// Whole Object Signature , СRC16-CCITT of whole data
		/// </summary>
		public ushort WOS { get; set; }

		/// <summary>
		/// File Name , optional
		/// </summary>
		public string  FN { get; set; }

	}
}
