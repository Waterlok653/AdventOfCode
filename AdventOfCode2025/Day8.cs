using System;
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
            long numberOfJoinTo = 1000;

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

            long nbGroupe = 1;
            int timeDone = 0;
            do
            {
                Day8Object closestOne = new Day8Object();
                Day8Object closestSeconde = new Day8Object();
                double distance = double.MaxValue;
                bool isSameGroupe = false;
                foreach (var box in boxs)
                {
                    foreach (var boxToConnect in boxs)
                    {
                        //if its the same or are alredy linked
                        if (box == boxToConnect || (box.connectedTo.Contains((boxToConnect))))
                        {
                            continue;
                        }
                        //get smalest distance
                        var currentDistance = Math.Sqrt((box.y - boxToConnect.y) * (box.y - boxToConnect.y) + (box.x - boxToConnect.x) * (box.x - boxToConnect.x) + (box.z - boxToConnect.z) * (box.z - boxToConnect.z));
                        if (distance > currentDistance)
                        {
                            distance = currentDistance;
                            closestOne = box;
                            closestSeconde = boxToConnect;
                            if (box.groupe != 0 && box.groupe == boxToConnect.groupe)
                            {
                                isSameGroupe = true;
                            }
                            else
                            {
                                isSameGroupe = false;
                            }
                        }

                    }
                }
                if (!isSameGroupe)
                {
                    //Merge Groupe
                    var boxToChange = boxs.Where(b => b.groupe != 0 && (b.groupe == closestSeconde.groupe || b.groupe == closestOne.groupe)).ToArray();
                    foreach (var item in boxToChange)
                    {
                        item.groupe = nbGroupe;
                    }
                    closestOne.groupe = nbGroupe;
                    closestSeconde.groupe = nbGroupe;

                    nbGroupe++;
                }
                //say that they are linked
                closestOne.connectedTo.Add(closestSeconde);
                closestSeconde.connectedTo.Add(closestOne);
                Console.WriteLine(timeDone);
                timeDone++;
            }
            while (timeDone < numberOfJoinTo);

            //size of every groupe
            List<long> sizeGroupe = new List<long>();
            for (long i = 1; i <= nbGroupe; i++)
            {
                sizeGroupe.Add(boxs.Where(b => b.groupe == i).Count());
            }

            var top3 = sizeGroupe.OrderDescending().Take(3).ToArray();
            for (long i = 0; i < top3.Length; i++)
            {
                if (top3[i] == 0)
                {
                    top3[i] = 1;
                }
            }

            //test
            /*foreach (var item in boxs.GroupBy(t => t.groupe))
            {
                Console.WriteLine(item.Key);
                foreach (var t in item)
                {
                    Console.Write(t.x + "," + t.y + "," + t.z + "|");
                }
                Console.WriteLine();
                Console.WriteLine();
            }*/

            Console.WriteLine("The Top5: " + top3[0] * top3[1] * top3[2]);

        }


        public static void SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day8.test.txt");
            string[] row = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

        }
    }
}


