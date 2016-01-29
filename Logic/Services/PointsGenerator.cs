using System;
using System.Collections.Generic;
using Logic.Domain;
using Logic.Services.Options;

namespace Logic.Services
{
    public class PointsGenerator
    {
        public PointsGeneratorOptions Options { get; set; }

        private Random rand;

        public PointsGenerator(PointsGeneratorOptions options)
        {
            Options = options;
        }

        public List<Point> GeneratePoints()
        {
            rand = new Random(DateTime.Now.Millisecond);

            var points = new List<Point>();
            for (var i = 0; i < Options.PointsCount; i++)
            {
                points.Add(new Point
                {
                    Cluster = null,
                    X = GenerateCoordinate(),
                    Y = GenerateCoordinate()
                });
            }

            return points;
        }

        private int GenerateCoordinate()
        {
            return rand.Next(Options.Min, Options.Max);
        }
    }
}
