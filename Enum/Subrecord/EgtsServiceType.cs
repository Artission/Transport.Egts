namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// типы сервисов, ну такими они задокументированы, в действительности на каждый тип месседжа свой обработчик
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum EgtsServiceType
	{
		/// <summary>
		/// The response
		/// </summary>
		Response = 0,

		/// <summary>
		/// The authentication service
		/// EGTS_AUTH_SERVICE
		/// </summary>
		Authentication = 1,

		/// <summary>
		/// такое чудо есть, и есть даже несколько структур к которым в комментарии приписано что они относятся к теледате, но никакой работы с ним
		/// в документации описание тоже прихрамывает, непонятно как с теледатой быть
		/// EGTS_TELEDATA_SERVICE     (2)
		/// </summary>
		TeleData = 2,

		/// <summary>
		/// The commands
		/// EGTS_COMMANDS_SERVICE
		/// </summary>
		Commands = 4,

		/// <summary>
		/// The firmware
		/// EGTS_FIRMWARE_SERVICE
		/// </summary>
		Firmware = 9,
		
		/// <summary>
		/// The ecall
		/// EGTS_ECALL_SERVICE
		/// </summary>
		Ecall = 10
	}
}
