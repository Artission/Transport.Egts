using System;

namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// значение состояния соответствующего входа
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), // зеровэлью она хочет чтоб называлось None но нормал, это состояние по документации, и не хочется его называть невнятным None
	System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames"), Flags]	// множественное число тут тоже не логично, парсится всего 1 байт
	public enum EgtsLoopInState
	{
		/// <summary>
		/// норма
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Тревога
		/// </summary>
		Alarm = 1,

		/// <summary>
		/// обрыв
		/// </summary>
		Break = 2,

		/// <summary>
		/// замыкание на землю
		/// </summary>
		EarthCircuit = 4,

		/// <summary>
		/// замыкание на питание
		/// </summary>
		PowerCircuit = 8
	}
}
