using Logic.Services;

namespace Logic.Domain
{
    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Cluster Cluster { get; set; }
    }
}
