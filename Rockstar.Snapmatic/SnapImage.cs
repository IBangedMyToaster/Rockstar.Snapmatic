using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic
{
    public class SnapImage
    {
        public byte[] Buffer { get; init; }
        public Int32 BufferSize { get; init; }
        public int Size { get; init; }

        public SnapImage(BinaryReader reader)
        {
            this.BufferSize = reader.ReadInt32();
            this.Size = reader.ReadInt32();
            this.Buffer = reader.ReadBytes(this.BufferSize);
        }

        /// <summary>
        /// Save a <see langword="byte"/> Array representing the ImageData as <see cref="Format"/>.
        /// </summary>
        /// <param name="imageData"><see langword="byte"/> Array of the ImageData.</param>
        /// <param name="fileName">Name of the new generated File without Extension.</param>
        /// <param name="format">Format of the new generated File, <see cref="Format.Jpeg"/> is the <see langword="default"/> Value</param>
        public void Save(string fileName, Format format = Format.Jpeg)
        {
            using (Image image = Image.FromStream(new MemoryStream(this.Buffer)))
            {
                image.Save($"{fileName}.{format.ToString().ToLower()}", GetFormat(format));
            }
        }

        public virtual bool IsValid()
        {
            try
            {
                using (var ms = new MemoryStream(this.Buffer))
                {
                    using (var bmp = new Bitmap(ms))
                    {
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
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
