using Lab2;
using Lab2.BlockCipher;
public static class Program
{
    private static string? _input = "";
    public static AesStreamCipher streamCipher { get; set; } = new();

    public static void Main(string[] args)
    {
        //while (true)
        //{
        //    _input = Console.ReadLine();

        //    Print("A5/1 Stream", true);
        //    Print("A5/1 Stream", false);
        //}

        var key = "blockchp";
        var text = "Cryptography consists a part of the science known as Cryptology.";

        Console.WriteLine($"Message to be encrypted: {text}");
        var blockCipher = new DesBlockCipher(key);
        var blocks = blockCipher.Encrypt(text);
        foreach (var block in blocks)
        {
            Console.WriteLine(block + ' ');
        }

        Console.WriteLine();

        blocks = blockCipher.Decrypt(blocks);
        foreach (var t in blocks)
        {
            Console.WriteLine(t);
        }

    }

    private static void Print(string name, bool isEncryption)
    {
        var invalidChecker = false;
        var inputText = "";
        var encryptedText = "";

        string displayedText;
        if (isEncryption)
            displayedText = "encrypted";
        else
            displayedText = "decrypted";
        Console.WriteLine($" {name} Cipher '{displayedText}");

        while (true)
        {
            Console.Write($"{(!invalidChecker ? $"Texy {displayedText}: " : "Valid text (non-empty): ")}");
            inputText = Console.ReadLine();
            invalidChecker = true;
            if (!string.IsNullOrEmpty(inputText))
            {
                break;
            }
        }

        if (isEncryption)
            encryptedText = streamCipher.Encrypt(inputText);
        else
            encryptedText = streamCipher.Decrypt(inputText);

        Console.WriteLine($"{displayedText.ToUpper()} message: {encryptedText}\n");
    }
}