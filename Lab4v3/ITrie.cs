using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4v3
{
    interface ITrie<TValue>
    {
    
        IEnumerable<TValue> Retrieve(string query);
        void Add(string key, TValue value);
    
    }
}
