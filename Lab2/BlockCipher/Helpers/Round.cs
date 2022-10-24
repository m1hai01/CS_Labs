﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab2.BlockCipher.Helpers.Converter;

namespace Lab2.BlockCipher.Helpers
{
    public class Round
    {
        private static int[,] MatrixRearrangement =
        {
            {16, 7, 20, 21, 29, 12, 28, 17},
            {1, 15, 23, 26, 5, 18, 31, 10},
            {2, 8, 24, 14, 32, 27, 3, 9},
            {19, 13, 30, 6, 22, 11, 4, 25}
        };

        private static int[,] MatrixExpand =
        {
            {32, 1, 2, 3, 4, 5},
            {4, 5, 6, 7, 8, 9},
            {8, 9, 10, 11, 12, 13},
            {12, 13, 14, 15, 16, 17},
            {16, 17, 18, 19, 20, 21},
            {20, 21, 22, 23, 24, 25},
            {24, 25, 26, 27, 28, 29},
            {28, 29, 30, 31, 32, 1}
        };


        public string BlockDilate(string block)
        {
            return BitsReplace(block, MatrixExpand);
        }

        public string InvertBlock(string block)
        {
            return BitsReplace(block, MatrixRearrangement);
        }

        public string Xor(string block, string key)
        {
            var output = string.Empty;
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i] == key[i])
                    output += "0";
                else
                    output += "1";
                
            }

            return output;
        }
    }
}
