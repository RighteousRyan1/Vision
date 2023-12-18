using FontStashSharp;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vision.Core.Exceptions;
using Vision.Framework.Loading;

namespace Vision.Framework.AssetSystem;

public class AssetRepository {
    private GraphicsDevice _device;
    private List<IContentLoadable> _assets;
    private FontSystem _fs;
    public AssetRepository(GraphicsDevice graphicsDevice, FontSystemSettings settings = default) {
        _assets = new();
        _device = graphicsDevice;
        _fs = new(settings);
    }

    /// <summary>
    ///     Retrieves an asset from the loaded asset cache.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="assetName">The name of the asset.</param>
    /// <returns></returns>
    public T Request<T>(string assetName) where T : class {
        if (!IsAssetLoaded(assetName))
            throw new AssetNotFoundException($"Failed to find requested asset: {assetName}");
        return (_assets.First(asset => asset.Name == assetName) as Asset<T>)!.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="AssetNotFoundException"></exception>
    private void LoadAsset<T>(string assetPath) where T : Asset<T> {
        // TODO: Finish implementation
        if (string.IsNullOrEmpty(assetPath))
            throw new ArgumentNullException(nameof(assetPath));
        if (!File.Exists(assetPath))
            throw new AssetNotFoundException($"Unable to fetch non-existent asset: {assetPath}");
        var cutName = Path.GetFileNameWithoutExtension(assetPath);
        if (_assets.Any(x => x.Name == cutName))
            return;

        var tType = typeof(T);

        if (tType.TypeHandle.Equals(typeof(Texture2D).TypeHandle)) {
            var result = Texture2D.FromFile(_device, assetPath);
            var asset = new Asset<Texture2D>(assetPath, result) {
                Name = cutName
            };
            _assets.Add(asset);
        }
        else if (tType.TypeHandle.Equals(typeof(SoundEffect).TypeHandle)) {
            var result = SoundEffect.FromFile(assetPath);
            var asset = new Asset<SoundEffect>(assetPath, result) {
                Name = cutName
            };
            _assets.Add(asset);
        }
        else if (tType.TypeHandle.Equals(typeof(Effect).TypeHandle)) {
            var result = new Effect(_device, File.ReadAllBytes(assetPath));
            var asset = new Asset<Effect>(assetPath, result) {
                Name = cutName
            };
            _assets.Add(asset);
        }
        // we'll need our own FBX parser. Would be fun to have an obj importer too.
        /*else if (tType.TypeHandle.Equals(typeof(Model).TypeHandle)) {
            var result = Texture2D.FromFile(_device, assetPath);
            var asset = new Asset<Model>(assetPath, result) { 
                Name = cutName
            };
            _assets.Add(asset);
        }*/
    }

    public void LoadFont(string fontPath, float fontSize) {
        _fs.AddFont(File.ReadAllBytes(fontPath));
        var result = _fs.GetFont(fontSize);
            var asset = new Asset<SpriteFontBase>(fontPath, result);
        _assets.Add(asset);
    }

    public bool IsAssetLoaded(string assetName) {
        var exists = _assets.Any(asset => asset.Name == assetName);

        // maybe some other stuff here

        return exists;
    }
}