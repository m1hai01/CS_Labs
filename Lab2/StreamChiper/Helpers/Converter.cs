using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Helpers
{
    internal class Converter
    {
        private static int NrOfBits { get; set; } = 8;

        // convert decimal number to a sequence of binary numbers (0, 1)
        public static List<int> DecimalToBinary(int decimalNumber)
        {
            var buffer = new List<int>();

            // transform a decimal number into a binary one, by continuously dividing it by 2 and taking the remainder,
            // till the decimal number is equal with 0
            while (decimalNumber != 0)
            {
                var remainder = decimalNumber % 2;
                buffer.Add(remainder);
                decimalNumber /= 2;
            }
            // append to binary number insufficient zeros to obtain an 8 bit binary number
            AddZeros(buffer);
            // reverse the binary number
            buffer.Reverse();

            return buffer;
        }

        // convert binary number to decimal number
        public static int BinaryToDecimal(List<int> binaryNumber)
        {
            var decimalNumber = 0.0;
            // reverse binary number to make it easier
            // to perform the calculations to transform a binary number into a decimal one
            binaryNumber.Reverse();

            // apply the formula of conversion (binary -> decimal):
            // decimal([b7b6b5b4b3b2b1b0]) = b0 * 2^0 + b1 * 2^1 + b2 * 2^2 + b3 * 2^3 + ...
            for (int i = 0; i < binaryNumber.Count; i++)
            {
                decimalNumber += binaryNumber[i] * Math.Pow(2, i);
            }

            return (int)decimalNumber;
        }

        // convert plain text to binary number
        public static List<int> ConvertTextToBinary(string plainText)
        {
            var buffer = new List<int>();

            // iterate over the plain text by taking each letter, mapping its ascii code,
            // then converting ascii code into binary one
            for (var i = 0; i < plainText.Length; i++)
            {
                var letter = plainText[i];
                var convertedLetter = DecimalToBinary((int) letter);
                buffer.AddRange(convertedLetter);
            }

            return buffer;
        }

        // convert binary number to plain text
        public static string BinaryToText(List<int> binaryMessage)
        {
            var buffer = new StringBuilder();

            // iterate over the binary message by taking each 8 bits, converting binary number (8 bits) into decimal one,
            // then mapping decimal number to a character using ASCII table
            for (int i = 0; i < binaryMessage.Count; i += NrOfBits)
            {
                var binaryLetter = binaryMessage.Skip(i).Take(NrOfBits).ToList();
                var letter = BinaryToDecimal(binaryLetter);
                buffer.Append((char)letter);
            }

            return buffer.ToString();
        }

        // append deficit zeros to binary number
        private static void AddZeros(List<int> buffer)
        {
            while (buffer.Count < NrOfBits)
            {
                buffer.Add(0);
            }
        }
    }
}
