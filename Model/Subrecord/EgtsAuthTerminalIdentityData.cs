
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// информация по терминалу
	/// egts_AUTH_TERM_IDENTITY_t
	/// </summary>
	public class EgtsAuthTerminalIdentityData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.TerminalIdentity; } }

		/// <summary>
		///  Terminal Identifier 
		/// </summary>
		public uint TID { get; set; }

		#region bitfields

		/// <summary>
		///  битовый флаг, определяющий наличие поля MSISDN в подзаписи (если бит равен 1, то поле передаётся, если 0, то не передаётся)
		/// </summary>
		public bool MNE { get; set; }

		/// <summary>
		///  битовый флаг, определяющий наличие поля BS в подзаписи (если бит равен 1, то поле передаётся, если 0, то не передаётся)
		/// </summary>
		public bool BSE { get; set; }

		/// <summary>
		///  битовый флаг определяет наличие поля NID в подзаписи (если бит равен 1, то поле передаётся, если 0, то не передаётся)
		/// </summary>
		public bool NIDE { get; set; }

		/// <summary>
		///  битовый флаг предназначен для определения алгоритма использования Сервисов 
		/// (если бит равен 1, то используется «простой» алгоритм, если 0, то алгоритм «запросов» на использование сервисов)
		/// </summary>
		public bool SSRA { get; set; }

		/// <summary>
		///  код языка, предпочтительного к использованию на стороне АС, по ISO 639-2, например, “rus” – русский
		/// </summary>
		public bool LNGCE { get; set; }

		/// <summary>
		///  битовый флаг, который определяет наличие поля IMSI в подзаписи (если бит равен 1, то поле передаётся, если 0, то не передаётся)
		/// </summary>
		public bool IMSIE { get; set; }

		/// <summary>
		///  битовый флаг, который определяет наличие поля IMEI в подзаписи (если бит равен 1, то поле передаётся, если 0, то не передаётся)
		/// </summary>
		public bool IMEIE { get; set; }

		/// <summary>
		/// битовый флаг, который определяет наличие поля HDID в подзаписи (если бит равен 1, то поле передаётся, если 0, то не передаётся)
		/// </summary>
		public bool HDIDE { get; set; }

		#endregion bitfields

		/// <summary>
		///  Home Dispatcher Identifier, optional,  switched on by HDIDE 
		/// </summary>
		public ushort HDID { get; set; }

		/// <summary>
		/// International Mobile Equipment Identity ,optional , switched on by IMEIE
		/// </summary>
		/// <value>
		/// The imei.
		/// </value>
		public string IMEI { get; set; } 

		/// <summary>
		/// International Mobile Subscriber Identity ,optional , switched on by IMSIE
		/// </summary>
		/// <value>
		/// The imsi.
		/// </value>
		public string IMSI{ get; set; } 

		/// <summary>
		/// Language Code ,optional , switched on by LNGCE
		/// </summary>
		/// <value>
		/// The LNGC.
		/// </value>
		public string LNGC{ get; set; }

		/// <summary>
		/// Network Identifier , optional , switched on by NIDE
		/// </summary>
		/// <value>
		/// The nid.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] NID{ get { return _nid; }}
		private readonly byte[] _nid = new byte[Constants.NetworkIDLength];

		/// <summary>
		///  Buffer Size , optional , switched on by BSE ??? 1024, 2048, 4096 ??? 
		/// </summary>
		public ushort BS { get; set; }

		/// <summary>
		/// Mobile Station Integrated Services Digital Network Number , optional , switched on by MNE
		/// </summary>
		/// <value>
		/// The msisdn.
		/// </value>
		public string MSISDN { get; set; } 

	}
}
