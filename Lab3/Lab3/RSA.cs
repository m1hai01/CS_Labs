using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CSLab3.Helpers;
using CSLab3.Interfaces;
using static CSLab3.Helpers.Converter;
using static CSLab3.Helpers.Calculations;


namespace CSLab3
{
    public class RSA : IRSA
    {
       

        public  (BigInteger N, BigInteger D) PrivateKey;
        public  (BigInteger N, BigInteger E) PublicKey;


        public const uint P = 61; 
        public const uint Q = 53; 
        public const uint E = 17; 
        public BigInteger N;
        public BigInteger L;
        public BigInteger D;

        public RSA()
        { 
            SetUpKey();
            PrivateKey = SetUpPrivateKey();
            PublicKey = SetUpPublicKey();
        }

        BigInteger SetUpKey()
        {
            //1: N = PQ
            return N = P * Q;
        }



        (BigInteger, BigInteger) SetUpPublicKey()
        {
            return (N, E);
        }


        (BigInteger, BigInteger) SetUpPrivateKey()
        {
            //the smallest number that is a multiple of both of them.
            //2: LCM(P - 1, Q - 1)
            L = LCM(new BigInteger(P - 1), new BigInteger(Q - 1));
            //3: D = E^-1 (MOD L(N)),
            //D -> inverse of E MOD L(N)
            D = InverseModulo(E, L);
            return (N, D);
        }

        public string Encrypt(string message)
        {
            //character -> ASCII
            var intMessage = StringToInts(message);
            //encrypt 
            var intEncryptMessage = EncryptDecryptMessage(intMessage, (int)PublicKey.E);
            //HEX output
            return IntToHexList(intEncryptMessage);
        }

        public string Decrypt(string ciphertext)
        {
            //HEX -> INT
            var intEncryptMessage = HexToInts(ciphertext);
            //decrypt
            var intDecryptMessage = EncryptDecryptMessage(intEncryptMessage, (int)PrivateKey.D);
            //ASCHI - > string
            return IntsToString(intDecryptMessage);
        }

        private int[] EncryptDecryptMessage(int[] messageBytes, int key)
        {
            var result = new int[messageBytes.Length];

            for (int i = 0; i < messageBytes.Length; i++)
            {
                //C = M ^ E (MOD N),
                //M = message,
                //E = key
                // M ^ KEY 
                var encrypted = BigInteger.Pow(messageBytes[i], key);
                
                encrypted = encrypted % PublicKey.N;
                result[i] = (int)encrypted;
            }

            return result;
        }
    }
}
