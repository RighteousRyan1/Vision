﻿using FontStashSharp;
using Vision.Core.Exceptions;

namespace Vision.Framework.AssetSystem;

public struct AssetLoadingParameters {
    public float FontSize { get; private set; }

    public AssetLoadingParameters(float? fontSize) {
        if (!fontSize.HasValue)
            FontSize = 14;
        else
            FontSize = fontSize.Value;
    }
}

public class Asset<T> : IContentLoadable where T : class {
    public Asset(string name, string path, AssetRepository assetRepository) {
        _path = path;
        _name = name;
        _assetRepository = assetRepository;
    }

    /// <summary>
    ///     The asset itself.
    /// </summary>
    /// <exception cref="AssetNotFoundException">
    ///     Throws if the <see cref="Asset{T}"/> was not loaded in the <see cref="AssetRepository"/> that created it.
    /// </exception>
    public T Value => _assetRepository.Request<T>(_name);

    /// <summary>
    ///     The current state of the asset.
    /// </summary>
    public AssetState State =>
        _assetRepository.IsAssetLoadedByPath(_path) ? AssetState.Loaded : AssetState.Unloaded;

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

    /// <summary>
    ///     Requests to the <see cref="AssetRepository"/> to load the asset this <see cref="Asset{T}"/> represents into memory.
    /// </summary>
    /// <param name="loadingParameters">Custom parameters for the loading process (Custom font sizes, etc)</param>
    /// <returns>True if the asset was loaded successfully.</returns>
    public bool RequestLoad(AssetLoadingParameters loadingParameters = new()) {
        _assetRepository.LoadAsset<T>(_path, loadingParameters); // Load the asset into memory.
        return _assetRepository.IsAssetLoadedByPath(_path);
    }
    
    /// <summary>
    ///     Requests to the <see cref="AssetRepository"/> to unload the asset this <see cref="Asset{T}"/> represents out of memory.
    /// </summary>
    /// <returns>True if the asset was unload was successful.</returns>
    public bool RequestUnload() {
        _assetRepository.UnloadAsset<T>(_path);
        return !_assetRepository.IsAssetLoadedByPath(_path);
    }

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