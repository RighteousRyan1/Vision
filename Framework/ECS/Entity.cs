using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Vision.Framework.ECS;

public interface IVisionEntity {
    public T Get<T>() where T : IVisionComponent;
    public bool Has<T>() where T : IVisionComponent;
    public void Add<T>(T component) where T : IVisionComponent;
    public void AddSystem<T>(IVisionSystem system);
}

#nullable enable
public class Entity : IVisionEntity {
    private List<IVisionComponent> _entityComponents;
    private List<IVisionSystem> _entitySystems;

    public ulong Id { get; private set; }
    public ulong WorldId { get; private set; }

    public Entity(ulong id, ulong worldId) {
        _entityComponents = new List<IVisionComponent>();
        _entitySystems = new List<IVisionSystem>();
        Id = id;
        WorldId = worldId;
    }

    /// <summary>
    ///     Gets the first <see cref="IVisionComponent"/> that matches the given type
    /// </summary>
    /// <typeparam name="T">A Type which inherits from <see cref="IVisionComponent"/></typeparam>
    /// <returns></returns>
    public T Get<T>() where T : IVisionComponent {
        return (T)_entityComponents.First(x => x is T);
    }

    public bool Has<T>() where T : IVisionComponent {
        return _entityComponents.FirstOrDefault(x => x is T) != null;
    }

    public void Add<T>(T component) where T : IVisionComponent {
        _entityComponents.Add(component); // The value will be (normally) boxed.
    }

    public void AddSystem<T>(IVisionSystem system) {
        _entitySystems.Add(system);
        system.InitialiseSystem(this);
    }
}