namespace Rockstar.Snapmatic
{
    public class SnapJSON
    {
        public SnapLocation loc { get; set; }
        public string area { get; set; }
        public long street { get; set; }
        public string nm { get; set; }
        public string rds { get; set; }
        public int scr { get; set; }
        public string sid { get; set; }
        public long crewid { get; set; }
        public string mid { get; set; }
        public string mode { get; set; }
        public bool meme { get; set; }
        public bool mug { get; set; }
        public long uid { get; set; }
        public SnapTime time { get; set; }
        public long creat { get; set; }
        public bool slf { get; set; }
        public bool drctr { get; set; }

        public SnapLocation SnapLocation
        {
            get => default;
            set
            {
            }
        }

        public SnapTime SnapTime
        {
            get => default;
            set
            {
            }
        }
    }
}
