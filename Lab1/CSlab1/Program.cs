// See https://aka.ms/new-console-template for more information

using CSlab1;
/*//1
CaesarCipher c = new CaesarCipher();
String text = Console.ReadLine();
int s = 3;
var encryptedMessage = c.encryptMessage(text, s);
Console.WriteLine("Text : " + text);
Console.WriteLine("Shift : " + s);
Console.WriteLine("Encrypt: " + encryptedMessage);
Console.WriteLine("Decrypt: " + c.decryptMessage(encryptedMessage.ToString(), s));*/


//2
CaesarCipherPermutation cc = new CaesarCipherPermutation();
cc.ConcString();
Console.Write("Text: ");
var message = Console.ReadLine();
Console.Write("Key: ");
var secretKey = Convert.ToInt32(Console.ReadLine());
var encryptedText = cc.Encrypt(message, secretKey);
Console.WriteLine("Encrypt: {0}", encryptedText);
Console.WriteLine("Decrypt: {0}", cc.Decrypt(encryptedText, secretKey));
Console.ReadLine();


/*//3
//передаем в конструктор класса буквы русского алфавита
var cipher = new VigenereCipher("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
Console.Write("Input text: ");
var inputText = Console.ReadLine().ToUpper();
Console.Write("key: ");
var password = Console.ReadLine().ToUpper();
var encryptedText1 = cipher.Encrypt(inputText, password);
Console.WriteLine("Encypt: {0}", encryptedText1);
Console.WriteLine("Decrypt: {0}", cipher.Decrypt(encryptedText1, password));
Console.ReadLine();*/

//4
var plaintext = "playfair".ToCharArray();

var keyword = "chiper";
var playfair = new PlayfairCipher(keyword);
playfair.PrintMatrix();

var encoded = playfair.encryptMessage(plaintext);
Console.WriteLine($"Encrypted message: {encoded}");
var decoded = playfair.decryptMessage(encoded.ToCharArray());
Console.WriteLine($"Decrypted message: {decoded}");