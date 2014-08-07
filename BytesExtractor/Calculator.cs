using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BytesExtractor
{
    public class Calculator
    {
        public static string ByteArrayToHexString(byte[] byteArray, string pattern = "{0:X2} ")
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException("byteArray");
            }

            var sb = new StringBuilder();
            foreach (byte oneByte in byteArray)
            {
                sb.Append(string.Format(pattern, oneByte));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString">
        /// 16进制字符串
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] HexStringToByteArray(string hexString)
        {
            if (hexString == null)
            {
                throw new ArgumentNullException("hexString");
            }

            hexString = hexString.Replace(" ", string.Empty);
            hexString = hexString.Replace("\r", string.Empty);
            hexString = hexString.Replace("\n", string.Empty);
            if ((hexString.Length % 2) != 0)
            {
                throw new ArgumentException("Argument fromat error!", "hexString");
            }

            var returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return returnBytes;
        }

        public static int[] ReadPattern(string patternString)
        {
            var x = patternString.Split(new[] {',', '，', ' ', '　', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            return
                patternString.Split(new[] {',', '，', ' ', '　', '\r', '\n'})
                    .Where(item => !string.IsNullOrEmpty(item))
                    .Select(int.Parse)
                    .ToArray();
        }

        public static string Extract(byte[] bytes, int[] pattern)
        {
            var index = 0;
            var sb = new StringBuilder();
            foreach (var len in pattern)
            {
                var piece = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    if (index >= bytes.Length)
                    {
                        break;
                    }
                    piece[i] = bytes[index];
                    index++;
                }

                sb.AppendLine(Calculator.ByteArrayToHexString(piece));
            }

            if (index < bytes.Length - 1)
            {
                var rest = new byte[bytes.Length - index];
                Array.Copy(bytes, index, rest, 0, bytes.Length - index);
                sb.AppendLine();
                sb.AppendLine(Calculator.ByteArrayToHexString(rest));
            }

            return sb.ToString();
        }
    }
}
