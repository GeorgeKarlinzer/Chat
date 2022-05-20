using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ChatWCFService
{
    public class Encryption
    {
        public const int saltByteLength = 32;

        public byte[] GenerateSalt()
        {
            var salt = new byte[saltByteLength];
            var random = new Random();

            for (int i = 0; i < saltByteLength; i++)
            {
                salt[i] = (byte)random.Next(0, 256);
            }

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