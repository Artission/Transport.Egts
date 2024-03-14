using System.Collections.Generic;
using System.Text;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using Transport.EGTS.Helpers;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	/// <summary>
	/// 
	/// </summary>
	public class AuthDispatcherIdentitySubrecordParser: ISubrecordParser
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
			get { return EgtsSubrecordType.DispatcherIdentity; }
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
			get { return EgtsServiceType.Authentication;}
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
			var ret = new EgtsAuthDispatcherIdentity();

			var begSize = remainsize;

			byte byt = 0;
			uint uin = 0;

			if (!ByteCombineHelper.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.DT = byt;
			if (!ByteCombineHelper.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.DID = uin;

			var unreadSize = srl - (begSize - remainsize);
			if (unreadSize > 0)
			{
				var str = ByteCombineHelper.ReadFixedSizeString((uint) unreadSize, dataArray, ref remainsize, ref offset);
				if (str == null)
					return null;
				ret.DSCR = str;
			}
			return ret;
		}

		/// <summary>
		/// Writes the subrecord to bytes.
		/// SRL заполняется тут же
		/// </summary>
		/// <param name="subrecord">The subrecord.</param>
		/// <returns></returns>
		public byte[] WriteSubrecordToBytes(ISubrecordData subrecord)
		{
			var obj = subrecord as EgtsAuthDispatcherIdentity;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();
			ret.Add(obj.DT);
			ret.AddRange(ByteCombineHelper.WriteValueToBuffer(obj.DID,4));
			ret.AddRange(Encoding.ASCII.GetBytes(obj.DSCR));
			return ret.ToArray();
		}

		#endregion
	}
}
