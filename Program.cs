using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace HdHrMonitor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string server = args[0];

            using var c0 = new HttpClient();
            using var c1 = new HttpClient();
            using var c2 = new HttpClient();
            while (true)
            {
                var start = DateTimeOffset.UtcNow;
                var data = await Task.WhenAll(
                    GetTunerData(c0, $"http://{server}/tuners.html?page=tuner0", 0),
                    GetTunerData(c1, $"http://{server}/tuners.html?page=tuner1", 1),
                    GetTunerData(c2, $"http://{server}/tuners.html?page=tuner2", 2)
                );
                await using var cx = new DataContext();
                cx.Data.AddRange(data);
                await cx.SaveChangesAsync();
                var wait = TimeSpan.FromSeconds(5).Subtract(DateTimeOffset.UtcNow.Subtract(start));
                if (wait.TotalMilliseconds < 100)
                {
                    wait = TimeSpan.FromMilliseconds(100);
                }
                Console.WriteLine($"Waiting for {wait}");
                await Task.Delay(wait);
            }
        }

        static Regex _splitter = new Regex("<td>([^<]*)</td><td>([^<]*)</td>");

        private static async Task<Data> GetTunerData(HttpClient client, string url, int tuner)
        {
            var resp = await client.GetStringAsync(url);
            var results = _splitter.Matches(resp).ToDictionary(i => i.Groups[1].Value, i => i.Groups[2].Value);

            results.TryGetValue("Virtual Channel", out var vc);
            results.TryGetValue("Frequency", out var f);
            results.TryGetValue("Program Number", out var pn);
            results.TryGetValue("Authorization", out var auth);
            results.TryGetValue("CCI Protection", out var cci);
            results.TryGetValue("Modulation Lock", out var mod);
            results.TryGetValue("PCR Lock", out var pcr);
            results.TryGetValue("Signal Strength", out var strength);
            results.TryGetValue("Signal Quality", out var quality);
            results.TryGetValue("Symbol Quality", out var symbol);
            results.TryGetValue("Streaming Rate", out var rate);
            results.TryGetValue("Resource Lock", out var rlock);

            return new Data()
            {
                DateTimeUtc = DateTimeOffset.UtcNow,
                TunerNumber = tuner,
                Channel = vc,
                ChannelFrequency = f,
                ProgramNumber = pn,
                Authorization = auth,
                CCIProtection = cci,
                ModulationLock = mod,
                PCRLock = pcr,
                SignalStrength = strength,
                SignalQuality = quality,
                SymbolQuality = symbol,
                StreamingRateRaw = rate,
                ResourceLock = rlock,
            };
        }
    }
}
