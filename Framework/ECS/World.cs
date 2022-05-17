using System;
using System.Collections.Generic;

namespace Vision.Framework.ECS
{
    public class World
    {
        public static World[] Worlds = new World[8];

        public int Capacity;
        public string Name;
        public uint Id { get; private set; }

        public List<Entity> Entities;

        public World(int capacity)
        {
            Entities = new(capacity);

            for (uint i = 0; i < Worlds.Length; i++)
            {
                if (Worlds[i] is null)
                {
                    Id = i;
                    Worlds[i] = this;
                    return;
                }
            }

            Id = (uint)Worlds.Length;
            Array.Resize(ref Worlds, Worlds.Length * 2);
            Worlds[Id] = this;
        }

        //public bool Has<T>()
            //=> World.Worl
    }
}
