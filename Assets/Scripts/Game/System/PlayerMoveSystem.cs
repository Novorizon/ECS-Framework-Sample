using ECS;
using UnityEngine;
namespace Game
{
    public class PlayerMoveSystem : ComponentSystem<Position, MoveDirection, Speed,PlayerController>
    {
        protected override void OnUpdate(int index, Entity entity, Position position, MoveDirection toward, Speed speed, PlayerController player)
        {
            var dt = Time.deltaTime;
            position.Value = position.Value + toward.Value * speed.Value * dt;
        }

    }
}