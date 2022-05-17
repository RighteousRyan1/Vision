using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Audio;

namespace Vision
{
    public abstract class Vision : Game
    {
        // For an entry point- at the least.
        static void Main() { }
        public Vision() : base()
        {
            Window.Title = "VisionFramework";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

        }
    }
}