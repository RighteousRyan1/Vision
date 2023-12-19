namespace Vision.Framework.AssetSystem;

public class Asset<T> : IContentLoadable where T : class {
    public Asset(string name, string path, T value) {
        Name = name;
        _path = path;
        Value = value;
        State = AssetState.Loaded;
    }

    public Asset(string name, string path) {
        Name = name;
        _path = path;
        Value = null;
        State = AssetState.Unloaded;
    }

    public T Value { get; private set; }

    public AssetState State { get; private set; }

    public string Name {
        get => _name;
        set => _name = value;
    }

    public string Path {
        get => _path;
        set => _path = value;
    }

    public void Load(T value) { }

    /// <summary>
    ///     The name of the asset.
    /// </summary>
    private string _name;

    /// <summary>
    ///     The path to the asset.
    /// </summary>
    private string _path;
}