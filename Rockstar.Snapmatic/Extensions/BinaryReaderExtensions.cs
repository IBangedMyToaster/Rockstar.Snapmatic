using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic.Extensions
{
    internal static class BinaryReaderExtensions
    {
        [Obsolete($"Use {nameof(BinaryReaderExtensions.ReadBytesAsString)} with the byte count instead.")]
        internal static string ReadStringToEnd(this BinaryReader reader)
        {
            string result = String.Empty;
            char c;

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                if ((c = reader.ReadChar()) == 0)
                    break;

                result += c;
            }

            return result;
        }

        internal static string ReadInt32AsString(this BinaryReader reader)
        {
            return ReadBytesAsString(reader, 4);
        }

        internal static string ReadBytesAsString(this BinaryReader reader, int count)
        {
            string result = String.Empty;
            long pos = reader.BaseStream.Position;
            char c;

            while (reader.BaseStream.Position < (pos + count))
            {
                if ((c = reader.ReadChar()) == 0)
                    break;

                result += c;
            }

            return result;
        }
    }
}
