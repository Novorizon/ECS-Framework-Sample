using ECS;
using System;

namespace Game
{
    [Serializable]
    public class Speed : IComponent
    {
        public float Value;
        //public AttributeParam Value;
        //public void AddBuff(BuffEffect type, float value)
        //{
        //    if (type == BuffEffect.Plus)
        //        Value.valuePlus += value;
        //    else if (type == BuffEffect.Multi)
        //        Value.valueMul += value;
        //}

        //public void RemoveBuff(BuffEffect type, float value)
        //{
        //    if (type == BuffEffect.Plus)
        //        Value.valuePlus -= value;
        //    else if (type == BuffEffect.Multi)
        //        Value.valueMul -= value;
        //}
    }
}
