using ECS;
using System;
using Unity.Mathematics;

namespace Game
{
    [Serializable]
    public class Velocity : IComponent
    {
        public float3 Value;
    }
}
