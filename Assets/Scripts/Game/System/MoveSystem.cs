using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class MoveSystem : ComponentSystem<LocalToWorld, Velocity, MoveDirection, FaceDirection, Speed>
    {
        public readonly List<Vector3> paths = new List<Vector3>();
        protected override void OnUpdate(int index, Entity entity, LocalToWorld localToWorld, Velocity velocity, MoveDirection move, FaceDirection face, Speed speed)
        {
            //for (int i = 0; i < component2.pathPoints.Count - 1; i++)
            //{
            //    var mapProxy = Facade.RetrieveProxy(GridMapProxy.NAME) as GridMapProxy;
            //}
        }
    }
}
