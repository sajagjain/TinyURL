using System;

namespace UrlShortener.Domain
{
    public class TinyURLGenerator
    {
        private static readonly char[] AllowedCharactersList = new char[] { 
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
            'a', 'b', 'c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A', 'B', 'C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        public string Shorten(ulong id)
        {
            Span<char> shortCode = stackalloc char[20];
            int currentIdx = 0;
            while (id > 0)
            {
                shortCode[currentIdx] = AllowedCharactersList[id % 62];
                currentIdx++;
                id = id / 62;
            }

            return shortCode.ToString();
        }
    }
}
