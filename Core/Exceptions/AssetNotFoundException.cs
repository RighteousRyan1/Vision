using System;

namespace Vision.Core.Exceptions;

[Serializable]
public class AssetNotFoundException : Exception {
	public AssetNotFoundException() { }
	public AssetNotFoundException(string message) : base(message) { }
	public AssetNotFoundException(string message, Exception inner) : base(message, inner) { }
	protected AssetNotFoundException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}