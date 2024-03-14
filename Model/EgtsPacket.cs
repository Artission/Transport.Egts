
using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model
{
	/// <summary>
	/// Модель пакета с данными распарсенного
	/// egts_packet_state_t
	/// </summary>
	public class EgtsPacket
	{
		// TODO TEMPORARY
		public List<byte> OriginalBytes
		{
			get { return _origBytes; }
		}
		private readonly List<byte> _origBytes = new List<byte>();

		public EgtsResponceHeader ResponceHeader { get; set; }

		/// <summary>
		/// Gets or sets the PRV skipped.
		/// </summary>
		/// <value>
		/// The PRV skipped.
		/// </value>
		public int PRVSkipped { get; set; }

		#region non packet data

		/// <summary>
		/// служебное состояние распарсивания пакета, не данные!
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public EgtsPacketState State { get; set; }

		#endregion non packet data

		#region header

		/// <summary>
		/// Gets the header bytes. 
		/// могут быть зашифрованы, см IsHeaderEncoded, причём в оригинальном коде нет зашифровки\расшифровки, так что не знаю как изобразить способ кодировки
		/// </summary>
		/// <value>
		/// The header bytes.
		/// </value>
		public List<byte> HeaderBytes
		{
			get { return _headerBytes; }
		}
		private readonly List<byte> _headerBytes = new List<byte>();

		/// <summary>
		/// Gets or sets a value indicating whether this instance is header encoded.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is header encoded; otherwise, <c>false</c>.
		/// </value>
		public bool IsHeaderEncoded { get; set; }

		/// <summary>
		/// хедер содержит общию информацию из пакета, без данных
		/// </summary>
		/// <value>
		/// The header.
		/// </value>
		public EgtsPacketHeader Header { get; set; }

		#endregion header

		#region signature

		/// <summary>
		/// Gets or sets a value indicating whether this instance is signed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is signed; otherwise, <c>false</c>.
		/// </value>
		public bool IsSigned { get; set; }

		/// <summary>
		/// Gets the signature bytes.
		/// </summary>
		/// <value>
		/// The signature bytes.
		/// </value>
		public List<byte> SignatureBytes
		{
			get { return _signatureBytes; }
		}
		private readonly List<byte> _signatureBytes = new List<byte>();

		#endregion signature

		#region Data

		/// <summary>
		/// Gets or sets the records.
		/// </summary>
		/// <value>
		/// The records.
		/// </value>
		public List<EgtsPacketRecord> Records
		{
			get { return _records; }
		}
		private readonly List<EgtsPacketRecord> _records = new List<EgtsPacketRecord>();

		/// <summary>
		/// Gets the data encoded bytes.
		/// </summary>
		/// <value>
		/// The data encoded bytes.
		/// </value>
		public List<byte> DataEncodedBytes
		{
			get { return _dataEncodedBytes; }
		}
		private readonly List<byte> _dataEncodedBytes = new List<byte>();


		/// <summary>
		/// Gets the data bytes.
		/// </summary>
		/// <value>
		/// The data bytes.
		/// </value>
		public List<byte> DataBytes
		{
			get { return _dataBytes; }
		}
		private readonly List<byte> _dataBytes = new List<byte>();

		/// <summary>
		/// Gets or sets a value indicating whether this instance is data encoded.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is data encoded; otherwise, <c>false</c>.
		/// </value>
		public bool IsDataEncoded { get; set; }

		#endregion Data

		#region CRCs

		/// <summary>
		/// Gets the data CRC bytes.
		/// </summary>
		/// <value>
		/// The data CRC bytes.
		/// </value>
		public List<byte> DataCRCBytes
		{
			get { return _dataCrcBytes; }
		}
		private readonly List<byte> _dataCrcBytes = new List<byte>();

		/// <summary>
		/// Gets or sets the data CRC.
		/// </summary>
		/// <value>
		/// The data CRC.
		/// </value>
		public int DataCRC { get; set; }


		#endregion CRCs

		/// <summary>
		/// Initializes a new instance of the <see cref="EgtsPacket"/> class.
		/// ОБЯЗАТЕЛЬНО следите за PID
		/// </summary>
		public EgtsPacket()
		{
			State = EgtsPacketState.Unknown;
			Header = new EgtsPacketHeader();

			// в большинстве случаев все заполненные поля останутся такими
			Header.PRV = EgtsPacketReciever.Version;
			Header.PT = EgtsPacketType.AppData;
			// и хотя кроме версии и типа пакета эти значения и так дефолтные они не закомменчены
			// закомменченные ниже это фактичски нерабочие в данный момент.
			Header.SKID = 0;
			Header.PRF = 0;
			Header.PR = EgtsProcessingPriority.Highest;
			
			/* 
			ret.Header.RTE = false;
			ret.Header.ENA = 0;
			ret.Header.CMP = false;
			ret.Header.PRA = null;
			ret.Header.RCA = null;
			ret.Header.TTL = null;
			ret.Header.HE = 0;*/
			
		}
	}
}
