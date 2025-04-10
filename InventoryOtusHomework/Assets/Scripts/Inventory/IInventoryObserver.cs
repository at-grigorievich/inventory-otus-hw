using System;
using System.Collections.Generic;

namespace ATG.OtusHW.Inventory
{
    public interface IInventoryObserver
    {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }

    public interface IEquipmentObserver
    {
        void OnItemTakeOn(InventoryItem item);
        void OnItemTakeOff(InventoryItem item);
    }
    
    public class HeroItemsEffectsController : IInventoryObserver, IDisposable
    {
        private readonly Inventory _inventory;
        private readonly Hero _hero;

        public HeroItemsEffectsController(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
            
            inventory.OnItemAdded += OnItemAdded;
            inventory.OnItemAddStacked += OnItemAdded;
            
            inventory.OnItemRemoved += OnItemRemoved;
            inventory.OnItemRemoveStacked += OnItemRemoved;
        }
        public void OnItemAdded(InventoryItem item)
        {
            if(HasEffect(item) == false) return;

            if (item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;
            
            foreach (var effect in effects)
            {
                effect.AddEffect(_hero);
            }
        }

        public void OnItemRemoved(InventoryItem item)
        {
            if(HasEffect(item) == false) return;

            if (item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;
            
            foreach (var effect in effects)
            {
                effect.RemoveEffect(_hero);
            }
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemAddStacked -= OnItemAdded;
            
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemRemoveStacked -= OnItemRemoved;
        }
        
        private bool HasEffect(InventoryItem item)
        {
            return (item.Flags & ItemFlags.Effectable) == ItemFlags.Effectable;
        }
    }
}