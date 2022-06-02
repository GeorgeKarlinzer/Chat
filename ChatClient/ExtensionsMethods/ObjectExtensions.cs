using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChatClient
{
    public static class ObjectExtensions
    {
        public static byte[] ToBytes(this object obj)
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();

            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
}
