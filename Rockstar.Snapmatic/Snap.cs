using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Snapmatic
{
    public class Snap
    {
        public string FilePath { get; internal set; } = null!;
        public string FileName { get; internal set; } = null!;
        public string Title { get; internal set; } = null!;
        public string Description { get; internal set; } = null!;
        public string RawJSON { get; internal set; } = null!;
        public SnapJSON JSON { get; internal set; } = null!;
        public string ImageFormat { get; internal set; } = null!;
        public SnapImage Image { get; internal set; } = null!;
        public DateTime Creation { get; internal set; }
    }
}