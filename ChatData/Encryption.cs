using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ChatData
{
    public class Encryption
    {
        public const int saltByteLength = 32;

        public byte[] GenerateSalt(string username)
        {
            var bytes = Encoding.ASCII.GetBytes(username);

            var sha256 = SHA256.Create();

            var salt = sha256.ComputeHash(bytes);

            return salt;
        }

        public byte[] ComputeSaltedHash(string text, byte[] salt)
        {
            if (salt.Length != saltByteLength)
                throw new Exception("Wrong salt length");

            var sha256 = SHA256.Create();

            var hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(text));

            var bitHash = new BitArray(hash);
            var bitSalt = new BitArray(salt);

            var saltedBitHash = bitHash.Xor(bitSalt);
            var saltedHash = new byte[saltByteLength];

            saltedBitHash.CopyTo(saltedHash, 0);

            return saltedHash;
        }
    }
}