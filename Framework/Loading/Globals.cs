using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vision.Framework.Loading
{
    public static class Globals
    {
        /// <summary>The graphics device to use when loading assets.</summary>
        public static GraphicsDevice ActingGraphicsDevice { get; set; }
        /// <summary>The root to all content loading.</summary>
        public static string BaseDirectory { get; set; }
        internal static void ThrowIfGdNull()
        {
            if (ActingGraphicsDevice is null)
                throw new NoSuitableGraphicsDeviceException($"{nameof(ActingGraphicsDevice)} has not been assigned.");
        }
    }
}
