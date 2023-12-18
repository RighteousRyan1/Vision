namespace Vision.Framework.ECS;

public interface IVisionComponent { // Made for Generics.
    public object GetTag();    
}

public struct Component : IVisionComponent {
    public object Tag;

    public object GetTag() => Tag;
}

