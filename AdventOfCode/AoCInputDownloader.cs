using System.Net.Http;
using System.Threading.Tasks;

public static class AoCInputDownloader
{
    public static async Task<string> DownloadInputAsync(string year, int day)
    {
        string session = Environment.GetEnvironmentVariable("AOC_SESSION") ?? "";
        if (string.IsNullOrEmpty(session))
            throw new Exception("AOC_SESSION environment variable is missing.");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", $"session={session}");
        client.DefaultRequestHeaders.Add("User-Agent", "AoC Downloader");

        string url = $"https://adventofcode.com/{year}/day/{day}/input";
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to fetch input for Day {day}");

        return await response.Content.ReadAsStringAsync();
    }
}