using System.Security.Cryptography;
using System.Text;

namespace Kryptografia
{
    public class Krypto
    {
        public static void EncryptFile(string filePath, byte[] key, byte[] iv)
        {
            try
            {
                string outputFile = filePath + ".encrypted";

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(fsOutput, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (FileStream fsInput = new FileStream(filePath, FileMode.Open))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;

                                while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    csEncrypt.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("File encrypted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encryption failed: {ex.Message}");
            }
        }
        public static void DecryptFile(string encryptedFilePath, byte[] key, byte[] iv)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    string decryptedFilePath = encryptedFilePath.Replace(".encrypted", "_decrypted");

                    using (FileStream fsInput = new FileStream(encryptedFilePath, FileMode.Open))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(fsInput, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (FileStream fsOutput = new FileStream(decryptedFilePath, FileMode.Create))
                            {
                                byte[] buffer = new byte[4096];
                                int bytesRead;

                                while ((bytesRead = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    fsOutput.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }

                    Console.WriteLine("File decrypted successfully.");
                    Console.WriteLine($"Decrypted file saved at: {decryptedFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption failed: {ex.Message}");
            }
        }

    }
}