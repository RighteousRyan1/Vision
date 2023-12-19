namespace Vision.Framework.ECS;

public interface IVisionComponent { // Made for Generics.
    public object GetTag();    
}

public struct Component : IVisionComponent {
    private object _tag;
    public object GetTag() => _tag;
}

