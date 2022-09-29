# Header 

# Intro to Cryptography. Classical ciphers. Caesar cipher.

### Course: Cryptography & Security
### Author: Mustuc Mihai

----
&ensp;&ensp;&ensp;Cryptography consists a part of the science known as Cryptology. The other part is Cryptanalysis. There are a lot of different algorithms/mechanisms used in Cryptography, but in the scope of these laboratory works the students need to get familiar with some examples of each kind.

&ensp;&ensp;&ensp; Ciphers are arguably the corner stone of cryptography. In general, a cipher is simply just a set of steps (an algorithm) for performing both an encryption, and the corresponding decryption.

### Caesar cipher

&ensp;&ensp;&ensp;The Caesar Cipher technique is one of the earliest and simplest methods of encryption technique. It’s simply a type of substitution cipher, i.e., each letter of a given text is replaced by a letter with a fixed number of positions down the alphabet. For example with a shift of 1, A would be replaced by B, B would become C, and so on. The method is apparently named after Julius Caesar, who apparently used it to communicate with his officials. 

### Caesar cipher with permutation

&ensp;&ensp;&ensp;The Caesar Permutation Cipher is a type of mono-alphabetic permutation cipher where the letters of the alphabet are chaotically arranged.

### Vigenere cipher

&ensp;&ensp;&ensp;The Vigenère cipher can be thought of as several Caesar ciphers with different keys. The easiest way is to present the ciphers in the form of a table, for the English alphabet we will get 26 lines of the Caesar cipher, in each line the shift is one more than the previous one.

&ensp;&ensp;&ensp;Mathematically, the Vigenère cipher can be described by the following formulas:

$Encrypt(m_{n}) = (Q + m_{n} + k) mod Q,$

$Decrypt(c_{n}) = (Q + c_{n} - k) mod Q,$



where $m_{n}$ is the position of the plaintext character, k n is the character position of the encryption key, Q is the number of characters in the alphabet, $c_{n}$ is the position of the ciphertext character.

### Playfair cipher

&ensp;&ensp;&ensp;Playfair cipher is an encryption algorithm to encrypt or encode a message. It is the same as a traditional cipher. The only difference is that it encrypts a digraph (a pair of two letters) instead of a single letter.

&ensp;&ensp;&ensp;It initially creates a key-table of 5*5 matrix. The matrix contains alphabets that act as the key for encryption of the plaintext. Note that any alphabet should not be repeated. Another point to note that there are 26 alphabets and we have only 25 blocks to put a letter inside it. Therefore, one letter is excess so, a letter will be omitted (usually J) from the matrix. Nevertheless, the plaintext contains J, then J is replaced by I. It means treat I and J as the same letter, accordingly.

&ensp;&ensp;&ensp;Since Playfair cipher encrypts the message digraph by digraph. Therefore, the Playfair cipher is an example of a digraph substitution cipher.

<b>Playfair Cipher Rules:</b>
1. Create the Square(5x5) key.
2. Write the text in simple form:
- The same letter cannot be used to form a pair. Break the letter into single letters and follow it with a false letter.

- Add an additional fictitious letter to the alone letter if it is standing alone throughout the matching procedure.

3. Protect the plain-text data:

- If both letters are in the same column, choose the letter that comes after each (going back to the top if at the bottom).

- Take the letter to the right of each letter if both appear in the same row (going back to the leftmost if at the rightmost position).

- If neither of the aforementioned rules applies: Together, the two letters form a rectangle.


&ensp;&ensp;&ensp; The decryption:

1. Create the Square(5x5) key.
2. Unlock the message's encryption:
- Take the letter that is located above each letter if both letters are in the same column (going back to the bottom if at the top).

- Take the letter to the left of each letter if both appear in the same row (going back to the rightmost if at the leftmost position).

- If neither of the aforementioned conditions applies: Use the two letters to form a rectangle, placing them in the diagonal opposite corner.

## Objectives:
1. Get familiar with the basics of cryptography and classical ciphers.

2. Implement 4 types of the classical ciphers:
    - Caesar cipher with one key used for substitution (as explained above),
    - Caesar cipher with one key used for substitution, and a permutation of the alphabet,
    - Vigenere cipher,
    - Playfair cipher.

3. Structure the project in methods/classes/packages as neeeded.

## Implementation description

* Caesar cipher

Algorithm for Caesar Cipher:

