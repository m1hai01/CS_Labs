using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSlab1
{
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
}
