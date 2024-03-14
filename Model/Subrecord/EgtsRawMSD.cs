using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// минимальный набор данных в чистом виде(возможно дальше нужно RawData раскладывать в норм MSD, а может в строку приводить и любоваться, в сырцах нету(
	/// egts_ECALL_RAW_MSD_DATA_t
	/// </summary>
	public class EgtsRawMSD : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.RawMSDData; } }

		/// <summary>
		/// Gets or sets the format.
		/// </summary>
		/// <value>
		/// The fm.
		/// </value>
		public byte FM { get; set; }

		/// <summary>
		/// Gets the raw data.
		/// </summary>
		/// <value>
		/// The raw data.
		/// </value>
		public List<byte> RawData
		{
			get { return _rawData; }
		}
		private readonly List<byte> _rawData = new List<byte>();
	}
}
