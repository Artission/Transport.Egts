
namespace Transport.EGTS.Enum.Subrecord
{
	public enum EgtsSymbolEncoding
	{
		/// <summary>
		/// CP-1251
		/// CHS_CP_1251
		/// </summary>
		CP1251 = 0,
		/// <summary>
		/// IA5 (CCITT T.50)/ASCII (ANSI X3.4)
		/// CHS_ANSI
		/// </summary>
		ANSI = 1,
		/// <summary>
		/// binary
		/// CHS_BIN
		/// </summary>
		BIN = 2,
		/// <summary>
		/// Latin 1 (ISO-8859-1)
		/// CHS_LATIN_1
		/// </summary>
		Latin1 = 3,
		/// <summary>
		/// binary , oops
		/// да, так в сырцах и написано "oops" видимо два типа для бинарных данных
		/// CHS_BIN_1
		/// </summary>
		BIN1 = 4,
		/// <summary>
		/// JIS (X 0208-1990)
		/// CHS_JIS
		/// </summary>
		JIS = 5,
		/// <summary>
		/// Cyrillic (ISO-8859-5)
		/// CHS_CYRILIC
		/// </summary>
		Cyrillic = 6,
		/// <summary>
		/// Latin/Hebrew (ISO-8859-8)
		/// CHS_HEBREW
		/// </summary>
		Hebrew = 7,
		/// <summary>
		/// UCS2 (ISO/IEC-10646)
		/// CHS_UCS2
		/// </summary>
		UCS2 = 8,
		/// <summary>
		/// unknown
		/// CHS_UNKNOWN
		/// </summary>
		Unknown  = 255
	}
}
