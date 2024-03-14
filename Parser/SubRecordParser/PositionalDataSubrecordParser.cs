using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Model.Subrecord;
using br = Transport.EGTS.Helpers.ByteCombineHelper;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class PositionalDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT
		{
			get { return EgtsSubrecordType.PositionalData; }
		}

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType
		{
			get { return EgtsServiceType.TeleData; }
		}

		/// <summary>
		/// Reads the sub record from bytes.
		/// </summary>
		/// <param name="srl">The subrecord data length.</param>
		/// <param name="dataArray">The data array.</param>
		/// <param name="remainsize">The remainsize.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		public ISubrecordData ReadSubRecordFromBytes(ushort srl, byte[] dataArray, ref ushort remainsize, ref ushort offset)
		{
			var ret = new EgtsPositionalData();

			uint uin = 0;
			ushort ush = 0;
			byte byt = 0;
			var begSize = remainsize;

			if (!br.ReadNextUintBytes(ref uin,dataArray,ref remainsize, ref offset))
				return null;
			ret.NTM = uin;
			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.LAT = uin;
			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.LONG = uin;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitfields(ret, byt);

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			var spdl = byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			var dirhspdh = byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			var dirl = byt;
			ret.SPD = (ushort)(spdl | ((dirhspdh & 0x3f) << 8));
			ret.DIR = (ushort)(((dirhspdh & 0x80) << 1) | dirl);
			ret.ALTS = (dirhspdh & 0x20) == 1;

			var arr = br.ReadRawByteArray(3, dataArray, ref remainsize, ref offset);
			if (arr==null)
				return null;
			ret.OdometerData.Clear();
			ret.OdometerData.AddRange(arr);

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.DIN = byt;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.SRC = (EgtsPositionalSourceEventType) byt;

			const ushort usSize = 3;	//там прям в документашке, не полный улонг а трёхбайтовый
			if (ret.ALTE)
			{
				
				ulong uval = 0;
				if (!br.ReadValueFromBuffer(ref uval, usSize, dataArray, ref remainsize, offset))
					return null;
				offset += usSize;
				ret.ALT = (uint)uval;
			}
			var remDat = srl - (begSize - remainsize);
			// новый отправляльщик (сервер) появился, который похоже заполняет необязатаельные поля нулями, посему делаем обрезку ниже
			if (remDat > 2) // возможно отсутсвующие поля заполняются нулями, тогда мы 3 верхних (ALT) пропустим
			{
				remainsize -= usSize;
				offset += usSize;
				remDat -= 3;
			}
			if (remDat > 0)
			{
				if (!br.ReadNextUshortBytes(ref ush,dataArray,ref remainsize, ref offset))
					return null;
				ret.SRCD = (short) ush;
			}

			return ret;
		}

		/// <summary>
		/// Writes the subrecord to bytes.
		/// SRL заполняется тут же
		/// </summary>
		/// <param name="subrecord">The subrecord.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public byte[] WriteSubrecordToBytes(ISubrecordData subrecord)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region private methods

		private void parseBitfields(EgtsPositionalData pd, byte b)
		{
			pd.VLD = (b & 0x01) == 1;
			b >>= 1;
			pd.FIX = (b & 0x01) == 1;
			b >>= 1;
			pd.CS = (b & 0x01) == 1;
			b >>= 1;
			pd.BB = (b & 0x01) == 1;
			b >>= 1;
			pd.MV = (b & 0x01) == 1;
			b >>= 1;
			pd.LAHS = (b & 0x01) == 1;
			b >>= 1;
			pd.LOHS = (b & 0x01) == 1;
			b >>= 1;
			pd.ALTE = (b & 0x01) == 1;
		}

		#endregion private methods
	}
}
