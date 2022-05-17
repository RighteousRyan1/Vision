using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.Framework.AssetSystem
{
    public enum AssetState
    {
        Unloaded,
        Loaded,
        OnHold
    }
    public class Asset<T> : IContentLoadable where T : class
    {
        public T Value { get; set; }
        public string Name { get; set; }

        public AssetState State { get; private set; }

        public Asset(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public void Load(T value)
        {

        }
    }
}
