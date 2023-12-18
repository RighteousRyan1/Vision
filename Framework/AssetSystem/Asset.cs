namespace Vision.Framework.AssetSystem;

public enum AssetState {
    Unloaded,
    Loaded,
    OnHold
}

public class Asset<T> : IContentLoadable where T : class {
    public Asset(string name, T value) {
        Name = name;
        Value = value;
    }

    public T Value { get; set; }

    public AssetState State { get; }
    public string Name { get; set; }

    public void Load(T value) { }
}