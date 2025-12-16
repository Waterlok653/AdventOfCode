namespace AdventOfCode2025
{
    public class Day3
    {
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day3.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            int[] voltages = new int[inputs.Length];
            int pos = 0;
            foreach (string i in inputs)
            {
                int valueFirst = 0;
                int valueSecond = 0;

                var inputInArray = i.ToCharArray();
                int startindex = 0;
                for (int x = 0; x < 2; x++)
                {
                    for (int j = 9; j > 0; j--)
                    {
                        for (int k = startindex; k < ((x == 0) ? inputInArray.Length - 1 : inputInArray.Length); k++)
                        {
                            if (int.Parse(inputInArray[k].ToString()) == j)
                            {
                                if (x == 1)
                                {
                                    valueSecond = j;
                                    break;
                                }
                                else
                                {
                                    startindex = k + 1;
                                    valueFirst = j;
                                    break;
                                }
                            }
                        }
                        if ((valueFirst != 0 && x == 0) || (valueSecond != 0 && x == 1))
                        {
                            break;
                        }
                    }
                }

                voltages[pos] = int.Parse(valueFirst.ToString() + valueSecond);
                pos++;
            }
            return voltages.Sum();
        }


        public Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day3.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            long[] voltages = new long[inputs.Length];
            int currentVol = 0;
            foreach (string i in inputs)
            {
                string reslut = "";
                (int value, int index)[] orderedList = new (int value, int index)[i.Length];
                int pos = 0;
                foreach (char nb in i.ToCharArray())
                {
                    orderedList[pos] = (int.Parse(nb.ToString()), pos);
                    pos++;
                }
                orderedList = orderedList.OrderByDescending(x => x.value).ToArray();
                int lastSelectedValueIndex = -1;

                for (pos = 0; pos < 12;)
                {
                    foreach (var item in orderedList)
                    {
                        if (item.index < orderedList.Length - 11 + pos && item.index > lastSelectedValueIndex)
                        {
                            reslut += item.value;
                            lastSelectedValueIndex = item.index;
                            pos++;
                            break;
                        }
                    }
                }
                voltages[currentVol] = long.Parse(reslut);
                currentVol++;
            }
            return voltages.Sum();
        }

    }
}