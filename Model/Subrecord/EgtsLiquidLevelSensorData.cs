using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// ДУЖ
	/// </summary>
	public class EgtsLiquidLevelSensorData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.LiquidLevelSensorData; }
		}
		#region bitfields

		/// <summary>
		/// (Liquid Level Sensor Number) порядковый номер датчика;
		/// </summary>
		/// <value>
		/// The LLSN.
		/// </value>
		public byte LLSN { get; set; }

		/// <summary>
		/// (Raw Data Flag) флаг, определяющий формат поля LiquidSensorData данной подзаписи
		/// true - поле LiquidSensorData имеет размер 4 байта (тип данных UINT) и содержит показания ДУЖ в формате, определяемом полем LLSVU;
		/// false - поле LiquidSensorData содержит данные ДУЖ в неизменном виде, как они поступили из внешнего порта абонентского терминала, длинной до 512 байт
		/// </summary>
		public bool RDF { get; set; }

		/// <summary>
		/// (Liquid Level Sensor Value Unit) битовый флаг, определяющий единицы измерения показаний ДУЖ
		/// </summary>
		public EgtsLiquidLevelSensorValueUnit LLSVU { get; set; }

		/// <summary>
		/// (Liquid Level Sensor Error Flag) битовый флаг, определяющий наличие ошибок при считывании значения датчика уровня жидкости (далее - ДУЖ)
		/// </summary>
		public bool LLSEF { get; set; }

		#endregion bitfields

		/// <summary>
		/// адрес модуля, данные о показаниях ДУЖ с которого поступили в абонентский терминал (номер внешнего порта абонентского терминала);
		/// </summary>
		/// <value>
		/// The maddr.
		/// </value>
		public ushort MADDR { get; set; }

		/// <summary>
		/// LLSD - показания ДУЖ в формате, определяемом полем RDF.
		/// </summary>
		/// <value>
		/// The liquid sensor data.
		/// </value>
		public List<byte> LiquidSensorData
		{
			get { return _llsd; }
		}
		private readonly List<byte> _llsd = new List<byte>();
	}
}
