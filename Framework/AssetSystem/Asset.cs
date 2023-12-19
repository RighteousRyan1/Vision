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

    /// <summary>
    ///     The current state of the asset.
    /// </summary>
    public AssetState State { get; private set; }

    /// <summary>
    ///     The name of the asset.
    /// </summary>
    public string Name {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    ///     The path to the asset.
    /// </summary>
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

    /// <summary>
    ///     The <see cref="AssetRepository"/> which loaded this <see cref="Asset{T}"/>
    /// </summary>
    private AssetRepository _assetRepository;
}