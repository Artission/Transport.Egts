
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// информация по сенсорам
	/// egts_TELEDATA_AD_SENSORS_DATA_t
	/// </summary>
	public class EgtsAnalogDigitalData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.AnalogDigitalSensorData; }
		}

		/// <summary>
		/// DIOE8 DIOE7 DIOE6 DIOE5 DIOE4 DIOE3 DIOE2 DIOE1
		/// (Digital Inputs Octet Exists) битовые флаги, определяющие наличие соответствующих полей дополнительных дискретных входов.
		/// Всего в одной подзаписи данного типа может быть передана информация о состоянии дополнительных 64 входов:
		/// </summary>
		/// <value>
		/// The dioe.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]// низкоуровневое заполнение, можно, с коллекцией слишком тяжко былоб
		public bool[] DIOE { get { return _dioe; } }
		private readonly bool[] _dioe = new bool[8];

		/// <summary>
		/// DOUT - битовые флаги дискретных выходов (если бит установлен в 1, то соответствующий этому биту выход активен);
		/// </summary>
		/// <value>
		/// The dout.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] DOUT { get { return _dout; } }
		private readonly bool[] _dout = new bool[8];

		/// <summary>
		///  /* ASFE8 ASFE7 ASFE6 ASFE5 ASFE4 ASFE3 ASFE2 ASFE1 */
		/// (Analog Sensor Field Exists) битовые флаги, определяющие наличие показаний от соответствующих аналоговых датчиков 
		/// (если бит установлен в 1, то данные от соответствующего датчика присутствуют, если 0, данные отсутствуют). 
		/// Если, например, поля ASFE1=1 и ASFE3=1, то в подзаписи после байта флагов ASFE8 - ASFE1 будут переданы 3 байта значений ANS1 и 3 байта значений ANS3.
		/// Значения для датчика ANS2, а также датчиков ANS4... ANS8 не будут передаваться в данной подзаписи;
		/// </summary>
		/// <value>
		/// The asfe.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] ASFE { get { return _asfe; } }
		private readonly bool[] _asfe = new bool[8];

		/// <summary>
		/// ADIO1 ... ADIO64 - показания дополнительных дискретных входов. 
		/// Поля представляют собой битовую маску, в которой значение каждого бита определяет активность соответствующего дискретного входа
		/// </summary>
		/// <value>
		/// The adio.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] ADIO { get { return _adio; } }
		private readonly bool[] _adio = new bool[64];


		/// <summary>
		/// ANS1 ... ANS8 - значение аналоговых датчиков с 1 по 8 соответственно.
		/// </summary>
		/// <value>
		/// The ans.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public uint[] ANS { get { return _ans; } }
		private readonly uint[] _ans = new uint[8];
	}
}
