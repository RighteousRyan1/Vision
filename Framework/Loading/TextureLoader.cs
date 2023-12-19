using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Vision.Framework.AssetSystem;

namespace Vision.Framework.Loading;

public class TextureLoader {
    private readonly GraphicsDevice _graphicsDevice;

    public TextureLoader(GraphicsDevice gfxDevice) {
        _graphicsDevice = gfxDevice;
    }

    /// <summary>
    ///     Loads an array of textures from a given directory directly from disk memory.
    /// </summary>
    /// <param name="directory">
    ///     The directory fed into the <see cref="Stream" /> to load the textures from.
    ///     <para>Do be aware that <see cref="BaseDirectory" /> is the root.</para>
    /// </param>
    /// <returns></returns>
    public IEnumerable<Texture2D> LoadTextures(AssetRepository repository, string directory) {
        IList<Texture2D> list = new List<Texture2D>();
        foreach (var file in Directory.GetFiles(directory))
            list.Add(Texture2D.FromFile(_graphicsDevice,
                Path.Combine(Globals.BaseDirectory, directory, file)));
        return list;
    }

    /// <summary>
    ///     Loads a single texture from a given directory.
    /// </summary>
    /// <param name="name">
    ///     The name of the asset.
    ///     <para>Do be aware that <see cref="BaseDirectory" /> is the root.</para>
    /// </param>
    /// <returns></returns>
    public Texture2D LoadTexture(AssetRepository repository, string name) {
        // Load texture then re-request if it was not loaded before.
        if (!repository.IsAssetLoadedByPath(Path.Combine(Globals.BaseDirectory, name)))
            repository.LoadAsset<Texture2D>(Path.Combine(Globals.BaseDirectory, name));

        return repository.Request<Texture2D>(name);
    }
}