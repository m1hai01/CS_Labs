using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2CS.Helpers
{
    internal class Converter
    {
        private static int NrOfBits { get; set; } = 8;

        // convert decimal number to a sequence of binary numbers (0, 1)
        public static List<int> DecimalToBinary(int decimalNumber)
        {
            var buffer = new List<int>();

            // transform a decimal number into a binary one, by continuously dividing it by 2 and taking the remainder,
           
            while (decimalNumber != 0)
            {
                var remainder = decimalNumber % 2;
                buffer.Add(remainder);
                decimalNumber /= 2;
            }
            // add to binary number deficient zeros to access an 8 bit 
            AddZeros(buffer);
            
            buffer.Reverse();

            return buffer;
        }

        
        public static int BinaryToDecimal(List<int> binaryNumber)
        {
            var decimalNumber = 0.0;
            
            //  calculations to change a binary -> decimal 
            binaryNumber.Reverse();

            // apply the formula of conversion binary to decimal
           
            for (int i = 0; i < binaryNumber.Count; i++)
            {
                decimalNumber += binaryNumber[i] * Math.Pow(2, i);
            }

            return (int)decimalNumber;
        }

        // convert plain text to binary 
        public static List<int> ConvertTextToBinary(string plainText)
        {
            var buffer = new List<int>();

            // iterate over the plain text -> ascii code -> binary
            
            for (var i = 0; i < plainText.Length; i++)
            {
                var letter = plainText[i];
                var convertedLetter = DecimalToBinary((int) letter);
                buffer.AddRange(convertedLetter);
            }

            return buffer;
        }

        // binary -> plain text
        public static string BinaryToText(List<int> binaryMessage)
        {
            var buffer = new StringBuilder();

            // iterate  binary message by taking each 8 bits
            //  binary number -> decimal 
            //  decimal number -ascii-> character 
            for (int i = 0; i < binaryMessage.Count; i += NrOfBits)
            {
                var binaryLetter = binaryMessage.Skip(i).Take(NrOfBits).ToList();
                var letter = BinaryToDecimal(binaryLetter);
                buffer.Append((char)letter);
            }

            return buffer.ToString();
        }

        // add deficit zeros to binary number
        private static void AddZeros(List<int> buffer)
        {
            while (buffer.Count < NrOfBits)
            {
                buffer.Add(0);
            }
        }
    }
}
