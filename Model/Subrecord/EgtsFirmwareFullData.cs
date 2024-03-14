using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// информация о ПО?
	/// egts_FIRMWARE_FULL_DATA_t
	/// </summary>
	public class EgtsFirmwareFullData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.FirmwareFullData; } }

		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		/// <value>
		/// The header.
		/// </value>
		public EgtsFirmwareHeader Header { get; set; }

		/// <summary>
		/// Gets the object data.
		/// </summary>
		/// <value>
		/// The object data.
		/// </value>
		public List<byte> ObjectData
		{
			get { return _objData; }
		}
		private readonly List<byte> _objData = new List<byte>();
	}
}
