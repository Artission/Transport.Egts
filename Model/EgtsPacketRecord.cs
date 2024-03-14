using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model
{
	public class EgtsPacketRecord
	{
		/// <summary>
		/// набор полей записи, данные в сабрекордах
		/// </summary>
		/// <value>
		/// The record header.
		/// </value>
		public EgtsRecordHeader RecordHeader { get; set; }

		/// <summary>
		/// данные в записи идут в сабрекордах
		/// </summary>
		/// <value>
		/// The sub records.
		/// </value>
		public List<EgtsPacketSubRecord> SubRecords
		{
			get { return _subRecords; }
		}
		private readonly List<EgtsPacketSubRecord> _subRecords = new List<EgtsPacketSubRecord>();

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsPacketRecord"/> class.
		/// </summary>
		public EgtsPacketRecord()
		{
			RecordHeader = new EgtsRecordHeader();
			// заполненные поля скорее всего не будут меняться
			RecordHeader.SSOD = false;//отправляет сервер
			RecordHeader.RSOD = true;// отправлем на терминал
			RecordHeader.GRP = false;
			RecordHeader.RPP = EgtsProcessingPriority.Highest;
			RecordHeader.TMFE = false;
			RecordHeader.EVFE = false;
		}
	}
}
