using System.Collections.Generic;
using System.Text;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class AuthInfoSubrecordParser: ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.AuthInfo; } }

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType { get { return EgtsServiceType.Authentication; } }

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
			var ret = new EgtsAuthInfo();

			var begSize = remainsize;

			var str = br.ReadStringToDelimiter(Constants.MaxUserNameLength, dataArray, ref remainsize, ref offset);
			if (str==null)
				return null;
			ret.UNM = str;
			str = br.ReadStringToDelimiter(Constants.MaxPasswordLength, dataArray, ref remainsize, ref offset);
			if (str == null)
				return null;
			ret.UPSW = str;

			var unreadSize = srl - (begSize - remainsize);
			if (unreadSize > 0)
			{
				str = br.ReadStringToDelimiter(Constants.MaxServerSequenceLength, dataArray, ref remainsize, ref offset);
				if (str == null)
					return null;
				ret.SS = str;
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
			var obj = subrecord as EgtsAuthInfo;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();
			if (obj.UNM.Length > Constants.MaxUserNameLength)
				throw new EgtsProceedException("Too big user name length");
			if (obj.UPSW.Length > Constants.MaxUserNameLength)
				throw new EgtsProceedException("Too big Password length");

			ret.AddRange(Encoding.ASCII.GetBytes(obj.UNM));
			ret.Add(0);
			ret.AddRange(Encoding.ASCII.GetBytes(obj.UPSW));
			ret.Add(0);
			
			if (!string.IsNullOrWhiteSpace(obj.SS))
			{
				if (obj.SS.Length > Constants.MaxServerSequenceLength)
					throw new EgtsProceedException("Too big ServerSequence length");
				ret.AddRange(Encoding.ASCII.GetBytes(obj.SS));
				ret.Add(0);
			}

			return ret.ToArray();
		}

		#endregion
	}
}
