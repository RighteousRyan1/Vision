using System;

namespace Vision.Framework.AssetSystem;

public class UnsupportedAssetException : Exception {
    public UnsupportedAssetException() { }
    public UnsupportedAssetException(string message) : base(message) { }
    public UnsupportedAssetException(string message, Exception inner) : base(message, inner) { }
}