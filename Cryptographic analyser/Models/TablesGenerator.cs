using System;
using System.Collections.Generic;

namespace Cryptographic_analyser.Models
{
    public class TablesGenerator
    {
        public const string M2K1 = "e1";
        public const string M3K2 = "e2";

        public const int Size = 6;
        public List<List<string>> Table { get; set; } = new List<List<string>>();
        public List<double> K { get; set; } = new List<double>();
        public List<double> M { get; set; } = new List<double>();

        private Random Random { get; set; } = new Random();

        public void Generate()
        {
        againMisha:
            var rows = new List<List<string>>(Size);
            var columns = new List<List<string>>(Size);

            for (var i = 0; i < Size; i++)
            {
                rows.Add(new List<string>(Size));
                columns.Add(new List<string>(Size));
                for (var j = 0; j < Size; j++)
                {
                    rows[i].Add(string.Empty);
                    columns[i].Add(string.Empty);
                }
            }

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    string value;
                    var tries = 0;
                    do
                    {
                        value = $"e{Random.Next(1, Size + 1)}";
                        tries++;
                        if (tries == Size * 5)
                            goto againMisha;
                    } while (rows[i].Contains(value) || columns[j].Contains(value));
                    rows[i][j] = value;
                    columns[j][i] = value;
                }
            }

        }

        public void GenerateK()
        {
            var numbers = new List<int>(Size);
            var sum = 0;
            for (var i = 0; i < Size; i++)
            {
                numbers.Add(Random.Next(1, 100));
                sum += numbers[i];
            }

            for (var i = 0; i < Size; i++)
                K.Add((double)numbers[i] / sum);
        }

        public void GenerateM()
        {
            for (var i = 0; i < Size; i++)
                M.Add(1.0 / Size);
        }
    }
}
