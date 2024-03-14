
namespace Transport.EGTS.Enum
{
	/// <summary>
	/// тип пакета, варианты перечислены в документации
	/// </summary>
	public enum EgtsPacketType
	{
		/// <summary>
		/// EGTS_PT_RESPONSE (подтверждение на пакет Транспортного уровня)
		/// </summary>
		Response = 0,
		/// <summary>
		/// EGTS_PT_APPDATA (пакет, содержащий данные протокола Уровня поддержки услуг)
		/// </summary>
		AppData = 1, 
		/// <summary>
		/// EGTS_PT_SIGNED_APPDATA (пакет, содержащий данные протокола Уровня поддержки услуг с цифровой подписью).
		/// </summary>
		SignedAppData = 2 
	}
}
