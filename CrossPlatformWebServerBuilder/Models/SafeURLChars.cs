// Copyright (c) Evan Overman (https://github.com/an-prata)
// CrossPlatformWebServerBuilder (https://github.com/an-prata/CrossPlatformWebServerBuilder)

using System;
using System.Linq;

namespace CrossPlatformWebServerBuilder.Models
{
    public class SafeURLChars
    {
        private static readonly string[] _unsafeChars = 
        { 
            " ", 
            "\"", 
            "<", 
            ">", 
            "#", 
            "%", 
            "{", 
            "}", 
            "|", 
            "\\", 
            "^", 
            "~", 
            "[", 
            "]", 
            "`" 
        };

        private static readonly string[] _safeChars = 
        { 
            "%20", 
            "%22", 
            "%3c", 
            "%3e", 
            "%23", 
            "%25", 
            "%7b", 
            "%7d", 
            "%7c", 
            "%5c", 
            "%5e", 
            "%7e", 
            "%5b", 
            "%5d", 
            "60" 
        };

        public static string[] UnsafeChars => _unsafeChars;
        public static string[] SafeChars => _safeChars;

        public static bool IsUnsafeChar(char c)
        {
            for (int i = 0; i < _unsafeChars.Length; i++)
                if (Convert.ToString(c) == _unsafeChars[i]) return true;
            return false;
        }

        public static string MakeSafeChar(char c)
        {
            if (IsUnsafeChar(c))
                for (int i = 0; i < _unsafeChars.Length; i++)
                    if (Convert.ToString(c) == _unsafeChars[i]) return _safeChars[i];
            return Convert.ToString(c);
        }

        public static bool ContainsUnsafeChar(string s)
        {
            foreach (char c in s) 
                for (int i = 0; i < _unsafeChars.Length; i++) 
                    if (IsUnsafeChar(c)) return true;
            return false;
        }

        public static string MakeURLSafe(string unsafeURL)
		{
			if (ContainsUnsafeChar(unsafeURL))
			{
                string safeURL = "";
				foreach (char c in unsafeURL) safeURL += MakeSafeChar(c);
                return safeURL;
			}
			
			return unsafeURL;
		}
    }
}