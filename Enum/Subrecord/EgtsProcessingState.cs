
namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// возможные состояния в респонсе или состояние терминала (за исключением ОК и InProgress)
	/// </summary>
	public enum EgtsProcessingState
	{
		/// <summary>
		/// Закончено полностью, для EgtsResponse
		/// Статус - выключен для EgtsAuthTerminalModuleData
		/// EGTS_PC_OK
		/// </summary>
		OK = 0,

		/// <summary>
		/// В процессе, для EgtsResponse
		/// Статус - включен для EgtsAuthTerminalModuleData
		/// EGTS_PC_IN_PROGRESS
		/// </summary>
		InProgress = 1,

		/// <summary>
		///  unsupported protocol 
		/// EGTS_PC_UNS_PROTOCOL
		/// </summary>
		UnsupportedProtocol = 128,

		/// <summary>
		///  decription error 
		/// EGTS_PC_DECRYPT_ERROR
		/// </summary>
		Descriptionerror = 129,

		/// <summary>
		///  processing denied 
		/// EGTS_PC_PROC_DENIED
		/// </summary>
		ProcessingDenied = 130,

		/// <summary>
		///  incorrect header format 
		/// EGTS_PC_INC_HEADERFORM
		/// </summary>
		IncorrectHeaderFormat = 131,

		/// <summary>
		///  incorrect data format 
		/// EGTS_PC_INC_DATAFORM
		/// </summary>
		IncorrectDataFormat = 132,

		/// <summary>
		///  unsupported type 
		/// EGTS_PC_UNS_TYPE
		/// </summary>
		UnsupportedType = 133,

		/// <summary>
		///  incorrect parameters number 
		/// EGTS_PC_NOTEN_PARAMS
		/// </summary>
		IncorrectParametersNumber = 134,

		/// <summary>
		///  attempt to retry processing 
		/// EGTS_PC_DBL_PROC
		/// </summary>
		DuplicateProcessing = 135,

		/// <summary>
		///  source data processing denied 
		/// EGTS_PC_PROC_SRC_DENIED
		/// </summary>
		SourceProcessingDenied = 136,

		/// <summary>
		///  header crc error 
		/// EGTS_PC_HEADERCRC_ERROR
		/// </summary>
		HeaderCrcError = 137,

		/// <summary>
		///  data crc error 
		/// EGTS_PC_DATACRC_ERROR
		/// </summary>
		DataCrcError = 138,

		/// <summary>
		///  invalid data length 
		/// EGTS_PC_INVDATALEN
		/// </summary>
		InvalidDataLength = 139,

		/// <summary>
		/// Route Not Found
		/// EGTS_PC_ROUTE_NFOUND
		/// </summary>
		RouteNotFound = 140,

		/// <summary>
		/// Route Closed 
		/// EGTS_PC_ROUTE_CLOSED
		/// </summary>
		RouteClosed = 141,

		/// <summary>
		/// Routing Denied 
		/// EGTS_PC_ROUTE_DENIED
		/// </summary>
		RoutingDenied = 142,

		/// <summary>
		/// Invalid Address 
		/// EGTS_PC_INVADDR
		/// </summary>
		InvalidAddress = 143,

		/// <summary>
		/// Retranslation Data Amount Exceed
		/// EGTS_PC_TTLEXPIRED
		/// </summary>
		RetranslationDataAmountExceed = 144,

		/// <summary>
		/// No Acknowledge 
		/// EGTS_PC_NO_ACK
		/// </summary>
		NoAcknowledge = 145,

		/// <summary>
		/// Object Not Found
		/// EGTS_PC_OBJ_NFOUND
		/// </summary>
		ObjectNotFound = 146,

		/// <summary>
		/// Event Not Found
		/// EGTS_PC_EVNT_NFOUND
		/// </summary>
		EventNotFound = 147,

		/// <summary>
		/// Service Not Found
		/// EGTS_PC_SRVC_NFOUND
		/// </summary>
		ServiceNotFound = 148,

		/// <summary>
		/// Service Denied 
		/// EGTS_PC_SRVC_DENIED
		/// </summary>
		ServiceDenied = 149,

		/// <summary>
		/// Unknown Service Type
		/// EGTS_PC_SRVC_UNKN
		/// </summary>
		UnknownServiceType = 150,

		/// <summary>
		/// Authorization Denied 
		/// EGTS_PC_AUTH_DENIED
		/// </summary>
		AuthorizationDenied = 151,

		/// <summary>
		/// Object Already Exist
		/// EGTS_PC_ALREADY_EXISTS
		/// </summary>
		ObjectAlreadyExist = 152,

		/// <summary>
		/// Identificator Not Found
		/// EGTS_PC_ID_NFOUND
		/// </summary>
		IdentificatorNotFound = 153,

		/// <summary>
		/// Date Or Time Incorrect
		/// EGTS_PC_INC_DATETIME
		/// </summary>
		DateOrTimeIncorrect = 154,

		/// <summary>
		///  input / output error 
		/// EGTS_PC_IO_ERROR
		/// </summary>
		IOError = 155,

		/// <summary>
		/// Resources Not Available
		/// EGTS_PC_NO_RES_AVAIL
		/// </summary>
		ResourcesNotAvailable = 156,

		/// <summary>
		/// Module Internal Fault
		/// EGTS_PC_MODULE_FAULT
		/// </summary>
		ModuleInternalFault = 157,

		/// <summary>
		/// Module Power Fault
		/// EGTS_PC_MODULE_PWR_FLT
		/// </summary>
		ModulePowerFault = 158,

		/// <summary>
		/// Module Processor Fault
		/// EGTS_PC_MODULE_PROC_FLT
		/// </summary>
		ModuleProcessorFault = 159,

		/// <summary>
		/// Module Software Fault
		/// EGTS_PC_MODULE_SW_FLT
		/// </summary>
		ModuleSoftwareFault = 160,

		/// <summary>
		/// Module Firmware Fault
		/// EGTS_PC_MODULE_FW_FLT
		/// </summary>
		ModuleFirmwareFault = 161,

		/// <summary>
		///  module input/output fault 
		/// EGTS_PC_MODULE_IO_FLT
		/// </summary>
		ModuleIOFault = 162,

		/// <summary>
		/// Module Memory Fault
		/// EGTS_PC_MODULE_MEM_FLT
		/// </summary>
		ModuleMemoryFault = 163,

		/// <summary>
		/// Test Failed 
		/// EGTS_PC_TEST_FAILED
		/// </summary>
		TestFailed = 164,

		/// <summary>
		///  unknown error
		/// EGTS_PC_UNKNOWN
		/// </summary>
		Unknown = 255

	}
}
