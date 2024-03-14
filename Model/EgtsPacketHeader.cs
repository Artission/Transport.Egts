
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model
{
	/// <summary>
	/// хедер содержит общию информацию из пакета, без данных
	/// egts_header_s
	/// </summary>
	public class EgtsPacketHeader
	{
		/// <summary>
		///  Protocol Version 
		/// </summary>
		public byte PRV { get; set; }

		/// <summary>
		///  Security Key ID 
		/// </summary>
		public byte SKID { get; set; }

		#region Bitfields

		/// <summary>
		/// Gets or sets the bitfields byte.
		/// сам байт полей ниже, необходим при расчёте CRC
		/// </summary>
		/// <value>
		/// The bitfields byte.
		/// </value>
		internal byte BitfieldsByte { get; set; }

		/// <summary>
		///  2  Prefix 
		/// как ни странно в документашке есть такой штук как префикс в хедере, который должен быть нулевой, 
		/// вероятно задел на будущее предполагает больше значений, но сейчас просто проверка на 0
		/// </summary>
		public byte PRF { get; set; }

		/// <summary>
		///  Is Route further
		/// </summary>
		public bool RTE { get; set; }

		/// <summary>
		///  2  Encryption Algorithm 
		/// ТОЖЕ должны быть варианты алгоритма шифрования, причём если оный есть, то использовать и проверить SKID нужно,
		///  но в оригинале есть только "дебаговая расшифровка", которая по сути ничего не делает
		/// </summary>
		public byte ENA { get; set; }

		/// <summary>
		///  Is Compressed 
		/// </summary>
		public bool CMP { get; set; }

		/// <summary>
		///  Priority 
		/// приоритет описан в документашке, поетому заведён енум
		/// </summary>
		public EgtsProcessingPriority PR { get; set; }

		#endregion Bitfields

		/// <summary>
		///  Header Length , up to HCS
		/// НЕ НУЖНО заполнять самому, заполнится из расчёта остальных полей и шифровки, автоматически при запарсивании пакета
		/// </summary>
		public byte HL { get; set; }

		/// <summary>
		///  Header Encoding 
		/// </summary>
		public byte HE { get; set; }

		#region encoded part ( 5 byte + 5(?) byte )

		/// <summary>
		///  Frame Data Length 
		/// НЕ НУЖНО заполнять в ручную, при запарсивании пакета, вычислится автоматически на основе данных, шифрования\сжатия
		/// </summary>
		public ushort FDL { get; set; }

		/// <summary>
		///  Packet Identifier 
		/// </summary>
		public ushort PID { get; set; }

		/// <summary>
		///  Packet Type 
		/// </summary>
		public EgtsPacketType PT { get; set; }

		#region optional part ( 5 byte )

		/// <summary>
		///  Peer Address , optional , exists when RTE switched on
		/// </summary>
		public ushort? PRA { get; set; }

		/// <summary>
		///  Recipient Address , optional , exists when RTE switched on 
		/// </summary>
		public ushort? RCA { get; set; }

		/// <summary>
		///  Time To Live , optional , exists when RTE switched on 
		/// </summary>
		public byte? TTL { get; set; }

		#endregion optional part ( 5 byte )

		#endregion encoded part ( 5 byte + 5(?) byte )

		/// <summary>
		///  Header Check Sum , mandatory , XOR
		/// НЕ НУЖНО заполнять, заполняется автоматически на основе хедера
		/// </summary>
		public byte HCS { get; set; }

	}
}
