
namespace Transport.EGTS.Enum
{
	/// <summary>
	/// тип подзаписей
	/// </summary>
	public enum EgtsSubrecordType
	{
		#region Common types

		/// <summary>
		/// The responce
		/// EGTS_SR_RESPONSE
		/// </summary>
		Response = 0,

		/// <summary>
		/// COMMAND_DATA
		/// EGTS_SR_COMMAND_DATA
		/// </summary>
		CommandData = 51,

		#endregion Common types

		#region Auth types
		
		/// <summary>
		/// TERM_IDENTITY
		/// EGTS_SR_TERM_IDENTITY
		/// </summary>
		TerminalIdentity = 1,
		/// <summary>
		/// MODULE_DATA
		/// EGTS_SR_MODULE_DATA
		/// </summary>
		ModuleData = 2,
		/// <summary>
		/// VEHICLE_DATA
		/// EGTS_SR_VEHICLE_DATA
		/// </summary>
		VehicleData = 3,

		/// <summary>
		/// Авторизация от другого сервера (НОВЫЙ ПУНКТИК!)
		/// EGTS_SR_DISPATCHER_IDENTITY
		/// </summary>
		DispatcherIdentity =5,

		/// <summary>
		/// AUTH_PARAMS
		/// EGTS_SR_AUTH_PARAMS
		/// </summary>
		AuthParams = 6,
		/// <summary>
		/// AUTH_INFO
		/// EGTS_SR_AUTH_INFO
		/// </summary>
		AuthInfo = 7,
		/// <summary>
		/// SERVICE_INFO
		/// EGTS_SR_SERVICE_INFO
		/// </summary>
		ServiceInfo = 8,
		/// <summary>
		/// RESULT_CODE
		/// EGTS_SR_RESULT_CODE
		/// </summary>
		ResultCode = 9,

		#endregion auth types

		#region teledata types

		/// <summary>
		/// Используется абонентским терминалом при передаче основных данных определения местоположения
		/// EGTS_SR_POS_DATA
		/// </summary>
		PositionalData = 16,

		/// <summary>
		/// Используется абонентским терминалом при передаче дополнительных данных определения местоположения
		/// EGTS_SR_EXT_POS_DATA
		/// </summary>
		ExtraPositionalData = 17,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс 
		/// информации о состоянии дополнительных дискретных и аналоговых входов
		/// EGTS_SR_AD_SENSORS_DATA
		/// </summary>
		AnalogDigitalSensorData = 18,

		/// <summary>
		/// Используется аппаратно-программным комплексом для передачи на абонентский терминал данных о значении счетных входов
		/// EGTS_SR_COUNTERS_DATA
		/// </summary>
		CountersData = 19,

		/// <summary>
		/// Используется для передачи на аппаратно-программный комплекс информации о состоянии абонентского терминала
		/// EGTS_SR_STATE_DATA
		/// </summary>
		TerminalState = 21,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о состоянии шлейфовых входов
		/// EGTS_SR_LOOPIN_DATA
		/// </summary>
		LoopbackInputData = 22,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о состоянии одного дискретного входа
		/// EGTS_SR_ABS_DIG_SENS_DATA
		/// </summary>
		SingleDigitalSensorData = 23,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о состоянии одного аналогового входа
		/// EGTS_SR_ABS_AN_SENS_DATA
		/// </summary>
		SingleAnalogSensorData = 24,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о состоянии одного счетного входа
		/// EGTS_SR_ABS_CNTR_DATA
		/// </summary>
		SingleCounterData = 25,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о состоянии одного шлейфового входа
		/// EGTS_SR_ABS_LOOPIN_DATA
		/// </summary>
		SingleLoopbackInputData = 26,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о показаниях ДУЖ
		/// EGTS_SR_LIQUID_LEVEL_SENSOR
		/// </summary>
		LiquidLevelSensorData = 27,

		/// <summary>
		/// Применяется абонентским терминалом для передачи на аппаратно-программный комплекс данных о показаниях счетчиков пассажиропотока
		/// EGTS_SR_PASSENGERS_COUNTERS
		/// </summary>
		PassengerCount = 28,

		#endregion teledata types

		#region ecall types

		/// <summary>
		/// ACCEL_DATA
		/// EGTS_SR_ACCEL_DATA
		/// </summary>
		AccelData = 20,
		/// <summary>
		/// RAW_MSD_DATA
		/// EGTS_SR_RAW_MSD_DATA
		/// </summary>
		RawMSDData = 40,
		/// <summary>
		/// MSD_DATA
		/// EGTS_SR_MSD_DATA
		/// </summary>
		MSDData = 50,
		/// <summary>
		/// TRACK_DATA
		/// EGTS_SR_TRACK_DATA
		/// </summary>
		TrackData = 62,

		#endregion ecall types

		#region firmware types

		/// <summary>
		/// SERVICE_PART_DATA
		/// EGTS_SR_SERVICE_PART_DATA
		/// </summary>
		FirmwarePartData = 33,
		/// <summary>
		/// SERVICE_FULL_DATA
		/// EGTS_SR_SERVICE_FULL_DATA
		/// </summary>
		FirmwareFullData = 34

		#endregion firmware types
	}
}
