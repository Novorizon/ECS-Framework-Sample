using Cinemachine;
using ECS;
using UnityEngine;

namespace Game
{
    public class RollSensitivity : IComponent
    {
        [Range(0.1f, 1f)]
        public float Value;
    }
    public class VirtualCamera : IComponent
    {
        public CinemachineVirtualCamera Value;
    }
}
