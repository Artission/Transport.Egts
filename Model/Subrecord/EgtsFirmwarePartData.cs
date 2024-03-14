using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// частичная инфа по ПО?
	/// egts_FIRMWARE_PART_DATA_t
	/// </summary>
	public class EgtsFirmwarePartData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.FirmwarePartData; } }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public ushort ID { get; set; }

		/// <summary>
		/// Part Number
		/// </summary>
		/// <value>
		/// The pn.
		/// </value>
		public ushort PN { get; set; }

		/// <summary>
		/// Expected Parts Quantity
		/// </summary>
		/// <value>
		/// The epq.
		/// </value>
		public ushort EPQ { get; set; }
		
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
