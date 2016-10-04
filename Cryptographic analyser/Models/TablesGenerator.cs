using System;
using System.Collections.Generic;

namespace Cryptographic_analyser.Models
{
    public class TablesGenerator
    {
        public const string M2K1 = "E1";
        public const string M3K2 = "E2";

        public const int Size = 6;
        public List<List<string>> Table { get; set; } = new List<List<string>>();
        public List<double> K { get; } = new List<double>();
        public List<double> M { get; } = new List<double>();

        private Random Random { get; } = new Random();

        public void Generate()
        {
            int generated;
            do
            {
                generated = 0;
                Table = new List<List<string>>(Size);
                var rows = new List<List<string>>(Size);
                var columns = new List<List<string>>(Size);

                for (var i = 0; i < Size; i++)
                {
                    rows.Add(new List<string>(Size));
                    columns.Add(new List<string>(Size));
                    Table.Add(new List<string>(Size));
                    for (var j = 0; j < Size; j++)
                    {
                        rows[i].Add(string.Empty);
                        columns[i].Add(string.Empty);
                        Table[i].Add(string.Empty);
                    }
                }

                Table[0][1] = M2K1;
                rows[0][1] = M2K1;
                columns[1][0] = M2K1;
                Table[1][2] = M3K2;
                rows[1][2] = M3K2;
                columns[2][1] = M3K2;

                var globalBreak = false;
                for (var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        if (i == 0 && j == 1 || i == 1 && j == 2)
                            continue; 

                        string value;
                        var tries = 0;
                        do
                        {
                            value = $"E{Random.Next(1, Size + 1)}";
                            tries++;
                            if (tries == Size*5)
                            {
                                globalBreak = true;
                                break;
                            }
                        } while (rows[i].Contains(value) || columns[j].Contains(value));
                        rows[i][j] = value;
                        columns[j][i] = value;
                        Table[i][j] = value;
                        generated++;
                    }
                    if (globalBreak)
                        break;
                }
            } while (generated != Size * Size - 2);
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

