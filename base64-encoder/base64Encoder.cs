using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Assignment2
{
    static class Base64Encoder
    {
        private static readonly Dictionary<uint, char> BASE64_ASCII_TABLE = new Dictionary<uint, char>()
        {
            { 0, 'A' }, { 16, 'Q' }, { 32, 'g' }, { 48, 'w' },
            { 1, 'B' }, { 17, 'R' }, { 33, 'h' }, { 49, 'x' },
            { 2, 'C' }, { 18, 'S' }, { 34, 'i' }, { 50, 'y' },
            { 3, 'D' }, { 19, 'T' }, { 35, 'j' }, { 51, 'z' },
            { 4, 'E' }, { 20, 'U' }, { 36, 'k' }, { 52, '0' },
            { 5, 'F' }, { 21, 'V' }, { 37, 'l' }, { 53, '1' },
            { 6, 'G' }, { 22, 'W' }, { 38, 'm' }, { 54, '2' },
            { 7, 'H' }, { 23, 'X' }, { 39, 'n' }, { 55, '3' },
            { 8, 'I' }, { 24, 'Y' }, { 40, 'o' }, { 56, '4' },
            { 9, 'J' }, { 25, 'Z' }, { 41, 'p' }, { 57, '5' },
            { 10, 'K' }, { 26, 'a' }, { 42, 'q' }, { 58, '6' },
            { 11, 'L' }, { 27, 'b' }, { 43, 'r' }, { 59, '7' },
            { 12, 'M' }, { 28, 'c' }, { 44,  's' }, { 60, '8' },
            { 13, 'N' }, { 29, 'd' }, { 45, 't' }, { 61, '9' },
            { 14, 'O' }, { 30, 'e' }, { 46, 'u' }, { 62, '+' },
            { 15, 'P' }, { 31, 'f' }, { 47, 'v' }, { 63, '/' },
        };

        public static string encode(string data)
        {
            StringBuilder sb = new StringBuilder(4096);

            const int BUFFER_SIZE = 3;
            for (int i = 0; i + 2 < data.Length; i += 3)
            {
                char[] buffer = new char[BUFFER_SIZE] { data[i], data[i + 1], data[i + 2] };

                sb.Append(encodeBuffer(buffer, 4));
            }

            char[] lastBuffer = new char[BUFFER_SIZE] { '0', '0', '0' };
            switch (data.Length % BUFFER_SIZE)
            {
                case 0:
                    break;
                case 1:
                    lastBuffer[0] = data[data.Length - 1];

                    sb.Append(encodeBuffer(lastBuffer, 2));
                    sb.Append('=');
                    sb.Append('=');
                    break;
                case 2:
                    lastBuffer[0] = data[data.Length - 2];
                    lastBuffer[1] = data[data.Length - 1];

                    sb.Append(encodeBuffer(lastBuffer, 3));
                    sb.Append('=');
                    break;
                default:
                    Debug.Assert(false, "invalid result");
                    break;
            }

            return sb.ToString();
        }

        private static string encodeBuffer(char[] buffer, int count)
        {
            StringBuilder sb = new StringBuilder(1024);

            string binary24bit = toBinary(buffer);
            for (int i = 0; i < 6 * count; i += 6)
            {
                string temp6bit = "";
                for (int j = 0; j < 6; ++j)
                {
                    temp6bit += binary24bit[j + i];
                }

                uint dec = toDecimal(temp6bit);
                sb.Append(BASE64_ASCII_TABLE[dec]);
            }

            return sb.ToString();
        }

        private static string toBinary(char[] buffer)
        {
            StringBuilder sb = new StringBuilder(1024);

            foreach (char ascii in buffer)
            {
                if (ascii == '0')
                {
                    sb.Append("00000000");
                }
                else
                {
                    string binary8bit = Convert.ToString(ascii, 2).PadLeft(8, '0');
                    sb.Append(binary8bit);
                }
            }

            return sb.ToString();
        }


        private static uint toDecimal(string binary)
        {
            uint num = 0;

            for (int i = binary.Length - 1; i > -1; --i)
            {
                if (binary[i] == '1')
                {
                    num += (uint)Math.Pow(2, binary.Length - 1 - i);
                }
            }

            return num;
        }
    }
}
