using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Helpers;

namespace Lab2
{
     public class AesStreamCipher : ICipher
    {
        private List<int> _sessionKey { get; set; } = new(64);
        private List<int> _frameKey { get; set; } = new(22);
        public StringBuilder _streamKey { get; set; } = new();
        private Lfsr _LfsrNr1 { get; set; }
        private Lfsr _LfsrNr2 { get; set; }
        private Lfsr _LfsrNr3 { get; set; }
        private Random _rnd { get; set; } = new();

        
        public AesStreamCipher()
        {
            // initialize  registers 
            _LfsrNr1 = new Lfsr(19, 8, new List<int>() { 13, 16, 17, 18 });
            _LfsrNr2 = new Lfsr(22, 10, new List<int>() { 20, 21 });
            _LfsrNr3 = new Lfsr(23, 10, new List<int>() { 7, 20, 21, 22 });


            // generate session key
            for (int i = 0; i < _sessionKey.Capacity; i++)
            {
                _sessionKey.Add(_rnd.Next(0, 2));
            }

            // generate  frame key
            for (int i = 0; i < _frameKey.Capacity; i++)
            {
                _frameKey.Add(_rnd.Next(0, 2));
            }

            //1Cycle64
            // all three registers are clocked 64 times(xor operation)
            //The 64 - bit session key's key bits are sequentially XORed with each register's result
            _LfsrNr1.XOR(_sessionKey);
            _LfsrNr2.XOR(_sessionKey);
            _LfsrNr3.XOR(_sessionKey);

            //2Cycle22
            //The 22 repetitions  ignores incorrect timing
            // register result is  XORed with  frame key 
            _LfsrNr1.XOR(_frameKey);
            _LfsrNr2.XOR(_frameKey);
            _LfsrNr3.XOR(_frameKey);


            //3Cycle100
            // Registers are timed 100 times 
            for (int i = 0; i < 100; i++)
            {
               //Following the majority rule
               //The clocking bits of all three registers are used to determine the majority bit.
               int majorityBit = Majority();
                if (_LfsrNr1.Register[_LfsrNr1.ClockingBit] == majorityBit)
                {
                    _LfsrNr1.XOR();
                }
                if (_LfsrNr2.Register[_LfsrNr2.ClockingBit] == majorityBit)
                {
                    _LfsrNr2.XOR();
                }
                if (_LfsrNr3.Register[_LfsrNr3.ClockingBit] == majorityBit)
                {
                    _LfsrNr3.XOR();
                }
            }


            //4Cycle228
            //Majority bit rule is used to clock registers 228 times with irregular timing,
            for (int i = 0; i < 228; i++)
            {
                
                var firstLsfrBit = _LfsrNr1.LastBit;
                var secondLsfrBit = _LfsrNr2.LastBit;
                var thirdLsfrBit = _LfsrNr3.LastBit;
                
                var keyStreamBit = firstLsfrBit ^ secondLsfrBit ^ thirdLsfrBit;
                // add the secure for encryption and decryption
                _streamKey.Append(keyStreamBit);

                // execute same logic as at the third stage
                int majorityBit = Majority();
                if (_LfsrNr1.Register[_LfsrNr1.ClockingBit] == majorityBit)
                {
                    _LfsrNr1.XOR();
                }
                if (_LfsrNr2.Register[_LfsrNr2.ClockingBit] == majorityBit)
                {
                    _LfsrNr2.XOR();
                }
                if (_LfsrNr3.Register[_LfsrNr3.ClockingBit] == majorityBit)
                {
                    _LfsrNr3.XOR();
                }
            }
        }


        
        private int Majority()
        {
            // add to list the value which is located on the index equal with clocking bit of each register
            var clockingBits = new List<int>();
            clockingBits.Add(_LfsrNr1.Register[_LfsrNr1.ClockingBit]);
            clockingBits.Add(_LfsrNr2.Register[_LfsrNr2.ClockingBit]);
            clockingBits.Add(_LfsrNr3.Register[_LfsrNr3.ClockingBit]);
            
            var nrOfZeros = clockingBits.Count(bit => bit == 0);
            
            var nrOfOnes = clockingBits.Count(bit => bit == 1);

            
            if (nrOfZeros > nrOfOnes)
                return 0;
            return 1;
            
        }

        


        
        public string Encrypt(string message)
        {

            var encryptedBinaryMessage = new List<int>();
            int streamKeyIndex = 0;

            
            var binaryMessage = Converter.ConvertTextToBinary(message);

            // encrypt the binary message using 
            for (var i = 0; i < binaryMessage.Count; i++)
            {
                var letter = binaryMessage[i];
                // case when the length of binary message is greater than the length of secure key,
                // then start going through each bit of the key from the beginning
                if (streamKeyIndex >= _streamKey.Length)
                {
                    streamKeyIndex = 0;
                }

                // perform xor operation between binary message and secure key 
                var encryptedLetter = letter ^ int.Parse(_streamKey[streamKeyIndex++].ToString());
                // add encrypted bit
                encryptedBinaryMessage.Add(encryptedLetter);
            }

            // convert binary encrypted message into plain text
            var encryptedMessage = Converter.BinaryToText(encryptedBinaryMessage);

            return encryptedMessage;
        }

        
        public string Decrypt(string message)
        {
            var binaryMessageDecrypt = new List<int>();
            int streamKeyIndex = 0;

            
            var binaryMessage = Converter.ConvertTextToBinary(message);

            // decrypt the encrypted binary message using xor 
            for (var i = 0; i < binaryMessage.Count; i++)
            {
                var letter = binaryMessage[i];
                
                // start going through each bit of the key from the beginning
                if (streamKeyIndex >= _streamKey.Length)
                {
                    streamKeyIndex = 0;
                }

                // perform xor operation between encrypted binary message and secure key bits
                var symbolDecrypted = letter ^ int.Parse(_streamKey[streamKeyIndex++].ToString());
                // add decrypted bit
                binaryMessageDecrypt.Add(symbolDecrypted);
            }

            // convert binary decrypted message into plain text
            var encryptedMessage = Converter.BinaryToText(binaryMessageDecrypt);

            return encryptedMessage;
        }
    }
}
