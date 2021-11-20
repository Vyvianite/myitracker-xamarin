namespace MyITracker

open System;
open System.Text
open System.IO
open System.Security.Cryptography

module LemonCypher = //v1 2020-02-12
  //Hack This is unsecure.
  let private PasswordHash = "5f042e659d16ada62bd286483a753db6"
  let private SaltKey = "7721e347f38ca532afefa365095d648e"
  let private VIKey = "acc5eb9d0$0866e9"

  let encrypt (plainText : string) =
    let viKey = Encoding.ASCII.GetBytes(VIKey)
    let plainTextBytes = Encoding.UTF8.GetBytes(plainText)
    let keyBytes = (new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey))).GetBytes(256 / 8)
    use symmetricKey = new RijndaelManaged( Mode = CipherMode.CBC, Padding = PaddingMode.Zeros )
    use encryptor = symmetricKey.CreateEncryptor(keyBytes, viKey)
    use memoryStream = new MemoryStream()
    use cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
    cryptoStream.FlushFinalBlock()
    Convert.ToBase64String(memoryStream.ToArray())

  let decrypt encryptedText =
    let cipherTextBytes = Convert.FromBase64String(encryptedText)
    let keyBytes = (new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey))).GetBytes(256 / 8)
    use symmetricKey = new RijndaelManaged( Mode = CipherMode.CBC, Padding = PaddingMode.Zeros )
    use decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey))
    use cryptoStream = new CryptoStream(new MemoryStream(cipherTextBytes), decryptor, CryptoStreamMode.Read)
    let plainTextBytes = Array.zeroCreate cipherTextBytes.Length
    let decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
    Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd([|'\u0000'|])