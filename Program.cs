using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                try
                {
                    var data = (await Task.WhenAll(
                        GetTunerData(c0, $"http://{server}/tuners.html?page=tuner0", 0),
                        GetTunerData(c1, $"http://{server}/tuners.html?page=tuner1", 1),
                        GetTunerData(c2, $"http://{server}/tuners.html?page=tuner2", 2)
                    )).Where(i => i.Channel != "none").ToList();
                    await using var cx = new DataContext();
                    cx.Data.AddRange(data);
                    await cx.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: ${ex}");
                }
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
        static Regex _signalStrengthSplitter = new Regex(@"(\d*)% \(([-0-9.]*) dBmV\)");
        static Regex _signalQualitySplitter = new Regex(@"(\d*)% \(([0-9.]*) dB\)");
        static Regex _symbolQualitySplitter = new Regex(@"(\d*)%");

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

            var signalStrength = string.IsNullOrEmpty(strength) ? null : _signalStrengthSplitter.Match(strength);
            var signalQuality = string.IsNullOrEmpty(quality) ? null : _signalQualitySplitter.Match(quality);
            var symbolQuality = string.IsNullOrEmpty(symbol) ? null : _symbolQualitySplitter.Match(symbol);

            var signalStrengthPct = (signalStrength == null || !signalStrength.Success) ? (decimal?)null : decimal.Parse(signalStrength.Groups[1].Value);
            var signalStrengthDb = (signalStrength == null || !signalStrength.Success) ? (decimal?)null : decimal.Parse(signalStrength.Groups[2].Value);
            var signalQualityPct = (signalQuality == null || !signalQuality.Success) ? (decimal?)null : decimal.Parse(signalQuality.Groups[1].Value);
            var signalQualityDb = (signalQuality == null || !signalQuality.Success) ? (decimal?)null : decimal.Parse(signalQuality.Groups[2].Value);
            var symbolQualityPct = (symbolQuality == null || !symbolQuality.Success) ? (decimal?)null : decimal.Parse(symbolQuality.Groups[1].Value);

            var authEnum = ParseAuthorization(ref auth);
            var cciEnum = ParseProtection(ref cci);
            var pcrEnum = ParseLock(ref pcr);

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
                AuthorizationEnum = authEnum,
                CCIProtectionEnum = cciEnum,
                PCRLockEnum = pcrEnum,
                //SignalStrength = strength,
                //SignalQuality = quality,
                //SymbolQuality = symbol,
                StreamingRateRaw = rate,
                ResourceLock = rlock,
                SignalStrengthPct = signalStrengthPct,
                SignalStrengthDbm = signalStrengthDb,
                SignalQualityPct = signalQualityPct,
                SignalQualityDbm = signalQualityDb,
                SymbolQualityPct = symbolQualityPct,
            };
        }

        public static Authorization? ParseAuthorization(ref string authorization)
        {
            switch(authorization)
            {
                case "not-subscribed":
                    authorization = null;
                    return Authorization.NotSubscribed;
                case "subscribed":
                    authorization = null;
                    return Authorization.Subscribed;
                case "none":
                    authorization = null;
                    return Authorization.None;
                case "unspecified":
                    authorization = null;
                    return null;
            }
            return null;
        }

        public static Protection? ParseProtection(ref string cci)
        {
            switch (cci)
            {
                case "unrestricted":
                    cci = null;
                    return Protection.Unrestricted;
                case "protected-copynever":
                    cci = null;
                    return Protection.ProtectedCopyNever;
                case "protected-copyonce":
                    cci = null;
                    return Protection.ProtectedCopyOnce;
                case "none":
                    cci = null;
                    return Protection.None;
            }
            return null;
        }

        public static Lock? ParseLock(ref string pcr)
        {
            switch (pcr)
            {
                case "locked":
                    pcr = null;
                    return Lock.Locked;
                case "none":
                    pcr = null;
                    return Lock.None;
            }
            return null;
        }
    }
}
