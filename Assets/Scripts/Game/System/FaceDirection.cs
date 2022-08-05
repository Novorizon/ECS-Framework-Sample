using System;
using Unity.Mathematics;
using ECS;

namespace Game
{
    [Serializable]
    public class FaceDirection : IComponent
    {
        public float3 Value;
    }
}
