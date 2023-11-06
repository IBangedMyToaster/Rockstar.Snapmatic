using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic.Extensions
{
    internal static class FileStreamExtensions
    {
        internal static byte[]? Skip(this FileStream fileStream, int offset)
        {
            if(fileStream.Length <= offset)
                return null;

            int count = (int)fileStream.Length - offset;
            byte[] buffer = new byte[count];

            fileStream.Seek(offset, SeekOrigin.Begin);

            for (int i = 0; i < count; i++)
            {
                int current = fileStream.ReadByte();

                if (current == -1)
                    break;

                buffer[i] = ((byte)current);
            }

            return buffer;
        }
    }
}
