
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class ResponceSubrecordParser: ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.Response; } }

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType { get
		{
			return EgtsServiceType.Response;
		} }

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
			var ret = new EgtsResponse();
			ushort ush = 0;
			byte byt = 0;
			if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
				return null;
			if (!br.ReadNextByteBytes(ref byt,dataArray,ref remainsize,ref offset))
				return null;
			ret.CRN = ush;
			ret.RST = (EgtsProcessingState)byt;
			return ret;
		}

		/// <summary>
		/// Writes the subrecord to bytes.
		/// </summary>
		/// <param name="subrecord">The subrecord.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public byte[] WriteSubrecordToBytes(ISubrecordData subrecord)
		{
			var obj = subrecord as EgtsResponse;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new byte[sizeof (ushort) + sizeof (byte)];
			var buf = br.WriteValueToBuffer(obj.CRN, sizeof (ushort));
			buf.CopyTo(ret, 0);
			buf = br.WriteValueToBuffer((byte) obj.RST, sizeof (byte));
			buf.CopyTo(ret,sizeof(ushort));

			return ret;
		}

		#endregion
	}
}
