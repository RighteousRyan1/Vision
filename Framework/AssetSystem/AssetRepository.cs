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
    private object _synchronisationLock;
    private GraphicsDevice _device;
    private List<IContentLoadable> _assets;
    private Dictionary<string, object> _assetMap; // The map of loaded assets, Key = AssetPath | Value = Asset 
    private FontSystem _fs;

    public AssetRepository(GraphicsDevice graphicsDevice, FontSystem fontSystem) {
        _synchronisationLock = new();
        _assets = new();
        _assetMap = new();
        _device = graphicsDevice;
        _fs = fontSystem;
    }

    public AssetRepository(GraphicsDevice graphicsDevice, FontSystemSettings settings) : this(graphicsDevice,
        new FontSystem(settings)) { } // Call an already implemented ctor.

    public AssetRepository(GraphicsDevice graphicsDevice) : this(graphicsDevice, new FontSystem()) { }


    /// <summary>
    ///     Retrieves an asset from the loaded asset cache.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="assetName">The name of the asset.</param>
    /// <returns></returns>
    public T Request<T>(string assetName) where T : class {
        lock (_synchronisationLock) {
            // TODO: Consideration to change assetName to assetPath to avoid possible collisions?
            if (!IsAssetLoadedByName(assetName))
                throw new AssetNotFoundException($"Failed to find requested asset: {assetName}");

            return (T)_assetMap[(_assets.First(asset => asset.Name == assetName) as Asset<T>)!.Path];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="AssetNotFoundException"></exception>
    public void LoadAsset<T>(string assetPath, AssetLoadingParameters loadingParameters = new()) where T : class {
        lock (_synchronisationLock) {
            // TODO: Finish implementation
            if (string.IsNullOrEmpty(assetPath))
                throw new ArgumentNullException(nameof(assetPath));
            if (!File.Exists(assetPath))
                throw new AssetNotFoundException($"Unable to fetch non-existent asset: {assetPath}");
            if (_assets.Any(x => x.Path == assetPath))
                return;

            var tType = typeof(T);

            object? loadedAsset = null;

            if (tType.TypeHandle.Equals(typeof(Texture2D).TypeHandle)) {
                loadedAsset = LoadTexture2D(assetPath);
            }
            else if (tType.TypeHandle.Equals(typeof(SoundEffect).TypeHandle)) {
                loadedAsset = LoadSoundEffect(assetPath);
            }
            else if (tType.TypeHandle.Equals(typeof(Effect).TypeHandle)) {
                loadedAsset = LoadEffect(assetPath);
            }
            else if (tType.TypeHandle.Equals(typeof(DynamicSpriteFont).TypeHandle)) {
                loadedAsset = LoadFont(assetPath, loadingParameters.FontSize);
            }
            // we'll need our own FBX parser. Would be fun to have an obj importer too.
            /*else if (tType.TypeHandle.Equals(typeof(Model).TypeHandle)) {
                var result = Texture2D.FromFile(_device, assetPath);
                var asset = new Asset<Model>(assetPath, result) {
                    Name = cutName
                };
                _assets.Add(asset);
            }*/

            if (loadedAsset == null)
                throw new UnsupportedAssetException(
                    "The asset type that was requested to load is not supported by the AssetLoader.");

            // Final stage: Map the asset path to the asset, there cannot be the same path for a different asset, boom.
            // We have to opt for objects, love the flexibility.
            _assetMap.Add(assetPath, loadedAsset);
        }
    }

    private Texture2D LoadTexture2D(string texturePath) {
        lock (_synchronisationLock) {
            var result = Texture2D.FromFile(_device, texturePath);
            var asset = new Asset<Texture2D>(Path.GetFileNameWithoutExtension(texturePath), texturePath, this);
            _assets.Add(asset);
            return result;
        }
    }

    private Effect LoadEffect(string effectPath) {
        lock (_synchronisationLock) {
            var result = new Effect(_device, File.ReadAllBytes(effectPath));
            var asset = new Asset<Effect>(Path.GetFileNameWithoutExtension(effectPath), effectPath, this);
            _assets.Add(asset);
            return result;
        }
    }

    private SoundEffect LoadSoundEffect(string soundEffectPath) {
        lock (_synchronisationLock) {
            var result = SoundEffect.FromFile(soundEffectPath);
            var asset = new Asset<SoundEffect>(Path.GetFileNameWithoutExtension(soundEffectPath), soundEffectPath,
                this);
            _assets.Add(asset);
            return result;
        }
    }

    private DynamicSpriteFont LoadFont(string fontPath, float fontSize) {
        lock (_synchronisationLock) {
            _fs.AddFont(File.ReadAllBytes(fontPath));
            var result = _fs.GetFont(fontSize);
            var asset = new Asset<SpriteFontBase>(Path.GetFileNameWithoutExtension(fontPath), fontPath, this);
            _assets.Add(asset);
            return result;
        }
    }

    public bool IsAssetLoadedByPath(string assetPath) {
        lock (_synchronisationLock) {
            var asset = _assets.FirstOrDefault(asset => asset.Path == assetPath);
            return asset != null && _assetMap.ContainsKey(asset.Path);
        }
    }

    public bool IsAssetLoadedByName(string assetName) { // Less trustworthy check due to names possibly repeating...
        lock (_synchronisationLock) {
            var asset = _assets.FirstOrDefault(asset => asset.Name == assetName);
            // If the asset has no Asset<T> or there is no value, that probably means its not even loaded
            return asset != null && _assetMap.ContainsKey(asset.Path);
        }
    }

    public void UnloadAsset<T>(string assetPath) where T : class {
        lock (_synchronisationLock) {
            if (!_assetMap.TryGetValue(assetPath, out var asset))
                return; // Not found on asset map.
            var tHandle = typeof(T).TypeHandle;
            if (tHandle.Equals(typeof(Texture2D).TypeHandle)) {
                ((Texture2D)asset).Dispose();
            }
            else if (tHandle.Equals(typeof(SoundEffect).TypeHandle)) {
                ((SoundEffect)asset).Dispose();
            }
            else if (tHandle.Equals(typeof(Effect).TypeHandle)) {
                ((Effect)asset).Dispose();
            }
            else if (tHandle.Equals(typeof(DynamicSpriteFont).TypeHandle)) {
                /* ¦--------------------------------------------¦
                 * ¦ This type requires no apparent clean-up.   ¦
                 * ¦--------------------------------------------¦
                 */
            }

            _assetMap.Remove(assetPath, out asset); // Remove.
        }
    }
}