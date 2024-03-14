

namespace Transport.EGTS.Model
{
	internal static class Constants
	{
		/// <summary>
		/// максимальная длина данных в пакете
		/// EGTS_MAX_SFDR_LEN в документации
		/// </summary>
		public const uint MaxDataLength = 65515U;

		/// <summary>
		/// максимальный размер буфера оригинальных данных
		/// EGTS_BACK_BUF_SIZE_1
		/// </summary>
		public const uint MaxBufferSize = 65536U;

		/// <summary>
		/// The maximum signature length
		/// EGTS_MAX_SIGN_LEN
		/// </summary>
		public const uint MaxSignatureLength = 512U;

		/// <summary>
		/// The maximum packet subrecords count
		/// EGTS_MAX_PACKET_SUBRECORDS
		/// </summary>
		public const uint MaxPacketSubrecordsCount = 64U;

		/// <summary>
		/// The maximum MDS additional data length
		/// EGTS_MDS_AD_LEN
		/// </summary>
		public const uint MaxMDSAdditionalDataLength = 56U;

		/// <summary>
		/// The maximum raw MDS length
		/// 
		/// </summary>
		public const uint MaxRawMDSLength = 1024U;

		/// <summary>
		/// The maximum firmware file name length
		/// EGTS_FN_LEN
		/// </summary>
		public const uint MaxFirmwareFileNameLength = 64U;

		/// <summary>
		/// The maximum user name length
		/// EGTS_UNM_LEN
		/// </summary>
		public const uint MaxUserNameLength = 32U;

		/// <summary>
		/// The maximum password length
		/// EGTS_UPSW_LEN
		/// </summary>
		public const uint MaxPasswordLength = 32U;

		/// <summary>
		/// The maximum server sequence length
		/// EGTS_SS_LEN
		/// </summary>
		public const uint MaxServerSequenceLength = 255U;
		
		/// <summary>
		/// The maximum public key length
		/// EGTS_PBK_LEN
		/// </summary>
		public const uint MaxPublicKeyLength = 512U;

		/// <summary>
		/// The maximum expression length
		/// EGTS_EXP_LEN
		/// </summary>
		public const uint MaxExpressionLength = 255U;

		/// <summary>
		/// The maximum serial number length
		/// EGTS_SRN_LEN
		/// </summary>
		public const uint MaxSerialNumberLength = 32U;

		/// <summary>
		/// The maximum description length
		/// EGTS_DSCR_LEN
		/// </summary>
		public const uint MaxDescriptionLength = 32U;

		/// <summary>
		/// The imei length
		/// </summary>
		public const uint IMEILength = 15U;

		/// <summary>
		/// The imsi length
		/// </summary>
		public const uint IMSILength = 16U;

		/// <summary>
		/// The language code length
		/// </summary>
		public const uint LanguageCodeLength = 3U;

		/// <summary>
		/// The network identifier length
		/// </summary>
		public const uint NetworkIDLength = 3U;

		/// <summary>
		/// The msisdn length
		/// </summary>
		public const uint MSISDNLength = 15U;

		/// <summary>
		/// The maximum vin length
		/// </summary>
		public const uint VinLength = 17U;

		/// <summary>
		/// Prefix,EGTS_PRF
		/// как ни странно в документашке есть такой штук как префикс в хедере, который должен быть нулевой, 
		/// вероятно задел на будущее предполагает больше значений, но сейчас просто проверка на 0
		/// </summary>
		public const byte PRF = (0x00);

		#region header constants

		/// <summary>
		/// length up to encoding part begin
		/// EGTS_HEADER_LEN_FIXED
		/// </summary>
		public const ushort FixedHeaderLength = 5;
		/// <summary>
		///  FDL, PID, PT
		/// EGTS_HEADER_LEN_ENCODED
		/// </summary>
		public const ushort HeaderLengthEncoded = 5; 
		/// <summary>
		/// PRA, RCA, TTL
		/// EGTS_HEADER_LEN_ROUTE
		/// </summary>
		public const ushort HeaderRouteLength = 5; 
		/// <summary>
		/// header Encoded max length (в исходниках стоит тудушка)
		/// EGTS_HEADER_LEN_ENCODED_MAX_LEN
		/// </summary>
		public const ushort EncodedHeaderMaxLength = 32;
		/// <summary>
		/// EGTS_HEADER_LEN_NO_ROUTE
		/// </summary>
		public const ushort HeaderLengthWithoutRoute = (FixedHeaderLength + HeaderLengthEncoded + 1) /* 12 byte так написано в сырцах! я хз почему 12 у них написано, по идее 11*/;
		/// <summary>
		/// EGTS_HEADER_LEN_WITH_ROUTE
		/// </summary>
		public const ushort HeaderLengthWithRoute = (HeaderLengthWithoutRoute + HeaderRouteLength)  /* 17 byte, та же фигня, по идее 16 */;
		/// <summary>
		/// EGTS_HEADER_LEN_MAX
		/// </summary>
		public const ushort MaxHeaderLength = (FixedHeaderLength + HeaderRouteLength + EncodedHeaderMaxLength + 1);

		#endregion header constants
	}
}
