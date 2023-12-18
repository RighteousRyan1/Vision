using Microsoft.Xna.Framework;

namespace Vision;

public abstract class Vision : Game {
    public Vision() {
        Window.Title = "VisionFramework";
    }

    // For an entry point- at the least.
    private static void Main() { }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void Update(GameTime gameTime) {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) { }
}