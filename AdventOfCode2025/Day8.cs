using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2025
{
    public class Day8Object
    {
        public long x;
        public long y;
        public long z;
        public long groupe;
        public List<Day8Object> connectedTo = new List<Day8Object>();
    }
    public static class Day8
    {
        public static void SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day8.txt");
            string[] row = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            Day8Object[] boxs = new Day8Object[row.Length];

            for (long i = 0; i < row.Length; i++)
            {
                var pos = row[i].Split(",").ToArray();
                boxs[i] = new Day8Object();
                boxs[i].x = long.Parse(pos[0]);
                boxs[i].y = long.Parse(pos[1]);
                boxs[i].z = long.Parse(pos[2]);
                boxs[i].groupe = 0;
                boxs[i].connectedTo = new List<Day8Object>();
            }
            List<(Day8Object, Day8Object, double)> allLinke = new List<(Day8Object, Day8Object, double)>();

            for (int i = 0; i < boxs.Length; i++)
            {
                for (int j = i + 1; j < boxs.Length; j++)
                {
                    allLinke.Add((boxs[i], boxs[j], (boxs[i].y - boxs[j].y) * (boxs[i].y - boxs[j].y) + (boxs[i].x - boxs[j].x) * (boxs[i].x - boxs[j].x) + (boxs[i].z - boxs[j].z) * (boxs[i].z - boxs[j].z)));
                }
            }
            List<(Day8Object, Day8Object, double)> linkToUse = allLinke.OrderBy(t => t.Item3).Take(1000).ToList();
            int currentGroupe = 1;
            foreach (var link in linkToUse)
            {
                //Merge Groupe
                var boxToChange = boxs.Where(b => b.groupe != 0 && (b.groupe == link.Item2.groupe || b.groupe == link.Item1.groupe)).ToArray();
                foreach (var item in boxToChange)
                {
                    item.groupe = currentGroupe;
                }
                link.Item1.groupe = currentGroupe;
                link.Item2.groupe = currentGroupe;

                currentGroupe++;
            }
            //size of every groupe
            List<Day8Object> allPoint = new List<Day8Object>();
            foreach (var item in linkToUse)
            {
                if (!allPoint.Contains(item.Item1))
                {
                    allPoint.Add(item.Item1);
                }
                if (!allPoint.Contains(item.Item2))
                {
                    allPoint.Add(item.Item2);
                }
            }
            List<long> sizeGroupe = new List<long>();
            for (long i = 1; i <= currentGroupe; i++)
            {
                sizeGroupe.Add(allPoint.Where(b => b.groupe == i).Count());
            }

            var top3 = sizeGroupe.OrderDescending().Take(3).ToArray();
            for (long i = 0; i < top3.Length; i++)
            {
                if (top3[i] == 0)
                {
                    top3[i] = 1;
                }
            }

            Console.WriteLine("The Top5: " + top3[0] * top3[1] * top3[2]);
        }


        public static void SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day8.txt");
            string[] row = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            Day8Object[] boxs = new Day8Object[row.Length];

            for (long i = 0; i < row.Length; i++)
            {
                var pos = row[i].Split(",").ToArray();
                boxs[i] = new Day8Object();
                boxs[i].x = long.Parse(pos[0]);
                boxs[i].y = long.Parse(pos[1]);
                boxs[i].z = long.Parse(pos[2]);
                boxs[i].groupe = 0;
                boxs[i].connectedTo = new List<Day8Object>();
            }
            List<(Day8Object, Day8Object, double)> allLinke = new List<(Day8Object, Day8Object, double)>();

            for (int i = 0; i < boxs.Length; i++)
            {
                for (int j = i + 1; j < boxs.Length; j++)
                {
                    allLinke.Add((boxs[i], boxs[j], (boxs[i].y - boxs[j].y) * (boxs[i].y - boxs[j].y) + (boxs[i].x - boxs[j].x) * (boxs[i].x - boxs[j].x) + (boxs[i].z - boxs[j].z) * (boxs[i].z - boxs[j].z)));
                }
            }
            List<(Day8Object, Day8Object, double)> linkToUse = allLinke.OrderBy(t => t.Item3).ToList();
            int currentGroupe = 1;
            foreach (var link in linkToUse)
            {
                //Merge Groupe
                var boxToChange = boxs.Where(b => b.groupe != 0 && (b.groupe == link.Item2.groupe || b.groupe == link.Item1.groupe)).ToArray();

                foreach (var item in boxToChange)
                {
                    item.groupe = currentGroupe;
                }
                link.Item1.groupe = currentGroupe;
                link.Item2.groupe = currentGroupe;

                if (boxs.GroupBy(b => b.groupe).Count() == 1)
                {
                    Console.WriteLine("the producte of last linke is " +link.Item1.x * link.Item2.x);
                    break;
                }

                currentGroupe++;
            }


        }
    }
}


