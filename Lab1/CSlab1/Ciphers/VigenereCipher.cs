using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSlab1
{
    public class VigenereCipher
    {
        const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        readonly string letters;

        public VigenereCipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? Alphabet : alphabet;
        }

        //repeating password generation
       

        private string Vigenere(string text, string password, bool encrypting = true)
        {
            //repeating password generation -> key duplication
            var p = password;
            while (p.Length < text.Length)
            {
                p += p;
            }
            
            //equal our word with key 
            var gamma = p.Substring(0, text.Length);

            var final = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = letters.IndexOf(text[i]);//index of our  character[i] in word from alphaber 
                var codeIndex = letters.IndexOf(gamma[i]);//index of our  key[i] in word from alphaber
                if (letterIndex < 0)
                {
                    //if the letter is not found, add it in its original form
                    final += text[i].ToString();
                }
                else
                {
                    final += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                }
            }

            return final;
        }

        public string Encrypt(string plainMessage, string password)
        {
            return Vigenere(plainMessage, password);
        }
             

        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}
