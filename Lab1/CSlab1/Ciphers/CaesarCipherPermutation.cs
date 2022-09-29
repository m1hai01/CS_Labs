using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSlab1
{
    internal class CaesarCipherPermutation
    {
        private HashSet<char> unique = new HashSet<char>();
        

        public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string permutation = "CIPHER";
        private string secondAlphabet;

        

        public void ConcString()
        {
            StringBuilder finalWord = new StringBuilder();

            CreateNewAlphabet(finalWord, permutation);
            CreateNewAlphabet(finalWord, alphabet);

            secondAlphabet = finalWord.ToString();
            Console.WriteLine("Permutation alphabet:" + secondAlphabet);
        }

        private void CreateNewAlphabet(StringBuilder finalWord, string iterateThat)
        {
            foreach (var letter in iterateThat)
            {
                if (unique.Contains(letter)) continue;
                unique.Add(letter);
                finalWord.Append(letter);
            }
        }

         

        private string CodeEncode(string text, int k)
        {
            //add lower case 
            var fullAlfabet = secondAlphabet + secondAlphabet.ToLower();
            var letterr = fullAlfabet.Length;
            var final = "";
            for (int i = 0; i < text.Length; i++)
            {
                //current letter
                var c = text[i];
                //index of letter in secondAlphabet
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    //if don't find symbol
                    final += c.ToString();
                }
                else
                {
                    var codeIndex = (letterr + index + k) % letterr;
                    final += fullAlfabet[codeIndex];
                }
            }

            return final;
        }

        
        public string Encrypt(string plainMessage, int key)
            => CodeEncode(plainMessage, key);

        public string Decrypt(string encryptedMessage, int key)
            => CodeEncode(encryptedMessage, -key);
    }
}
