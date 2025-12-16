namespace AdventOfCode.Year2025
{
    public class Day9
    {
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode\\AdventOfCode\\Year2025\\inputs\\day9.txt");
            string[] rows = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            long[][] flore = rows.Select(t => t.Split(",").Select(c => long.Parse(c)).ToArray()).ToArray();

            long maxSize = 0;
            for (long i = 0; i < flore.Length; i++)
            {
                for (long k = 0; k < flore.Length; k++)
                {
                    var lenght = (flore[k][0] - flore[i][0] + 1);
                    var height = (flore[k][1] - flore[i][1] + 1);
                    var size = lenght * height;
                    if (size > maxSize)
                    {
                        maxSize = size;
                    }
                }

            }
            return maxSize;
        }


        public Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode\\AdventOfCode\\Year2025\\inputs\\day9.txt");
            string[] rows = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            List<int[]> flore = rows.Select(t => t.Split(",").Select(c => int.Parse(c)).ToArray()).ToList();

            ((int x, int y) point1, (int x, int y) point2)[] borders = new ((int x, int y) point1, (int x, int y) point2)[flore.Count];


            int lastX = 0;
            int lastY = 0;
            bool isFirst = true;
            flore.Add(flore.First());

            int i = 0;
            foreach (var tile in flore)
            {
                if (isFirst)
                {
                    lastX = tile[0];
                    lastY = tile[1];
                    isFirst = false;

                }
                else
                {
                    var newX = tile[0];
                    var newY = tile[1];
                    if (newX == lastX)
                    {
                        if (newY < lastY)
                        {
                            borders[i++] = ((newX, newY), (lastX, lastY));
                        }
                        else
                        {
                            borders[i++] = ((lastX, lastY), (newX, newY));
                        }
                    }
                    else
                    {
                        if (newX < lastX)
                        {
                            borders[i++] = ((newX, newY), (lastX, lastY));
                        }
                        else
                        {
                            borders[i++] = ((lastX, lastY), (newX, newY));
                        }
                    }
                    lastX = newX;
                    lastY = newY;
                }
            }

            List<(int, int, int, int, long)> AllRectengle = new List<(int, int, int, int, long)>();
            foreach (var tile1 in flore)
            {
                foreach (var tile2 in flore)
                {
                    var lenght = Math.Abs(tile2[0] - tile1[0]) + 1;
                    var height = Math.Abs(tile2[1] - tile1[1]) + 1;
                    var size = lenght * height;
                    AllRectengle.Add((tile1[0], tile1[1], tile2[0], tile2[1], size));
                }
            }

            AllRectengle = AllRectengle.Distinct().OrderByDescending(x => x.Item5).ToList();

            foreach (var item in AllRectengle)
            {
                if (IsAValidRectangle((item.Item1, item.Item2), (item.Item3, item.Item4), borders))
                {
                    return item.Item5;
                }
            }
            throw new Exception();
        }

        public bool IsAValidRectangle((int x, int y) point1, (int x, int y) point2, ((int x, int y) point1, (int x, int y) point2)[] borders)
        {
            var bigestX = Math.Max(point1.x, point2.x);
            var bigestY = Math.Max(point1.y, point2.y);
            var smallestX = Math.Min(point1.x, point2.x);
            var smallestY = Math.Min(point1.y, point2.y);

            return GetAllBorderCollidingWithRectangle(borders, (smallestX + 1, smallestY + 1), (bigestX - 1, bigestY - 1)).Count == 0;
        }

        public List<((int x, int y) point1, (int x, int y) point2)> GetAllBorderCollidingWithRectangle(((int x, int y) point1, (int x, int y) point2)[] borders, (int x, int y) point1, (int x, int y) point2)
        {
            var returnValue = new List<((int x, int y) point1, (int x, int y) point2)>();

            foreach (var item in borders)
            {
                if (item.point1.y <= point2.y && item.point2.y >= point1.y)
                {
                    if (item.point1.x <= point2.x && item.point2.x >= point1.x)
                    {
                        returnValue.Add(item);
                    }
                }
            }
            return returnValue;
        }


    }
}