Input: 

- A String of lower case letters, called Text.
- An Integer between 0-25 denoting the required shift.

Procedure: 

- Traverse the given text one character at a time .
- For each character, transform the given character as per the rule, depending on whether we’re encrypting or decrypting the text.
- Return the new string generated.

&ensp;&ensp;&ensp;A program that receives a Text (string) and Shift value( integer) and returns the encrypted text. 

&ensp;&ensp;&ensp;We write another function decrypt similar to encrypt, that’ll apply the given shift in the opposite direction to decrypt the original text

```
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
```

* Caesar cipher with alphabet permutation

&ensp;&ensp;&ensp; That cipher is basically the same as simple cipher, but we have one extra step: insert a word in the without any repeating characters,
then insert the rest of the alphabet, don't include any characters if they are already in the permutatedAlphabet. 

```
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

```

* Vigenere cipher

&ensp;&ensp;&ensp; First of all, we double the key until it is of the same size as our word.
Then we do it so that the duplicated key is equal in number of characters to our word.
And already based on the given formula: "final += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();" encryption and decryption will take place.

```
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
```

* Playfair cipher

&ensp;&ensp;&ensp; Regarding playfair cipher I made a class called PlayfairCipher that includes a dictionary that records the coordinates of the letters in the matrix, a 5x5 matrix, and a predetermined alphabet. The matrix *_letters* must first be initialized by adding the keyword before the other letters of the alphabet. A function called LettersSummate(string message) is used for it. Repeat the entire phrase while adding the letters.

Rules:

1. Skip the character in question if it is not a letter.

2. Verify a letter's inclusion in the dictionary of letters added to the matrix; if so, skip it.

3. Verify if the letter is "I" or "J," as they are identical in that cipher.

&ensp;&ensp;&ensp; The application is ready to encrypt and decode messages after the matrix has been constructed. To encrypt the text, *encryptMessage(char[] message)* should be called with the plaintext message free of whitespaces, dots, and commas, and in uppercase. I used the *ChangeString.DeleteWildSymbols(new string(message))* method, which employs a Regex, to remove whitespace and dots. 

&ensp;&ensp;&ensp; Now, if two characters next to one other are the same, I want to separate them. In order to do so, I go two characters by two, and if they are the same, I add a "X" to divide them and create different neighboring characters. If the message's length is unusual, I add another X at the conclusion of the message.

&ensp;&ensp;&ensp; I must now divide the message into two-character pairs and toss FOR each pair.
When that task is finished, it's time to swap out the characters. The method's name is ReplaceLetters(IEnumerable, string, pairs, iterations, int).

&ensp;&ensp;&ensp; Now in the ChangeCharacter(), I iterate every pair, Each letter in the pair's coordinates should be taken, and then it should be determined if it is on the sameRow, sameColumn, or neither.
SameColumn((int I int j) X, (int I int j) Y) replaces the current letter with the one below it. SameRow((int I int j) X, (int I int j) Y) moves the letter to the right of the current character in the matrix; if at the rightmost section, take the leftmost character. Take the first choice from the column if you are at the bottom. Last but not least, "rectangle((int I int j) X, (int I int j) Y)", In the matrix, two letters create a rectangle. The function shifts the character on the same row, but from the pair's other corner. Same for the second character. 

&ensp;&ensp;&ensp; The same ChangeCharacter() is used to decode a message, but with iterations set to 3, which implies that while the encryption principles remain the same, new locations are used to pinpoint the original characters.

