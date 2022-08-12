using ECS;

namespace Game
{
    public enum EntityLayerMask
    {
        None = 0,
        Hero = 1,
        Player = 1 << 1, // 玩家
        Npc = 1 << 2,    // Npc
        Static = 1 << 3, // 静态物体 建筑 障碍物等

        Friend = 1 << 4, // 友方
        Enemy = 1 << 5, // 敌方
        Neutrality = 1 << 6, // 中立

        Interactive = 1 << 10,  // 可交互
        Select = 1 << 11,  // 可选中
        Attack = 1 << 12,  // 可
        Dialogue = 1 << 13,  // 可
        Pickup = 1 << 14,  // 可
        Follow = 1 << 15,  // 可
    }
    public enum RelationshipLayer
    {
        None = 0,
        Friend = 1,
        Enemy = 2,
        Neutrality = 3,
    }

    public enum InteractiveLayer
    {
        Interactive = 0,
        Select = 1,
        Attack =2,
        Dialogue =3,
        Pickup = 4,
        Follow =5
    }
    public class EntityLayer : IComponent
    {
        public EntityLayerMask Value;

        public bool Has(EntityLayerMask mask)
        {
            return (Value & mask) != 0;
        }
    }


    public class Relationship : IComponent
    {
        public RelationshipLayer Value;
    }
}
