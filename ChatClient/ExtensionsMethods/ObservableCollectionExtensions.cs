using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal static class ObservableCollectionExtensions
    {
        public static void Fill<T>(this ObservableCollection<T> collection, IEnumerable<T> updatedCollection)
        {
            collection.Clear();
            foreach (var item in updatedCollection)
                collection.Add(item);
        }
    }
}
