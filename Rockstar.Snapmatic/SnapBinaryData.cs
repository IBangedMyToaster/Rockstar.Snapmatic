using Rockstar.Snapmatic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic
{
    internal class SnapBinaryData
    {
        private Int32? Position { get; set; }
        private Int32 Offset { get; set; }
        private Int32 Pointer { get; set; }
        internal string Title { get; private set; } = null!;
        internal Int32 Length { get; private set; }
        internal string Value { get; private set; } = null!;
        private BinaryReader Reader { get; set; }

        internal SnapBinaryData(BinaryReader reader, int offset, int? position = null)

        {
            this.Position = position;
            this.Offset = offset;
            this.Reader = reader;
            
            if(this.Position.HasValue)
                this.Reader.BaseStream.Position = this.Position.Value;

            this.Pointer = this.Offset + this.Reader.ReadInt32();
        }

        internal SnapBinaryData ReadData()
        {
            this.Reader.BaseStream.Position = this.Pointer;
            this.Title = this.Reader.ReadInt32AsString();
            this.Length = this.Reader.ReadInt32();
            this.Value = this.Reader.ReadBytesAsString(this.Length);

            return this;
        }
    }
}
