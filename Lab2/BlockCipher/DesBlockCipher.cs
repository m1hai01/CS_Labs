using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.BlockCipher.Helpers;
using static Lab2.BlockCipher.Helpers.Converter;

namespace Lab2.BlockCipher
{
    public class DesBlockCipher : IChiperr
    {
        private  InitialPermutation _initialPermutation = new ();
        private  Round _round = new();
        private  SBox _sBox = new();
        private  FinalPermutation _finalPermutation = new();
        private KeyCreate _keyCreate;

        public DesBlockCipher(string key)
        {
            _keyCreate = new KeyCreate(key);
        }

        public List<string> Encrypt(string plaintext, int encrypt = 0)
        {
            //split text into block of 8
            var blockText = plaintext.BreakeApart(8).ToList();

            
            if (blockText.Last().Length != 8)
            {
                //pkcs7
                var padLength = 8 - (blockText.Last().Length % 8);
                blockText[^1] = blockText[^1].PadRight(8, Convert.ToChar(padLength));
            }

            for (int i = 0; i < blockText.Count; i++)
            {
                //transform the block of 8 bytes into 64 bits
                blockText[i] = ToBinary(Encoding.UTF8.GetBytes(blockText[i]));
            }

            blockText = RunEncryption(blockText);
            return blockText;
        }

        public List<string> Decrypt(List<string> ciphertext)
        {
            for (int i = 0; i < ciphertext.Count; i++)
            {
                ciphertext[i] = HexStrToBinary(ciphertext[i]);
            }

            ciphertext = RunEncryption(ciphertext, 15);
            return ciphertext;
        }

        private List<string> RunEncryption(List<string> blockText, int encrypt = 0)
        {
            // encrypt/decrypt 8 bytes 
            var output = new List<string>();


            for (var i = 0; i < blockText.Count; i++)
            {
                var block = blockText[i];
                (string leftPlainText, string rightPlainText) = _initialPermutation.Permutation(block);
                var value = string.Empty;

                // 16 rounds
                for (int j = 0; j < 16; j++)
                {
                    // rightPlainText -> 48bit key
                    var rightPTex = _round.BlockDilate(rightPlainText);

                    //xor -> key
                    var xor = _round.Xor(rightPTex, _keyCreate.NrOfKey[Math.Abs(j - encrypt)]);

                    //SBox 
                    value = _sBox.Invert(xor);

                    //round permutation ?
                    var value2 = _round.InvertBlock(value);

                    //xor -> leftPlainText
                    xor = _round.Xor(leftPlainText, value2);

                    //LFT <=> RPT
                    leftPlainText = rightPlainText;
                    rightPlainText = xor;
                }

                output.Add(_finalPermutation.Transfer(rightPlainText + leftPlainText));
            }

            for (int i = 0; i < output.Count; i++)
            {
                output[i] = BinaryStrToHexStr(output[i]);
            }

            return output;
        }
    }
}
