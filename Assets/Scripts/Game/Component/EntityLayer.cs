using ECS;

namespace Game
{
    public enum EntityLayerMask
    {
        None = 0,
        Hero = 1,
        Player = 1 << 1, // ���
        Npc = 1 << 2,    // Npc
        Static = 1 << 3, // ��̬���� ���� �ϰ����

        Friend = 1 << 4, // �ѷ�
        Enemy = 1 << 5, // �з�
        Neutrality = 1 << 6, // ����

        Interactive = 1 << 10,  // �ɽ���
        Select = 1 << 11,  // ��ѡ��
        Attack = 1 << 12,  // ��
        Dialogue = 1 << 13,  // ��
        Pickup = 1 << 14,  // ��
        Follow = 1 << 15,  // ��
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
