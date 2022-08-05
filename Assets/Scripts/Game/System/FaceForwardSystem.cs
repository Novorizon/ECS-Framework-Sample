using ECS;
using Unity.Mathematics;

namespace Game
{
    /// <summary>
    /// ���ý�ɫ��RotationΪFace����
    /// </summary>
    public class FaceForwardSystem : ComponentSystem<Rotation, FaceDirection>
    {
        protected override void OnUpdate(int index, Entity entity, Rotation roll, FaceDirection face)
        {
            roll.Value = quaternion.LookRotation(face.Value, math.up());
        }
    }
}
