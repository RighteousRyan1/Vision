using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.Framework.AssetSystem
{
    public class AssetRepository
    {
        private IContentLoadable[] Assets { get; set; }

        /// <summary>
        /// Retrieves an asset from the loaded asset cache.
        /// </summary>
        /// <typeparam name="T">The type of content to load.</typeparam>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        public T Request<T>(string assetName) where T : class
        {
            return (Assets.First(asset => asset.Name == assetName) as Asset<T>).Value;
        }

        private void LoadAsset<T>(string assetName) where T : class
        {
            // TODO: Finish implementation
        }

        public bool AssetExists(string assetName)
        {
            bool exists = Assets.Any(asset => asset.Name == assetName);

            // maybe some other stuff here

            return exists;
        }
    }
}
