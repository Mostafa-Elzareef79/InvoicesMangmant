using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
 namespace ItRootsTask_Core.Interfaces
{
    public interface IEncryption
    {
        string DecryptString(string cipherText);
        Stream DecryptStream(Stream cipherStream);
        byte[] Encrypt(string plainText);
        CryptoStream EncryptStream(Stream responseStream);
        string EncryptString(string plainText);

    }
}
