﻿using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class TerminalStateDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SRT
		{
			get { return EgtsSubrecordType.TerminalState; }
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
			var ret = new EgtsTerminalStateData();

			byte byt = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.ST = (EgtsTerminalState)byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.MPSV = byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.BBV = byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.IBV = byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBifields(ret, byt);

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

		private void parseBifields(EgtsTerminalStateData tsd, byte b)
		{
			tsd.BBU = (b & 0x01) == 1;
			b >>= 1;
			tsd.IBU = (b & 0x01) == 1;
			b >>= 1;
			tsd.NMS = (b & 0x01) == 1;
		}

		#endregion private methods
	}
}
