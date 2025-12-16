namespace AdventOfCode2025
{
    public class Day11Object
    {
        public string Name;
        public string LinkedToString;
        public List<Day11Object> LeadTo;
        public List<Day11Object> CommeFrom;
    }
    public class Day11
    {
        Dictionary<(Day11Object, bool fft, bool dac), Int128> DidVisite = new Dictionary<(Day11Object, bool fft, bool dac), Int128>();
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day11.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            List<Day11Object> allServer = new List<Day11Object>();
            foreach (var server in inputs)
            {
                Day11Object day11Object = new Day11Object();
                var parts = server.Split(":");
                day11Object.Name = parts[0].Trim();
                day11Object.LinkedToString = parts[1].Trim();
                day11Object.LeadTo = new List<Day11Object>();
                allServer.Add(day11Object);
            }
            foreach (var server in allServer)
            {
                foreach (var canConnectTo in allServer.Where(t => server.LinkedToString.Contains(t.Name)))
                {
                    server.LeadTo.Add(canConnectTo);
                }
            }

            var sum = FindPathToOutput(allServer.Where(t => t.Name.Equals("you")).First(), false, false, false);
            return sum;
        }

        private int FindPathToOutput(Day11Object current, bool NeedToVisitDACandFFT, bool didVisiteDac, bool didVisitFFT, string wentby = "")
        {
            int sum = 0;
            if (current.Name.Equals("fft"))
            {
                didVisitFFT = true;
            }
            if (current.Name.Equals("dac"))
            {
                didVisiteDac = true;
            }
            if ((!NeedToVisitDACandFFT && current.LinkedToString.Equals("out")) || (NeedToVisitDACandFFT && didVisiteDac && didVisitFFT && current.LinkedToString.Equals("out")))
            {
                return 1;
            }
            foreach (var canGoTo in current.LeadTo)
            {
                current = canGoTo;
                wentby += current.Name;
                wentby += ",";
                sum += FindPathToOutput(current, NeedToVisitDACandFFT, didVisiteDac, didVisitFFT, wentby);
            }

            return sum;
        }

        private Int128 FindPathToOutputVia(Day11Object current, bool didPassByFFT, bool didPassByDAC)
        {
            Int128 sum = 0;
            if (current.Name.Equals("fft"))
            {
                didPassByFFT = true;
            }
            if (current.Name.Equals("dac"))
            {
                didPassByDAC = true;
            }
            if (DidVisite.ContainsKey((current, didPassByFFT, didPassByDAC)))
            {
                return DidVisite[(current, didPassByFFT, didPassByDAC)];
            }
            if (current.LinkedToString.Equals("out") && didPassByFFT && didPassByDAC)
            {
                return 1;
            }
            foreach (var leadTo in current.LeadTo)
            {
                sum += FindPathToOutputVia(leadTo, didPassByFFT, didPassByDAC);
            }
            DidVisite.Add((current, didPassByFFT, didPassByDAC), sum);

            return sum;
        }

        public Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day11.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            List<Day11Object> allServer = new List<Day11Object>();
            foreach (var server in inputs)
            {
                Day11Object day11Object = new Day11Object();
                var parts = server.Split(":");
                day11Object.Name = parts[0].Trim();
                day11Object.LinkedToString = parts[1].Trim();
                day11Object.LeadTo = new List<Day11Object>();
                day11Object.CommeFrom = new List<Day11Object>();
                allServer.Add(day11Object);
            }
            foreach (var server in allServer)
            {
                foreach (var canConnectTo in allServer.Where(t => server.LinkedToString.Contains(t.Name)))
                {
                    server.LeadTo.Add(canConnectTo);
                    canConnectTo.CommeFrom.Add(server);
                }
            }
            var sum = FindPathToOutputVia(allServer.Where(t => t.Name.Equals("svr")).First(), false, false);

            return sum;
        }
    }
}
