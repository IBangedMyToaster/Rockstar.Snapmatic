using Rockstar.Snapmatic.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic
{
    public static class Snapmatic
    {
        private const int _header = 292;

        /// <summary>
        /// Removes the extra added bytes of the Snapmatic File and checks for corruption.
        /// </summary>
        /// <param name="fileName">Full Path of the Snapmatic File</param>
        /// <returns>Returns a <see langword="byte"/> Array representing the Snapmatic Data</returns>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="InvalidDataException"/>
        public static byte[] Convert(string fileName)
        {
            using FileStream fileStream = new FileInfo(fileName).OpenRead();
            byte[]? result = fileStream.Skip(_header);

            if (result is null)
                throw new InvalidOperationException($"The File {fileName} is invalid. Offset of {_header} is higher than StreamLength ({fileStream.Length})");

            if (IsCorrupt(result))
                throw new InvalidDataException($"The File {fileName} is corrupt and won't be able to be converted.");

            return result;
        }

        /// <summary>
        /// Save a <see langword="byte"/> Array representing the ImageData as <see cref="Format"/>.
        /// </summary>
        /// <param name="imageData"><see langword="byte"/> Array of the ImageData.</param>
        /// <param name="fileName">Name of the new generated File without Extension.</param>
        /// <param name="format">Format of the new generated File, <see cref="Format.Jpeg"/> is the <see langword="default"/> Value</param>
        public static void Save(byte[] imageData, string fileName, Format format = Format.Jpeg)
        {
            using (Image image = Image.FromStream(new MemoryStream(imageData)))
            {
                image.Save($"{fileName}.{format.ToString().ToLower()}", GetFormat(format));
            }
        }

        private static bool IsCorrupt(byte[] imageData)
        {
            try
            {
                using (var ms = new MemoryStream(imageData))
                {
                    using (var bmp = new Bitmap(ms))
                    {
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private static ImageFormat GetFormat(Format format) => format switch
        {
            Format.Jpeg => ImageFormat.Jpeg,
            Format.Png => ImageFormat.Png,
            _ => throw new NotSupportedException()
        };
    }
}
