using BenchmarkDotNet.Attributes;
using UrlShortener.Domain;

namespace UrlShortener.Benchmarks
{
    [MemoryDiagnoser]
    public class TinyUrlBenchmark
    {
        private readonly char[] AllowedCharactersList = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A', 'B', 'C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        private readonly TinyURLGenerator Generator;

        public TinyUrlBenchmark()
        {
            Generator = new TinyURLGenerator();
        }

        [Benchmark]
        public void BaseMarker()
        {
            GetBase62EncodedString(23233);
        }

        [Benchmark]
        public void IntermediateMarker()
        {
            GetBase62EncodedStringUsingFixedLengthCharArray(23233);
        }

        [Benchmark]
        public void CurrentMarker()
        {
            Generator.Shorten(23233);
        }

        private string GetBase62EncodedString(ulong nextIdx)
        {
            string encodedString = string.Empty;
            while (nextIdx > 0)
            {
                ulong remainer = nextIdx % 62;
                encodedString = AllowedCharactersList[remainer] + encodedString;
                nextIdx = nextIdx / 62;
            }

            return encodedString;
        }

        private string GetBase62EncodedStringUsingFixedLengthCharArray(ulong nextIdx)
        {
            Span<char> encodedString = new char[20];
            int currentIdx = 0;
            ulong remainer;
            while (nextIdx > 0)
            {
                remainer = nextIdx % 62;
                encodedString[currentIdx] = AllowedCharactersList[remainer];
                currentIdx++;
                nextIdx = nextIdx / 62;
            }

            return encodedString.ToString();
        }

        private string GetBase62EncodedStringUsingFixedLengthCharArrayWithStackAlloc(ulong nextIdx)
        {
            Span<char> encodedString = stackalloc char[20];
            int currentIdx = 0;
            ulong remainer;
            while (nextIdx > 0)
            {
                remainer = nextIdx % 62;
                encodedString[currentIdx] = AllowedCharactersList[remainer];
                currentIdx++;
                nextIdx = nextIdx / 62;
            }

            return encodedString.ToString();
        }
    }
}
