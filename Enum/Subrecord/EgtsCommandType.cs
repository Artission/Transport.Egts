

namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// типы комманд(и подтверждений, по сути)
	/// </summary>
	public enum EgtsCommandType
	{
		/// <summary>
		/// CA требует, хотя по идее такого значения не должно бы быть
		/// </summary>
		None = 0,
		/// <summary>
		/// подтверждение о приеме, обработке или результат выполнения команды
		/// CT_COMCONF
		/// </summary>
		CommandConfirmation = 1,
		/// <summary>
		/// подтверждение о приеме, отображении и/или обработке информационного сообщения
		/// CT_MSGCONF
		/// </summary>
		MessageConfirmation = 2,
		/// <summary>
		/// информационное сообщение от абонентского терминала
		/// CT_MSGFROM
		/// </summary>
		MessageFrom = 3,
		/// <summary>
		/// информационное сообщение для вывода на устройство отображения
		/// CT_MSGTO
		/// </summary>
		MessageTo = 4,
		/// <summary>
		/// команда для выполнения на абонентском терминале
		/// CT_COM
		/// </summary>
		Command = 5,
		/// <summary>
		/// удаление из очереди на выполнение переданной ранее команды
		/// CT_DELCOM
		/// </summary>
		DeleteCommand = 6,
		/// <summary>
		/// дополнительный подзапрос для выполнения (к переданной ранее команде)
		/// CT_SUBREQ
		/// </summary>
		SubCommand = 7,
		/// <summary>
		/// подтверждение о доставке команды или информационного сообщения;
		/// CT_DELIV
		/// </summary>
		DeliveryConfirmation = 8
	}
}
