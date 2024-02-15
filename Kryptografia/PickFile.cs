using Kryptografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


Console.WriteLine("1. Encrypt File");
Console.WriteLine("2. Decrypt File");
Console.Write("Enter your choice (1 or 2): ");
string choice = Console.ReadLine();

switch (choice)
{
    case "1":
        EncryptFile();
        break;
    case "2":
        DecryptFile();
        break;
    default:
        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
        break;
}
        

static void EncryptFile()
{
    Console.Write("Enter the path of the file to encrypt: ");
    string filePath = Console.ReadLine();

    if (!File.Exists(filePath))
    {
        Console.WriteLine("File does not exist.");
        return;
    }

    byte[] key = GenerateRandomKey();
    byte[] iv = GenerateRandomIV();

    Console.WriteLine($"Generated Key: {Convert.ToBase64String(key)}");
    Console.WriteLine($"Generated IV: {Convert.ToBase64String(iv)}");

    Krypto.EncryptFile(filePath, key, iv);
}

static void DecryptFile()
{
    Console.Write("Enter the path of the encrypted file: ");
    string encryptedFilePath = Console.ReadLine();

    Console.Write("Enter the key: ");
    byte[] key = Convert.FromBase64String(Console.ReadLine());

    Console.Write("Enter the IV: ");
    byte[] iv = Convert.FromBase64String(Console.ReadLine());

    Krypto.DecryptFile(encryptedFilePath, key, iv);
}
static byte[] GenerateRandomKey()
{
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.GenerateKey();
        return aesAlg.Key;
    }
}
static byte[] GenerateRandomIV()
{
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.GenerateIV();
        return aesAlg.IV;
    }
}