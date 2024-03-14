using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// ответ на другие рекорды
	/// egts_RESPONSE_t
	/// </summary>
	public class EgtsResponse : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.Response; } }

		/// <summary>
		/// номер подтверждаемой записи (значение поля RN из обрабатываемой записи);
		/// </summary>
		/// <value>
		/// The CRN.
		/// </value>
		public ushort CRN { get; set; }

		/// <summary>
		/// статус обработки записи. (хз что именно, может и енум)
		/// </summary>
		/// <value>
		/// The RST.
		/// </value>
		public EgtsProcessingState RST { get; set; }
	}
}
