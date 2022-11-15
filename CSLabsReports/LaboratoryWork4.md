# Hash functions and Digital Signatures.

### Course: Cryptography & Security
### Author: Mustuc Mihai

----
&ensp;&ensp;&ensp; Hashing is a technique used to compute a new representation of an existing value, message or any piece of text. The new representation is also commonly called a digest of the initial text, and it is a one way function meaning that it should be impossible to retrieve the initial content from the digest.

&ensp;&ensp;&ensp; Such a technique has the following usages:
  * Offering confidentiality when storing passwords,
  * Checking for integrity for some downloaded files or content,
  * Creation of digital signatures, which provides integrity and non-repudiation.

&ensp;&ensp;&ensp; In order to create digital signatures, the initial message or text needs to be hashed to get the digest. After that, the digest is to be encrypted using a public key encryption cipher. Having this, the obtained digital signature can be decrypted with the public key and the hash can be compared with an additional hash computed from the received message to check the integrity of it.

### SHA256

&ensp;&ensp;&ensp; The SHA-256 algorithm is one flavor of SHA-2 (Secure Hash Algorithm 2), which was created by the National Security Agency in 2001 as a successor to SHA-1. SHA-256 is a patented cryptographic hash function that outputs a value that is 256 bits long. SHA-256 is one of the most secure hashing functions on the market. The US government requires its agencies to protect certain sensitive information using SHA-256. While the exact details of how SHA-256 works are classified, we know that it is built with a Merkle-Damgård structure derived from a one-way compression function itself created with the Davies-Meyer structure from a specialized block cipher.

## Objectives:
1. Get familiar with the hashing techniques/algorithms.
2. Use an appropriate hashing algorithms to store passwords in a local DB.
    1. You can use already implemented algortihms from libraries provided for your language.
    2. The DB choise is up to you, but it can be something simple, like an in memory one.
3. Use an asymmetric cipher to implement a digital signature process for a user message.
    1. Take the user input message.
    2. Preprocess the message, if needed.
    3. Get a digest of it via hashing.
    4. Encrypt it with the chosen cipher.
    5. Perform a digital signature check by comparing the hash of the message with the decrypted one.

# Implementation 

&ensp;&ensp;&ensp; I utilized the SHA256 Class from.NET 6 in my fourth lab. Following the encryption of the password using RSA cipher implemented in LAB3, the data is added to the database. A password must be entered once more and hashed. After that, the password entered by the current user is pulled from a database, decrypted using the RSA cipher, and compared to the password already in use.

## Sha256 Encryption
```
public sealed class SHA256Encrypt : ISHA256Encrypt

    {
        public string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var secretBytes = Encoding.UTF8.GetBytes(password);
            var secretHash = sha256.ComputeHash(secretBytes);
            return Convert.ToHexString(secretHash);
        }
    }
}
```

## UserService
```
public class UserService
    {
        private readonly IArchiveUser _userArchive;
        private readonly IRSA _rsa;
        private readonly ISHA256Encrypt _sha256Encryptor;

        private Guid _userId;

        public UserService(IArchiveUser userRepository, IRSA rsa, ISHA256Encrypt sha256Encryptor)
        {
            _userArchive = userRepository;
            _rsa = rsa;
            _sha256Encryptor = sha256Encryptor;
        }

        public void UserAddDB()
        {
            Console.WriteLine("Login: ");
            var login = Console.ReadLine();
            Console.WriteLine("Password");
            var password = Console.ReadLine();

            password = _sha256Encryptor.HashPassword(password);

            var hexEncryptedPassword = _rsa.Encrypt(password);
            _userId = Guid.NewGuid();

            var user = new User
            {
                Login = login,
                Password = hexEncryptedPassword,
                UserId = _userId
            };

            _userArchive.UserInitialization(user);
        }

```


## Screenshots

Output

![image](https://cdn.discordapp.com/attachments/826165651306971166/1042173644270485594/image.png)


![image](https://cdn.discordapp.com/attachments/826165651306971166/1042173367240884444/unknown.png)

## Conclusions

I became acquainted with the SHA256 hashing method during that laboratory work.