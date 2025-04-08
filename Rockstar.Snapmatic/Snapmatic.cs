using Microsoft.Win32;
using Rockstar.Snapmatic.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic
{
    public static class Snapmatic
    {
        /// <summary>
        /// Reads the contents of the file at <paramref name="filePath"/> and returns a <see cref="Snap"/> object.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>A valid <see cref="Snap"/> object. Throws an <see cref="InvalidDataException"/> if <paramref name="filePath"/> does not point to a valid Snapmatic file.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException">
        /// Thrown if the <paramref name="filePath"/>is not a valid Snapmatic file.
        /// </exception>
        public static Snap Load(string filePath)
        {
            Snap snap = new Snap();

            using (BinaryReader reader = new BinaryReader(File.OpenRead(filePath)))
            {
                int _snapmaticPrefix = reader.ReadInt32();

                if (_snapmaticPrefix == 0x01000000)
                {
                    snap.FilePath = filePath;
                    snap.FileName = Path.GetFileName(filePath);

                    SnapBinaryData json = new SnapBinaryData(reader, 264, 268);
                    SnapBinaryData title = new SnapBinaryData(reader, 264);
                    SnapBinaryData desc = new SnapBinaryData(reader, 264);
                    snap.ImageFormat = reader.ReadInt32AsString();
                    snap.Image = new SnapImage(reader);

                    json.ReadData();
                    title.ReadData();
                    desc.ReadData();

                    // Snapmatic JSON
                    snap.RawJSON = json.Value;
                    snap.JSON = JsonSerializer.Deserialize<SnapJSON>(snap.RawJSON) ?? throw new ArgumentNullException(nameof(snap.JSON));

                    // Snapmatic DateTime
                    snap.Creation = DateTimeOffset.FromUnixTimeSeconds(snap.JSON.creat).LocalDateTime;

                    // Snapmatic Title
                    snap.Title = title.Value ?? throw new ArgumentNullException(nameof(title.Value));

                    // Description
                    snap.Description = desc.Value;
                }
                else
                    throw new InvalidDataException($"The argument {nameof(filePath)} is not a valid Snapmatic file.");
            }

            return snap;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------

        private const int _HEADER = 292;

        [Obsolete("Use 'Rockstar.Snapmatic.Snap.Image.Buffer' to retrieve the image buffer. To obtain a valid Snap object, pass the file path to 'Rockstar.Snapmatic.Snapmatic.Load'.")]
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
            byte[]? result = fileStream.Skip(_HEADER);

            if (result is null)
                throw new InvalidOperationException($"The File {fileName} is invalid. Offset of {_HEADER} is higher than StreamLength ({fileStream.Length})");

            if (IsCorrupt(result))
                throw new InvalidDataException($"The File {fileName} is corrupt and won't be able to be converted.");

            return result;
        }

        [Obsolete("Use 'Rockstar.Snapmatic.Snap.Image.Save' to save the image. To obtain a valid Snap object, pass the file path to 'Rockstar.Snapmatic.Snapmatic.Load'.")]
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

        [Obsolete]
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
            catch (Exception)
            {
                return true;
            }
        }

        [Obsolete]
        private static ImageFormat GetFormat(Format format) => format switch
        {
            Format.Jpeg => ImageFormat.Jpeg,
            Format.Png => ImageFormat.Png,
            _ => throw new NotSupportedException()
        };
    }
}
