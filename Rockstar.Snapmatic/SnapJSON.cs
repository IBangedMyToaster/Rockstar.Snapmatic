namespace Rockstar.Snapmatic
{
    public class SnapJSON
    {
        public SnapLocation loc { get; set; } = null!;
        public string area { get; set; } = null!;
        public long street { get; set; }
        public string nm { get; set; } = null!;
        public string rds { get; set; } = null!;
        public int scr { get; set; }
        public string sid { get; set; } = null!;
        public long crewid { get; set; }
        public string mid { get; set; } = null!;
        public string mode { get; set; } = null!;
        public bool meme { get; set; }
        public bool mug { get; set; }
        public long uid { get; set; }
        public SnapTime time { get; set; } = null!;
        public long creat { get; set; }
        public bool slf { get; set; }
        public bool drctr { get; set; }
    }
}
