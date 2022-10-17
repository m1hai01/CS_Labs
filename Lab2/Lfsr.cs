using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2CS
{
    internal class Lfsr
    {
        public List<int> Register { get; set; }
        public int LastBit { get; set; }
        private int RegisterSize { get; set; }
        public int ClockingBit { get; set; }
        private List<int> TappedBits { get; set; }

        public Lfsr(int registerSize, int clockingBit, List<int> tappedBits)
        {
            RegisterSize = registerSize;
            ClockingBit = clockingBit;
            TappedBits = tappedBits;
            Register = new List<int>(new int[registerSize]);
        }

        // compute inner xor of tapped bits of the register
        private int XORInteriorResult()
        {
            // take the last tapped bit
            var resultInterior = Register[TappedBits[^1]];
            // take one by one starting with the second to last bit
            // and perform xor between resultInterior and current tapped bit
            for (int i = TappedBits.Count - 2; i >= 0; i--)
            {
                var intermediateBit = TappedBits[i];
                resultInterior ^= Register[intermediateBit];
            }

            return resultInterior;
        }

        // push new element into the register and shift all elements to the left
        private void ShiftPush(int newBit)
        {
            // shift all elements to the right by assigning to i the value of i-1 from register 
            for (int i = Register.Count - 1; i > 0; i--)
            {
                Register[i] = Register[i - 1];
            }
            // insert into the first position of register the new bit
            Register[0] = newBit;

            // save the value of last bit from register
            // for further computation of secure key at the forth stage (ForthCycle228())
            LastBit = Register[^1];
        }

        // perform xor:
        // case 1: if list is null ->
        // a simple xor of all tapped bits is performed with insertion of a new bit at the beginning
        // case 2: if list is not null -> 
        // a xor of all tapped bits and each bit from input is performed with insertion of a new bit at the beginning
        public void XOR(List<int>? input = null)
        {
            int resultInterior;

            if (input != null)
            {
                for (var i = 0; i < input.Count; i++)
                {
                    var bit = input[i];
                    resultInterior = XORInteriorResult();
                    var intermediateResult = resultInterior ^ bit;
                    ShiftPush(intermediateResult);
                }
            }
            else
            {
                resultInterior = XORInteriorResult();
                ShiftPush(resultInterior);
            }
        }

        // print the register
        private void PrintRegister()
        {
            for (var i = 0; i < Register.Count; i++)
            {
                var bit = Register[i];
                Console.Write($"{bit} ");
            }

            Console.WriteLine();
        }
    }
}
