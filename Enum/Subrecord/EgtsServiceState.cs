
namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// 
	/// </summary>
	public enum EgtsServiceState
	{
		/// <summary>
		/// Сервис в рабочем состоянии и разрешен к использованию
		/// EGTS_SST_IN_SERVICE
		/// </summary>
		Works = 0,

		/// <summary>
		/// Сервис в нерабочем состоянии (выключен)
		/// EGTS_SST_OUT_OF_SERVICE
		/// </summary>
		Disabled = 128,

		/// <summary>
		/// Сервис запрещён для использования
		/// EGTS_SST_DENIED
		/// </summary>
		Denied = 129,

		/// <summary>
		/// Сервис не настроен
		/// EGTS_SST_NO_CONF
		/// </summary>
		NotConfigured = 130,

		/// <summary>
		/// Сервис временно недоступен
		/// EGTS_SST_TEMP_UNAVAIL
		/// </summary>
		TemporaryUnavalible = 131
	}
}
