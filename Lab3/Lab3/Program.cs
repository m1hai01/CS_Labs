using System;
using System.Numerics;
using System.Text;
using CSLab3.Interfaces;

namespace CSLab3 
{
    internal class Program
    {


       
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.Unicode;
            string inputText = "MIHAI";

            Console.WriteLine($"Our message: {inputText}");
            IRSA rsa = new RSA();
            var encrypted = rsa.Encrypt(inputText);

            Console.Write("Encrypted message: ");
            for (int i = 0; i < encrypted.Length; i = i + 8)
            {
                var letter = encrypted.Substring(i, 8);
                Console.Write($"{letter} ");
            }

            Console.WriteLine();
            Console.WriteLine($"Decrypted inputText:{rsa.Decrypt(encrypted)}");
            
        }
    }
}