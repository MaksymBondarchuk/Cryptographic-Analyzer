using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cryptographic_analyser.Models
{
    public class TablesGenerator
    {
        public const string M2K1 = "E1";
        public const string M3K2 = "E2";

        public int SizeM { get; set; } = 6;
        public int SizeK { get; set; } = 6;
        public int SizeE { get; set; } = 6;
        public List<List<string>> F { get; set; } = new List<List<string>>();
        public List<double> K { get; } = new List<double>();
        public List<double> M { get; } = new List<double>();

        public List<List<double>> Table4 { get; set; } = new List<List<double>>();
        public List<List<double>> Table5 { get; set; } = new List<List<double>>();
        public List<List<double>> Table6 { get; set; } = new List<List<double>>();

        private Random Random { get; } = new Random();

        public void GenerateF()
        {
            var eNumber = new List<int>();
            for (var i = 0; i < SizeE; i++)
                eNumber.Add(1);

            var diff = SizeK - SizeE;
            var idx = 0;
            while (0 < diff)
            {
                eNumber[idx++ % SizeE]++;
                diff--;
            }

            int generated;
            do
            {
                generated = 0;
                F = new List<List<string>>();
                var rows = new List<List<string>>();
                var columns = new List<List<string>>();

                for (var i = 0; i < SizeK; i++)
                {
                    F.Add(new List<string>());
                    rows.Add(new List<string>());
                    for (var j = 0; j < SizeM; j++)
                    {
                        rows[i].Add(string.Empty);
                        F[i].Add(string.Empty);
                    }
                }

                for (var i = 0; i < SizeM; i++)
                {
                    columns.Add(new List<string>());
                    for (var j = 0; j < SizeK; j++)
                        columns[i].Add(string.Empty);
                }

                F[0][1] = M2K1;
                rows[0][1] = M2K1;
                columns[1][0] = M2K1;
                F[1][2] = M3K2;
                rows[1][2] = M3K2;
                columns[2][1] = M3K2;

                var globalBreak = false;
                for (var k = 0; k < SizeK; k++)
                {
                    for (var m = 0; m < SizeM; m++)
                    {
                        if (k == 0 && m == 1 || k == 1 && m == 2)
                            continue;

                        string value;
                        int eIndex;
                        var tries = 0;
                        do
                        {
                            eIndex = Random.Next(SizeE);
                            value = $"E{eIndex + 1}";
                            tries++;
                            if (tries == SizeE * 5)
                            {
                                globalBreak = true;
                                break;
                            }
                        } while (rows[k].Contains(value) ||
                            eNumber[eIndex] <= columns[m].Count(t => t == value));
                        rows[k][m] = value;
                        columns[m][k] = value;
                        F[k][m] = value;
                        generated++;
                        if (k == SizeK - 1 && m == SizeM - 1)
                        {
                            var x = 0;
                        }
                    }
                    if (globalBreak)
                        break;
                }
            } while (generated != SizeK * SizeM - 2);
        }

        bool ChechF()
        {
            return false;
        }

        public void GenerateFv2()
        {
            do
            {
                var row = new List<double>();
            } while (true);
        }

        public void GenerateKorM(List<double> table, int size, bool isRandom)
        {
            table.Clear();
            if (isRandom)
            {
                for (var i = 0; i < size; i++)
                    table.Add(1.0 / size);
                table.Add(table.Sum());
            }
            else
            {
                var numbers = new List<int>(size);
                var sum = 0;
                for (var i = 0; i < size; i++)
                {
                    numbers.Add(Random.Next(1, 100));
                    sum += numbers[i];
                }

                for (var i = 0; i < size; i++)
                    table.Add((double)numbers[i] / sum);
                table.Add(table.Sum());
            }

        }

        private double Calculate112(int m, int e)
        {
            var result = .0;

            for (var k = 0; k < K.Count - 1; k++)
                if (F[k][m] == $"E{e + 1}")
                    result += K[k];

            return result;
        }

        private double Calculate113(int e)
        {
            var result = .0;
            for (var k = 0; k < SizeK; k++)
                for (var m = 0; m < SizeM; m++)
                    if (F[k][m] == $"E{e + 1}")
                        result += M[m] * K[k];
            return result;
        }

        public void GenerateTable4()
        {
            Table4.Clear();
            for (var e = 0; e < SizeE; e++)
            {
                Table4.Add(new List<double>());

                for (var m = 0; m < SizeM; m++)
                    Table4[e].Add(M[m] * Calculate112(m, e) / Calculate113(e));
                Table4[e].Add(Table4[e].Sum());
            }
        }

        public void GenerateTable5()
        {
            Table5.Clear();
            for (var e = 0; e < SizeE; e++)
            {
                Table5.Add(new List<double>());

                for (var m = 0; m < SizeM; m++)
                    Table5[e].Add(Table4[e][m] - M[m]);
                Table5[e].Add(Table5[e].Sum());
            }
        }

        private double Calculate116(int k, int e)
        {
            var result = .0;

            for (var m = 0; m < M.Count - 1; m++)
                if (F[k][m] == $"E{e + 1}")
                    result += M[m];

            return result;
        }

        public void GenerateTable6()
        {
            Table6.Clear();
            for (var k = 0; k < SizeK; k++)
            {
                Table6.Add(new List<double>());

                for (var e = 0; e < SizeE; e++)
                {
                    // Find message
                    var mIdx = F[k].IndexOf($"E{e + 1}");

                    Table6[k].Add(mIdx == -1 ? 0 : K[k] * Calculate116(k, e) / Calculate113(e) / M[mIdx]);
                }
                Table6[k].Add(Table6[k].Sum());
            }
        }
    }
}

