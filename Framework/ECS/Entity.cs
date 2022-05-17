using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.Framework.ECS
{
    public class Entity
    {
        public readonly uint Id;

        public List<Component> Components;



        public Entity(uint id, uint worldId)
        {
            Components = new();

        }

        //public bool Has<T>() where T : struct
            //=> 
    }
}
