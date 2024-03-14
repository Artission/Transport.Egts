using Transport.EGTS.Enum;
using Transport.EGTS.Exceptions;
using Transport.EGTS.Model;
using byteHelper = Transport.EGTS.Helpers.ByteCombineHelper;

namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// Класс для декодирования байтиков, в том числе и зашифрованных в заголовок
	/// </summary>
	internal class HeaderEncodingHelper
	{

		/// <summary>
		/// Decodes the header bytes.
		/// </summary>
		/// <param name="phead">The phead.</param>
		/// <param name="pecnoded_part">The pecnoded_part.</param>
		/// <param name="encoded_size">The encoded_size.</param>
		/// <returns></returns>
		internal bool DecodeHeaderBytes( EgtsPacketHeader phead , byte[] pecnoded_part , ushort encoded_size  )
		{

			if (phead.HE == 0)
			{
				return readPlainHeader(phead, pecnoded_part, encoded_size);
			}
			throw new EgtsProceedException("Неподдерживаемое кодирование заголовка");
		}

		internal byte[] EncodeHeader(EgtsPacketHeader header, byte headerEncoding)
		{
			if (headerEncoding == 0)
			{
				return _writePlainHeader(header);
			}
			throw new EgtsProceedException("Неподдерживаемое кодирование заголовка");
		}

		#region private methods

		private bool readPlainHeader(EgtsPacketHeader phead, byte[] pecnoded_part, ushort encoded_size)
		{
			var puc = pecnoded_part;
			var sz = encoded_size;

			ulong val = 0;
			if (byteHelper.ReadValueFromBuffer(ref val,sizeof(ushort), puc, ref sz, encoded_size - sz))
				phead.FDL = (ushort)val;
			if (byteHelper.ReadValueFromBuffer(ref val, sizeof(ushort), puc, ref sz, encoded_size - sz))
				phead.PID = (ushort)val;
			if (byteHelper.ReadValueFromBuffer(ref val, sizeof(byte), puc, ref sz, encoded_size - sz))
				phead.PT = (EgtsPacketType)val;
			if (phead.RTE)
			{
				if (byteHelper.ReadValueFromBuffer(ref val, sizeof(ushort), puc, ref sz, encoded_size - sz))
					phead.PRA = (ushort) val;
				if (byteHelper.ReadValueFromBuffer(ref val, sizeof(ushort), puc, ref sz, encoded_size - sz))
					phead.RCA = (ushort) val;
				if (byteHelper.ReadValueFromBuffer(ref val, sizeof(byte), puc, ref sz, encoded_size - sz))
					phead.TTL = (byte) val;
			}

			if (sz != 0U)
			{
				return false;
			}
			return true;

		}

		private byte[] _writePlainHeader(EgtsPacketHeader header)
		{
			var arrSize = header.RTE ? Constants.HeaderLengthEncoded + Constants.HeaderRouteLength : Constants.HeaderLengthEncoded;
			var ret = new byte[arrSize];
			var offset = 0;
			var ushSize = sizeof (ushort);
			var bytSize = sizeof (byte);

			var buf = byteHelper.WriteValueToBuffer(header.FDL, ushSize);
			if (buf==null)
				return null;
			buf.CopyTo(ret,offset);
			offset += ushSize;
			buf = byteHelper.WriteValueToBuffer(header.PID, ushSize);
			if (buf == null)
				return null;
			buf.CopyTo(ret, offset);
			offset += ushSize;
			buf = byteHelper.WriteValueToBuffer((byte)header.PT, bytSize);
			if (buf == null)
				return null;
			buf.CopyTo(ret, offset);
			offset += bytSize;
			if (header.RTE)
			{
				if (header.PRA == null || header.RCA == null || header.TTL == null)
					return null;
				buf = byteHelper.WriteValueToBuffer(header.PRA.Value, ushSize);
				if (buf == null)
					return null;
				buf.CopyTo(ret, offset);
				offset += ushSize;
				buf = byteHelper.WriteValueToBuffer(header.RCA.Value, ushSize);
				if (buf==null)
					return null;
				buf.CopyTo(ret, offset);
				offset += ushSize;
				buf = byteHelper.WriteValueToBuffer(header.TTL.Value, bytSize);
				if (buf==null)
					return null;
				buf.CopyTo(ret, offset);
			}

			return ret;
		}

		

		#endregion private methods
	}
}
