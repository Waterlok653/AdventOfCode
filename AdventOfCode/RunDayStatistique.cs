namespace AdventOfCode
{
    public static class RunDayStatistique
    {
        public static void RunPart(string day, string part, Func<object?> action)
        {
            Console.WriteLine("+--------------------------------------------+");
            Console.WriteLine($"| {day} - {part} ");
            Console.WriteLine("+--------------------------------------------+");

            var start = DateTime.Now;
            object? result = action();
            var duration = DateTime.Now - start;

            // Format duration
            string formattedTime;
            if (duration.TotalSeconds < 1)
                formattedTime = $"{duration.TotalMilliseconds:F2} ms";
            else if (duration.TotalMinutes < 1)
                formattedTime = $"{duration.TotalSeconds:F2} s";
            else
                formattedTime = $"{duration.TotalMinutes:F2} min";
            formattedTime = $"{duration.TotalMilliseconds:F2} ms";

            Console.WriteLine($"Duration       : {formattedTime}");
            Console.WriteLine($"Result         : {result}");
            Console.WriteLine();
        }
    }
}