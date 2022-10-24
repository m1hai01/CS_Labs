using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.BlockCipher.Helpers
{
    public class Converter
    {
        public static string ToBinary(byte[] data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static string BitsReplace(string binaryStr, int[,] matrix)
        {
            var output = string.Empty;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var index = matrix[i, j];
                    output += binaryStr[index - 1];
                }
            }

            return output;
        }

        public static string BinaryStrToHexStr(string binary)
        {
            if (string.IsNullOrEmpty(binary))
            {
                return binary;
            }

            var output = new StringBuilder(binary.Length / 8 + 1);

            var moduleOfLength = binary.Length % 8;
            if (moduleOfLength != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                var eightBits = binary.Substring(i, 8);
                output.Append($"{Convert.ToByte(eightBits, 2):X2}");
            }

            return output.ToString();
        }

        public static string HexStrToBinary(string hex)
        {
            return string.Join(string.Empty,
                hex.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
        }

        public static char Xor(char leftBit, char rightBit)
        {
            if (leftBit == rightBit)
                return '0';
            else
                return '1';
        }
    }
}
