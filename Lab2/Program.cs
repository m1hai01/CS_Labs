namespace LAB2CS;

public static class Program
{ 
    private static string? _input = "";
    public static StreamCipher streamCipher { get; set; } = new();

    public static void Main(string[] args)
    {
        while (true)
        {
            _input = Console.ReadLine();
            
            Print("A5/1 Stream", true);
            Print("A5/1 Stream", false);
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