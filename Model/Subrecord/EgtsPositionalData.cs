using System;
using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Helpers;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// данные о местоположении
	/// EGTS_SR_POS_DATA
	/// </summary>
	public class EgtsPositionalData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.PositionalData; }
		}
		/// <summary>
		/// время навигации (количество секунд с 00:00:00 01.01.2010 UTC);
		/// </summary>
		public uint NTM { get; set; }

		/// <summary>
		/// широта по модулю, градусы/90 · 0xFFFFFFFF и взята целая часть;
		/// </summary>
		public uint LAT { get; set; }

		/// <summary>
		/// долгота по модулю, градусы/180 · 0xFFFFFFFF и взята целая часть;
		/// </summary>
		public uint LONG { get; set; }

		#region first bitfields

		/// <summary>
		/// битовый флаг определяет наличие поля ALT в подзаписи: 1- - поле ALT передается; 0- - не передается;
		/// </summary>
		public bool ALTE { get; set; }

		/// <summary>
		/// битовый флаг определяет полушарие долготы: 0- - восточная долгота; 1- - западная долгота;
		/// </summary>
		public bool LOHS { get; set; }

		/// <summary>
		/// битовый флаг определяет полушарие широты: 0- - северная широта; 1- - южная широта;
		/// </summary>
		public bool LAHS { get; set; }

		/// <summary>
		/// битовый флаг, признак движения: 1- - движение; 0- - транспортное средство находится в режиме стоянки;
		/// </summary>
		public bool MV { get; set; }

		/// <summary>
		/// битовый флаг, признак отправки данных из памяти ("черный ящик"): 0- - актуальные данные; 1- - данные из памяти ("черного ящика");
		/// </summary>
		public bool BB { get; set; }

		/// <summary>
		/// битовое поле, тип определения координат: 0- - 2D fix; 1- - 3D fix;
		/// </summary>
		public bool FIX { get; set; }

		/// <summary>
		/// битовое поле, тип используемой системы: 0- - система координат WGS-84; 1- - государственная геоцентрическая система координат (ПЗ-90.02);
		/// </summary>
		public bool CS { get; set; }

		/// <summary>
		/// битовый флаг, признак "валидности" координатных данных: 1- - данные "валидны"; 0- - "невалидные" данные;
		/// </summary>
		public bool VLD { get; set; }

		#endregion first bitfields


		/// <summary>
		/// скорость в км/ч с дискретностью 0,1 км/ч (используется 14 младших бит);
		/// комбинируется из 1 полного байта и 5 бит следующего
		/// </summary>
		public ushort SPD { get; set; }

		/// <summary>
		/// (Altitude Sign) битовый флаг, определяет высоту относительно уровня моря и имеет смысл только при установленном флаге ALTE: 
		/// 0- точка выше уровня моря; 1 - ниже уровня моря;
		/// </summary>
		public bool ALTS { get; set; }

		/// <summary>
		/// направление движения. 
		/// Определяется как угол в градусах, который отсчитывается по часовой стрелке между северным направлением 
		/// географического меридиана и направлением движения в точке измерения (дополнительно старший бит находится в поле DIRH);
		/// комбинируется из 1 верхнего бита и полного нижнего байта
		/// </summary>
		public ushort DIR { get; set; }

		/// <summary>
		/// пройденное расстояние (пробег) в км, с дискретностью 0,1 км;
		/// ODM
		/// </summary>
		public List<byte> OdometerData
		{
			get { return _odmData; }
		}
		private readonly List<byte> _odmData = new List<byte>();

		/// <summary>
		/// битовые флаги, определяют состояние основных дискретных входов 1 ... 8 (если бит равен 1, то соответствующий вход активен, если 0, то неактивен).
		///  Данное поле включено для удобства использования и экономии трафика при работе в системах мониторинга транспорта базового уровня;
		/// </summary>
		public byte DIN { get; set; }

		/// <summary>
		/// определяет источник (событие), инициировавший посылку данной навигационной информации (информация представлена в Таблице N 3);
		/// </summary>
		public EgtsPositionalSourceEventType SRC { get; set; }

		/// <summary>
		/// высота над уровнем моря, м (опциональный параметр, наличие которого определяется битовым флагом ALTE);
		/// </summary>
		public uint ALT { get; set; }

		/// <summary>
		/// данные, характеризующие источник (событие) из поля SRC. Наличие и интерпретация значения данного поля определяется полем SRC.
		/// </summary>
		public short SRCD { get; set; }

		#region unwrapped properties
		
		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>
		/// The latitude.
		/// </value>
		public double Latitude
		{
			get { return EgtsCoordinateHelper.UintLatitude2Double(LAT); }
			set { LAT = EgtsCoordinateHelper.DoubleLatitude2Uint(value); }
		}

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>
		/// The longitude.
		/// </value>
		public double Longitude
		{
			get { return EgtsCoordinateHelper.UintLongitude2Double(LONG); }
			set { LONG = EgtsCoordinateHelper.DoubleLongitude2Uint(value); }
		}

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime Date
		{
			get { return EgtsDateTimeHelper.UInt2DateTime(NTM); }
			set { NTM = EgtsDateTimeHelper.DateTime2UInt(value); }
		}

		#endregion unwrapped properties
	}
}
