using ECS;
using System;
using Unity.Mathematics;

namespace Game
{
    [Serializable]
    public class MoveDirection : IComponent
    {
        public float3 Value;
    }

    [Serializable]
    public struct Toward : IComponent
    {
        public float3 Value;
    }
}
