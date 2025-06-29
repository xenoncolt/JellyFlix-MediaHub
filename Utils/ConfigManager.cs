using JellyFlix_MediaHub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JellyFlix_MediaHub.Utils
{
    internal class ConfigManager
    {
        private static readonly string AppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "JellyFlix_MediaHub");

        private static readonly string ConfigFilePath = Path.Combine(AppDataFolder, "config.json");
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("JF_SECURE_KEY_FOR_API_STORAGE_JELLFLIX_HUB_23");


        public static AppConfig LoadConfig()
        {
            try
            {
                if (!Directory.Exists(AppDataFolder) || !File.Exists(ConfigFilePath))
                {
                    return new AppConfig();
                }

                string encryptedJson = File.ReadAllText(ConfigFilePath);
                string decryptedJson = Decrypt(encryptedJson);
                return JsonConvert.DeserializeObject<AppConfig>(decryptedJson) ?? new AppConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                return new AppConfig();
            }
        }

        public static bool SaveConfig(AppConfig config)
        {
            try
            {
                if (!Directory.Exists(AppDataFolder))
                {
                    Directory.CreateDirectory(AppDataFolder);
                }

                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                string encryptedJson = Encrypt(json);
                File.WriteAllText(ConfigFilePath, encryptedJson);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving configuration: {ex.Message}");
                return false;
            }
        }

        private static string Encrypt(string plainText)
        {
            try
            {
                byte[] iv = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(iv);
                }

                using (Aes aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Write the IV first so Application or I can retrieve it later
                        memoryStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encryption error: {ex.Message}");
                return string.Empty;
            }
        }

        private static string Decrypt(string cipherText)
        {
            try
            {
                byte[] fullCipher = Convert.FromBase64String(cipherText);

                // Get the IV from the first 16 bytes
                byte[] iv = new byte[16];
                byte[] cipher = new byte[fullCipher.Length - 16];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(cipher))
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Decryption error: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
