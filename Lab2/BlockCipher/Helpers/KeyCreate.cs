using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.BlockCipher.Helpers.Converter;

namespace Lab2.BlockCipher.Helpers
{
    public class KeyCreate
    {
        private string Key;
        public List<string> NrOfKey { get; private set; } = new();

        //56 bits , delete  8th bit
        private static int[,] MatrixOfKeyNr1 =
        {
            { 57, 49, 41, 33, 25, 17, 9 },
            { 1, 58, 50, 42, 34, 26, 18 },
            { 10, 2, 59, 51, 43, 35, 27 },
            { 19, 11, 3, 60, 52, 44, 36 },
            { 63, 55, 47, 39, 31, 23, 15 },
            { 7, 62, 54, 46, 38, 30, 22 },
            { 14, 6, 61, 53, 45, 37, 29 },
            { 21, 13, 5, 28, 20, 12, 4 }
        };

        //48 bits  to create a new key
        private static int[,] MatrixOfKeyNr2 =
        {
            { 14, 17, 11, 24, 1, 5, 3, 28 },
            { 15, 6, 21, 10, 23, 19, 12, 4},
            { 26, 8, 16, 7, 27, 20, 13, 2},
            { 41, 52, 31, 37, 47, 55, 30, 40},
            { 51, 45, 33, 48, 44, 49, 39, 56},
            { 34, 53, 46, 42, 50, 36, 29, 32}
        };

        // how many moves on key
        private static int[] MoveRounds = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        public KeyCreate(string key)
        {
            Key = key;
            KeysProduce();
        }

        private void KeysProduce()
        {
            // ToByteArray
            var binaryKey = ToBinary(Encoding.UTF8.GetBytes(Key));
            //remove  8th bit
            binaryKey = BitsReplace(binaryKey, MatrixOfKeyNr1);

            //divide key -> left and right
            (string left, string right) = (binaryKey[..28], binaryKey[28..]);

            //16 round
            for (int i = 0; i < 16; i++)
            {
                left = LeftMove(left, MoveRounds[i]);
                right = LeftMove(right, MoveRounds[i]);
                var key = left + right;
                var newKey = BitsReplace(key, MatrixOfKeyNr2);
                NrOfKey.Add(newKey);
            }
        }

        private string LeftMove(string key, int shift)
        {
            string partLeft = key[..shift];
            string partRight = key[shift..];
            return partRight + partLeft;
        }
    }
}
