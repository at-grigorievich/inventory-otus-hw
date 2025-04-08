using System;

namespace ATG.OtusHW.Inventory
{
    public interface IItemComponent
    {
        IItemComponent Clone();
    }
    
    [Serializable]
    public class StackableItemComponent: IItemComponent
    {
        public int Count;
        public int MaxCount;
        
        public IItemComponent Clone()
        {
            return new StackableItemComponent() { Count = Count, MaxCount = MaxCount };
        }
    }
    
    [Serializable]
    public class HeroDamageEffectComponent : IItemComponent
    {
        public int DamageEffect = 2;
        
        public IItemComponent Clone()
        {
            return new HeroDamageEffectComponent()
            {
                DamageEffect = this.DamageEffect
            };
        }
    }
}