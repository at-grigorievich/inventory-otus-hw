using System;
using UnityEngine;

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
            return new StackableItemComponent() { Count = 1, MaxCount = MaxCount };
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
    
    [Serializable]
    public class HeroHitPointsEffectComponent : IItemComponent
    {
        public int HitPointsEffect = 2;
        
        public IItemComponent Clone()
        {
            return new HeroHitPointsEffectComponent()
            {
                HitPointsEffect = this.HitPointsEffect
            };
        }
    }
    
    [Serializable]
    public class HeroSpeedEffectComponent : IItemComponent
    {
        public int SpeedEffect = 2;
        
        public IItemComponent Clone()
        {
            return new HeroHitPointsEffectComponent()
            {
                HitPointsEffect = this.SpeedEffect
            };
        }
    }

    [Serializable]
    public class HeroEquipmentComponent : IItemComponent
    {
        public EquipType Tag;

        [SerializeReference] 
        public IItemComponent[] EquipEffects;
        
        public IItemComponent Clone()
        {
            var clonedEffects = new IItemComponent[EquipEffects.Length];
            for (int i = 0; i < EquipEffects.Length; i++)
            {
                clonedEffects[i] = EquipEffects[i].Clone();
            }

            return new HeroEquipmentComponent()
            {
                Tag = this.Tag,
                EquipEffects = clonedEffects
            };
        }
    }
}