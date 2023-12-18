using System.Collections.Generic;

namespace Vision.Framework.ECS;

public interface IVisionSystem {
    public void InitialiseSystem(IVisionEntity entity);
}

public struct System : IVisionSystem {
    public void OnFrameRendered() { }
    public void OnPhysicsTick() { }
    public void InitialiseSystem(IVisionEntity entity) { }
}