using System.Collections.Generic;

namespace Logic.Domain
{
    public class Cluster
    {
        public Point Core { get; set; }

        public List<Point> Points { get; set; }

        public Cluster()
        {
            Points = new List<Point>();
        }
    }
}
