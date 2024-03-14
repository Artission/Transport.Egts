
namespace Transport.EGTS.Enum.Subrecord
{
	public enum EgtsLiquidLevelSensorValueUnit
	{
		/// <summary>
		/// нетарированное показание ДУЖ
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// показания ДУЖ в процентах от общего объема емкости;
		/// </summary>
		Percent = 1,

		/// <summary>
		/// показания ДУЖ в литрах с дискретностью в 0,1 литра.
		/// </summary>
		Liters = 2
	}
}
