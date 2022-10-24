# Symmetric Ciphers. Stream Ciphers. Block Ciphers.

### Course: Cryptography & Security
### Author: Mustuc Mihai

----
## Overview
&ensp;&ensp;&ensp; Symmetric Cryptography deals with the encryption of plain text when having only one encryption key which needs to remain private. Based on the way the plain text is processed/encrypted there are 2 types of ciphers:
- Stream ciphers:
    - The encryption is done one byte at a time.
    - Stream ciphers use confusion to hide the plain text.
    - Make use of substitution techniques to modify the plain text.
    - The implementation is fairly complex.
    - The execution is fast.
- Block ciphers:
    - The encryption is done one block of plain text at a time.
    - Block ciphers use confusion and diffusion to hide the plain text.
    - Make use of transposition techniques to modify the plain text.
    - The implementation is simpler relative to the stream ciphers.
    - The execution is slow compared to the stream ciphers.


### A5/1 Stream cipher

&ensp;&ensp;&ensp; A5 is a family of symmetric stream ciphers most famously used as the encryption schemes in GSM 1 and succeeding technologies. The A5 algorithms are designed for simple commodity hardware with focus on security and speed. The short key length used in A5, along with other vulnerabilities, makes GSM prone to attacks.

### Data Encryption Standard (DES)

&ensp;&ensp;&ensp; Data encryption standard (DES) has been found vulnerable to very powerful attacks and therefore, the popularity of DES has been found slightly on the decline. DES is a block cipher and encrypts data in blocks of size of 64 bits each, which means 64 bits of plain text go as the input to DES, which produces 64 bits of ciphertext. The same algorithm and key are used for encryption and decryption, with minor differences. The key length is 56 bits. 

## Objectives:
1. Get familiar with the symmetric cryptography, stream and block ciphers.

2. Implement an example of a stream cipher.

3. Implement an example of a block cipher.

4. The implementation should, ideally follow the abstraction/contract/interface used in the previous laboratory work.

5. Please use packages/directories to logically split the files that you will have.

6. As in the previous task, please use a client class or test classes to showcase the execution of your programs.

## Implementation 

* A5/1 Stream cipher

&ensp;&ensp;&ensp; First of all,initialize the clocking bit, tapped bits, and register size in each of the three registers.
After that I generate keys.

```
// initialize  registers 
            _LfsrNr1 = new Lfsr(19, 8, new List<int>() { 13, 16, 17, 18 });
            _LfsrNr2 = new Lfsr(22, 10, new List<int>() { 20, 21 });
            _LfsrNr3 = new Lfsr(23, 10, new List<int>() { 7, 20, 21, 22 });
```
```
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
```

&ensp;&ensp;&ensp;Implementation of four cycles. 

&ensp;&ensp;&ensp;The xor method ignores incorrect clocking during the first cycle, which clocks all three registers 64 times. The 64-bit session key's key bits are successively xored while each register's feedback is being processed.

&ensp;&ensp;&ensp;All three registers are timed 22 times with a xor operation on the second cycle.
The 22-bit frame key's key bits are sequentially XORed with each register's response.

&ensp;&ensp;&ensp;Registers are timed 100 times on the third cycle using incorrect timing. The majority rule (Majority()) is used to irregular clocking. Based on the clocking bits of all three registers, the majority bit is calculated.

&ensp;&ensp;&ensp;Using the majority bit rule, registers are clocked 228 times in the fourth cycle, however each register's result is.

```
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
```
Majority bit.
```
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
```
&ensp;&ensp;&ensp;Encryption.
1. Create a series of binary integers from the supplied message.
2. Use xor operation between the secure key and the binary message to encrypt the binary message.
3. If the size of the secure key is longer than the size of the binary message, then start by traversing over each bit of the key sequentially.
4. Transform binary encrypted message into plain text.
```
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
```
&ensp;&ensp;&ensp;Decryption
1. Create a series of binary integers from the encrypted input message.
2. Using a xor operation between the encrypted binary message and secure key, decrypt the binary message.
3. If the length of the secure key is longer than the length of the binary message, then the key's bits are examined starting at the beginning.
4. Change the message's binary decoded form into plain text.
```
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
```

* Data Encryption Standard(DES)

&ensp;&ensp;&ensp;First, initial permutation is used to initialize DES. The block is divided into two parts of 32 bits after I replace the block's bits with those from the original permutation matrix. Following completion of Key Create using the desired secret key, the 56-bit key is divided into two separate 28-bit keys by applying "ReplaceBits" to every eighth bit of the key. After applying 16 rounds, 28-bit keys are now moved using a list called ShiftRounds that specifies how many shifts should be performed to each round key, and the bits of the key are changed using a different matrix.
I require a block of text that has been encrypted in order to apply 16 rounds. The initial permutation's right half is enlarged to a 48-bit key before being XORed with the key. The Sbox component divides the 48-bit key into eight blocks of six bits each.
We need obtain the Sbox row and column for every block. The first and final bits of a 6-bit are used to determine the row, whereas bits 1 through 5 are used to calculate the column.
After doing the Sbox component, flip the output, XOR it with the left plain text, and then switch the left plain text with the right plain text.  The left plain text output and the right plain text outcome are then subjected to one last permutation.



```
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
```

```
public (string, string) Permutation(string strBinary)
        {
            strBinary = BitsReplace(strBinary, Matrix);
            return (strBinary[..32], strBinary[32..]);
        }
```

```
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
```


## Screenshots



Output:
- A5/1 
![image](https://cdn.discordapp.com/attachments/826165651306971166/1031508791914668052/unknown.png)

- DES 
![image](https://cdn.discordapp.com/attachments/826165651306971166/1034149509376262294/unknown.png)
![image](https://cdn.discordapp.com/attachments/826165651306971166/1034203959302295582/unknown.png)
![image](https://cdn.discordapp.com/attachments/826165651306971166/1034204102235799624/unknown.png)

## Conclusions

In this laboratory work, I studied about stream cipher and block cipher.