```
public class PlayfairCipher
    {
        private string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private  char[,] _letters = new char[5, 5];
        
        
        // letters  used in initialization and their coordinates
        private  Dictionary<char, (int, int)> _usedLetters = new Dictionary<char, (int, int)>(); // letters  used in initialization and their coordinates
        private int i, j;

        public PlayfairCipher(string message)
        {
            //add the keyword's  letters in matrix
            LettersSummate(message.ToUpper());

            //then include the remaining letters of the alphabet
            LettersSummate(Alphabet); 
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < _letters.GetLength(0); i++)
            {
                for (int j = 0; j < _letters.GetLength(1); j++)
                {
                    Console.Write($"{_letters[i, j]} |");
                }

                Console.WriteLine();
            }
        }

        private void LettersSummate(string message)
        {
            foreach (var letter in message)
            {
                if (!char.IsLetter(letter))
                    continue;
                
                if (_usedLetters.ContainsKey(letter))
                    continue;

                if (letter == 'I' && _usedLetters.ContainsKey('J')) 
                    continue;

                if (letter == 'J' && _usedLetters.ContainsKey('I'))
                    continue;

                //addletter and its coordinates in the matrix
                _usedLetters.Add(letter, (i, j)); 
                _letters[i, j++] = letter; 
                                          
                //if outOfRange 
                if (j != 5) 
                    continue;
                i++;
                j = 0;
            }
        }

       


        public string encryptMessage(char[] message)
        {
            //delete wild
            var without = ChangeString.DeleteWildSymbols(new string(message));
            var modifiedText = new StringBuilder(without.ToUpper());

            // separate characters if are equal
            for (int  i = 1; i < modifiedText.Length; i+=2)
            {
                if (!modifiedText[i].Equals(modifiedText[i - 1]))
                {
                    i += 2;
                    continue;
                }

                // separate them with an X
                modifiedText.Insert(i, 'X');
            }

            //if don't have full pairs
            if (modifiedText.Length % 2 == 1) modifiedText.Append('X');

            // parts of two characters
            var pairs = Substrings(modifiedText.ToString());
            var encoded = ChangeCharacter(pairs, 0);

            return string.Join("", encoded);
        }

        private List<string> Substrings(string formattedText)
        {
            //devide in pairs
            IEnumerable<string> pairs = new List<string>();
            for (int k = 1; k < formattedText.Length; k += 2)
            {
                string pair = (formattedText[k - 1].ToString() + formattedText[k]).ToString();

                pairs = pairs.Append(pair);
            }

            return pairs.ToList();
        }

        public string decryptMessage(char[] message)
        {
            //parts of two characters
            var pairs = Substrings(new string(message));
            // 3 more iterations on SameRow and SameColumn
            var encoded = ChangeCharacter(pairs, 3);

            return string.Join("", encoded);
        }

        // list of modified pairs by rule PF
        private List<string> ChangeCharacter(IEnumerable<string> pairs, int iterations)
        {
            var final = new List<string>();
            foreach (var pair in pairs)
            {
                //coordinates 
                (int i, int j) Xlocation = _usedLetters[pair[0]];
                (int i, int j) Ylocation = _usedLetters[pair[1]];

                if (Xlocation.i == Ylocation.i)
                {
                    //out of range
                    Xlocation = new(Xlocation.i, (Xlocation.j + iterations) % 5);
                    Ylocation = new(Ylocation.i, (Ylocation.j + iterations) % 5);
                    final.Add(SameRow(Xlocation, Ylocation));
                }
                else if (Xlocation.j == Ylocation.j)
                {
                    Xlocation = new((Xlocation.i + iterations) % 5, Xlocation.j);
                    Ylocation = new((Ylocation.i + iterations) % 5, Ylocation.j);
                    final.Add(SameColumn(Xlocation, Ylocation));
                }
                else final.Add(Rectangle(Xlocation, Ylocation));
            }

            return final;
        }

        private string SameRow((int i, int j) Xlocation, (int i, int j) Ylocation)
        {
            //current = right after take left, use mod
            
            char[] pair =
            {
            _letters[Xlocation.i % 5, (Xlocation.j + 1) % 5],
            _letters[Ylocation.i % 5, (Ylocation.j + 1) % 5]
            };

            return new string(pair);
        }

        private string SameColumn((int i, int j) Xlocation, (int i, int j) Ylocation)
        {
            //current = bottom after top with mod
            char[] pair =
            {
            _letters[(Xlocation.i + 1) % 5, Xlocation.j],
            _letters[(Ylocation.i + 1) % 5, Ylocation.j]
            };

            return new string(pair);
        }

        private string Rectangle((int i, int j) Xlocation, (int i, int j) Ylocation)
        {

            char[] pair =
            {
            _letters[Xlocation.i, Ylocation.j],
            _letters[Ylocation.i, Xlocation.j]
            };

            return new string(pair);
        }

    }
```

```
public static class ChangeString
    {
        public static string DeleteWildSymbols(string input)
          {
            //delete wild symbols
            var r = new Regex("(?:[^a-z0-9]|(?<=['\"])s)",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
    }
```


## Screenshots



## Conclusions

In that laboratory work, I got familiar with basic cryptography and implemented four classical ciphers.

