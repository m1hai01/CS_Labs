using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSlab1
{
    public class CaesarCipher 
    {
        
        public  StringBuilder encryptMessage(string text, int s)
            {
                // The implementation of Cipher A.
                StringBuilder result1 = new StringBuilder();

                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsUpper(text[i]))
                    {
                        char ch = (char) (((int) text[i] +
                            s - 65) % 26 + 65);
                        result1.Append(ch);
                    }
                    else
                    {
                        char ch = (char) (((int) text[i] +
                            s - 97) % 26 + 97);
                        result1.Append(ch);
                    }
                }

                return result1;
            }

        public StringBuilder decryptMessage(string text, int s)
        {
            // The implementation of Cipher A.
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    char ch = (char)(((int)text[i] +
                        (26 - s) - 65) % 26 + 65);
                    result.Append(ch);
                }
                else
                {
                    char ch = (char)(((int)text[i] +
                        (26 - s) - 97) % 26 + 97);
                    result.Append(ch);
                }
            }

            return result;
        }

    }
}
