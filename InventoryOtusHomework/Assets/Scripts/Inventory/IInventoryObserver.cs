using System;

namespace ATG.OtusHW.Inventory
{
    public interface IInventoryObserver
    {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }
    
    public class HeroItemsEffectsController : IInventoryObserver
    {
        private Inventory _inventory;
        private Hero _hero;
        
        public void Construct(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
            
            inventory.OnItemAdded += OnItemAdded;
            inventory.OnItemRemoved += OnItemRemoved;
        }

        public void OnDispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }
        
        public void OnItemAdded(InventoryItem item)
        {
            if(HasEffect(item) == false) return;

            if (item.TryGetComponent(out HeroDamageEffectComponent damageEffect) == true)
            {
                _hero.damage += damageEffect.DamageEffect;
            }
        }

        public void OnItemRemoved(InventoryItem item)
        {
            if(HasEffect(item) == false) return;

            if (item.TryGetComponent(out HeroDamageEffectComponent damageEffect) == true)
            {
                _hero.damage -= damageEffect.DamageEffect;
            }
        }

        private bool HasEffect(InventoryItem item)
        {
            return (item.Flags & ItemFlags.Effectable) == ItemFlags.Effectable;
        }
    }
}