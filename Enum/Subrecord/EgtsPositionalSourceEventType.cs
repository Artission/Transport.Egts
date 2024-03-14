
namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// Список источников посылок координатных данных
	/// </summary>
	public enum EgtsPositionalSourceEventType
	{
		/// <summary>
		/// таймер при включенном зажигании
		/// </summary>
		IgnitionOnTimer=0,

		/// <summary>
		/// пробег заданной дистанции
		/// </summary>
		MileageCompleted=1,

		/// <summary>
		/// превышение установленного значения угла поворота
		/// </summary>
		ExceedAngle=2,

		/// <summary>
		/// ответ на запрос
		/// </summary>
		Responce=3,

		/// <summary>
		/// изменение состояния входа X
		/// </summary>
		XStateChange=4,

		/// <summary>
		/// таймер при выключенном зажигании
		/// </summary>
		IgnitionOffTimer=5,

		/// <summary>
		/// отключение периферийного оборудования
		/// </summary>
		PeriferyDisable=6,

		/// <summary>
		/// превышение одного из заданных порогов скорости
		/// </summary>
		ExceedSpeed=7,

		/// <summary>
		/// перезагрузка центрального процессора (рестарт)
		/// </summary>
		CPURestart=8,

		/// <summary>
		/// перегрузка по выходу Y
		/// </summary>
		YOverflow=9,

		/// <summary>
		/// сработал датчик вскрытия корпуса прибора
		/// </summary>
		TerminalOpened=10,

		/// <summary>
		/// переход на резервное питание/отключение внешнего питания
		/// </summary>
		ExternalPowerSupplyOff=11,

		/// <summary>
		/// снижение напряжения источника резервного питания ниже порогового значения
		/// </summary>
		InternalPoweSupplyLow=12,

		/// <summary>
		/// нажата "тревожная кнопка"
		/// </summary>
		DangerButton=13,

		/// <summary>
		/// запрос на установление голосовой связи с оператором
		/// </summary>
		VoiceConnectionRequest=14,

		/// <summary>
		/// экстренный вызов
		/// </summary>
		EmergencyCall=15,

		/// <summary>
		/// появление данных от внешнего сервиса
		/// </summary>
		ExternalServiceData=16,
/*
		/// <summary>
		/// зарезервировано
		/// </summary>
		Reserved=17,

		/// <summary>
		/// зарезервировано
		/// </summary>
		Reserved=18,*/

		/// <summary>
		/// неисправность резервного аккумулятора
		/// </summary>
		ReserveBatteryFail=19,

		/// <summary>
		/// резкий разгон
		/// </summary>
		ForcedAcceleration=20,

		/// <summary>
		/// резкое торможение
		/// </summary>
		ForcedStop=21,

		/// <summary>
		/// отключение или неисправность навигационного модуля
		/// </summary>
		NavigationModuleDisable=22,

		/// <summary>
		/// отключение или неисправность датчика автоматической идентификации события ДТП
		/// </summary>
		IdSensorDisable=23,

		/// <summary>
		/// отключение или неисправность антенны GSM/UMTS
		/// </summary>
		GSMDisable=24,

		/// <summary>
		/// отключение или неисправность антенны навигационной системы
		/// </summary>
		NavigationSensorDisable=25,
		/*
		/// <summary>
		/// зарезервировано
		/// </summary>
		Reserved=26,*/

		/// <summary>
		/// снижение скорости ниже одного из заданных порогов
		/// </summary>
		SpeedLowered=27,

		/// <summary>
		/// перемещение при выключенном зажигании
		/// </summary>
		IgnitionOffMove=28,

		/// <summary>
		/// таймер в режиме "экстренное слежение"
		/// </summary>
		EmergencyTrackingTimer=29,

		/// <summary>
		/// начало/окончание навигации
		/// </summary>
		NavigationStateChanged=30,

		/// <summary>
		/// "нестабильная навигация" (превышение порога частоты прерывания режима навигации при включенном зажигании или режиме экстренного слежения)
		/// </summary>
		UnstableNavigation=31,

		/// <summary>
		/// установка IP соединения
		/// </summary>
		IPConnection=32,

		/// <summary>
		/// нестабильная регистрация в сети подвижной радиотелефонной связи
		/// </summary>
		UnstableRadioRegistration=33,

		/// <summary>
		/// "нестабильная связь" (превышение порога частоты прерывания/восстановления IP соединения при включенном зажигании или режиме экстренного слежения)
		/// </summary>
		UnstableConnection=34,

		/// <summary>
		/// изменение режима работы
		/// </summary>
		WorkModeChanged=35

	}
}
