# Asymmetric Ciphers. RSA Cipher.

### Course: Cryptography & Security
### Author: Mustuc Mihai

----
&ensp;&ensp;&ensp; Asymmetric Cryptography (a.k.a. Public-Key Cryptography)deals with the encryption of plain text when having 2 keys, one being public and the other one private. The keys form a pair and despite being different they are related.

&ensp;&ensp;&ensp; As the name implies, the public key is available to the public but the private one is available only to the authenticated recipients. 

&ensp;&ensp;&ensp; A popular use case of the asymmetric encryption is in SSL/TLS certificates along side symmetric encryption mechanisms. It is necessary to use both types of encryption because asymmetric ciphers are computationally expensive, so these are usually used for the communication initiation and key exchange, or sometimes called handshake. The messages after that are encrypted with symmetric ciphers.

### RSA Cipher

&ensp;&ensp;&ensp;RSA algorithm is an asymmetric cryptography algorithm. Asymmetric actually means that it works on two different keys i.e. Public Key and Private Key. As the name describes that the Public Key is given to everyone and the Private key is kept private.

An example of asymmetric cryptography : 

&ensp;&ensp;&ensp;A client (for example browser) sends its public key to the server and requests some data.
The server encrypts the data using the client’s public key and sends the encrypted data.
The client receives this data and decrypts it.
Since this is asymmetric, nobody else except the browser can decrypt the data even if a third party has the public key of the browser.

&ensp;&ensp;&ensp;The idea! The idea of RSA is based on the fact that it is difficult to factorize a large integer. The public key consists of two numbers where one number is a multiplication of two large prime numbers. And private key is also derived from the same two prime numbers. So if somebody can factorize the large number, the private key is compromised. Therefore encryption strength totally lies on the key size and if we double or triple the key size, the strength of encryption increases exponentially. RSA keys can be typically 1024 or 2048 bits long, but experts believe that 1024-bit keys could be broken in the near future. But till now it seems to be an infeasible task.


## Objectives:
1. Get familiar with the asymmetric cryptography mechanisms.

2. Implement an example of an asymmetric cipher.

3. As in the previous task, please use a client class or test classes to showcase the execution of your programs.

## Implementation description

* RSA 

&ensp;&ensp;&ensp; **SetUpKey**. In that step public and private key are generated. We need two prime numbers _P_ and _Q_ which should be kept secret. 
Compute _N = PQ_. Then, Least Common Multiple should be computed, that variable will be used in private key. 

![image](https://cdn.discordapp.com/attachments/826165651306971166/1041342807236939777/image.png)

[GCD](#Gcd) is Greatest Common Divisor, at each iteration, a or b will hold the remainder of mod operation, 
other one will hold its predecessor and vice versa until remainder is 0. 

&ensp;&ensp;&ensp; Then, _d_ is calculated by applying [modular multiplicative inverse](#ModInverse) of E modulo L. To calculate it efficiently,
I used [Extended Euclidean algorithm](#EGcd), imagine we have sa + tb = gcd(a, b), it can be simplified to sa + tb = 1, because 
E and L are coprime. It is basically the Gcd algorithm with some additional temporary values to save values of _s_ and _t_. 
When remainder is 0, algorithm stops. Function returns both s and t, can be simplified to return only one value. When _s_ value is returned, it can be negative, so I 
add to it value of L until it is a positive number. That resulting number is _d_. Public key is a tuple of (N, E) and private key is a tuple of (N, D). 
  
&ensp;&ensp;&ensp; **Encryption and Decryption**.

![image](https://cdn.discordapp.com/attachments/826165651306971166/1041344150735429792/image.png)
```
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
```
&ensp;&ensp;&ensp; **Main formula of Encryption and Decryption**.
```
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
```


&ensp;&ensp;&ensp; **Generation of keys**.
```
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
```

&ensp;&ensp;&ensp; **Calcuations**
```
public class Calculations
    {
        //the smallest number that is a multiple of both of them.
        public static BigInteger LCM(BigInteger P, BigInteger Q)
        {
            var ab = P * Q;
            var gcd = GCD(P, Q);
            return ab / gcd;
        }

        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a = a % b;
                }
                else
                    b = b % a;
            }

            return a | b;
        }

        public static BigInteger InverseModulo(BigInteger left, BigInteger mod)
        {
            //returns a and b factors of ax + by = gcd(left, mod), only need a
            var egcd = FullGCD(left, mod);

            var result = egcd.a;

            if (result < 0)
                result += mod;

            return result % mod;
        }

        private static (BigInteger a, BigInteger b)
            FullGCD(BigInteger inverseOf, BigInteger mod)
        {
            //function returns both s and t, can be simplified to return only one value

            //sa + tb = gcd(a,b)
            //inverseOf (calculate inverse of input), mod (with corresponding modulo) 
            //q (quotient of mod / inverseOf), r (remainder of mod / inverseOf)

            //first row initial values
            BigInteger s1 = 0;
            BigInteger t1 = 1;
            BigInteger s2 = 1;
            BigInteger t2 = 0;

            while (inverseOf != 0)
            {
                var q = mod / inverseOf;
                var r = mod % inverseOf;

                var s3 = s1 - s2 * q;
                var t3 = t1 - t2 * q;

                mod = inverseOf; //as in euclidean algorithm, move the previous inverseOf to current mod
                inverseOf = r; //inverseOf will be the remainder of 
                s1 = s2; //s1 becomes s2 from previous row
                t1 = t2; //t1 becomes t2 from previous row 
                s2 = s3; //s2 becomes s3 from previous row
                t2 = t3; //t2 becomes t3 from previous row 
            }

            return (s1, t1);
        }
    }
```



## Screenshots

- RSA 

![image](https://cdn.discordapp.com/attachments/826165651306971166/1041347152733012039/image.png)

## Conclusions

In that laboratory work, I learned about asymmetric cryptograph methods and put an RSA cipher into practice..