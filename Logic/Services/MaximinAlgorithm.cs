using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Domain;
using MoreLinq;

namespace Logic.Services
{
    public class MaximinAlgorithm
    {
        private Random rand;
        private List<Cluster> clusters;
        private bool isDone;

        public List<Cluster> Classify(List<Point> points)
        {
            isDone = false;

            var firstCluster = CreateCluster(GetRandomPoint(points));
            var secondCluster = CreateCluster(GetFarthestPoint(firstCluster.Core, points));

            clusters = new List<Cluster>
            {
               firstCluster,
               secondCluster
            };

            while (!isDone)
            {
                DistributeByClusters(points);
                var coreContender = ChooseCoreContender(points);
                if (ValidateCoreContender(coreContender))
                {
                    clusters.Add(CreateCluster(coreContender));
                }
                else
                {
                    isDone = true;
                }
            }
            

            return clusters;
        }

        private Point GetRandomPoint(List<Point> points)
        {
            rand = new Random(DateTime.Now.Millisecond);
            return points[rand.Next(0, points.Count - 1)];
        }

        private Cluster CreateCluster(Point point)
        {
            return new Cluster
            {
                Core = point
            };
        }

        private Point GetFarthestPoint(Point point, List<Point> points)
        {
            return points.MaxBy(p => GetDistance(p, point));
        }

        private int GetDistance(Point first, Point second)
        {
            return (first.X - second.X)*(first.X - second.X)
                + (first.Y - second.Y)*(first.Y - second.Y);
        }

        private void DistributeByClusters(List<Point> points)
        {
            foreach (var point in points)
            {
                var currentPoint = point;
                if (currentPoint.Cluster != null)
                {
                    currentPoint.Cluster.Points.Remove(currentPoint);
                }

                var currentCluster = clusters.MinBy(c => GetDistance(c.Core, currentPoint));
                currentCluster.Points.Add(currentPoint);
                currentPoint.Cluster = currentCluster;
            }
        }

        private Point ChooseCoreContender(List<Point> points)
        {
            return points.MaxBy(p => GetDistance(p.Cluster.Core, p));
        }

        private bool ValidateCoreContender(Point point)
        {
            var distance = GetDistance(point, point.Cluster.Core);
            var average = clusters.Average(c => GetDistance(c.Core, point));

            return distance > average / 2;
        }
    }
}
