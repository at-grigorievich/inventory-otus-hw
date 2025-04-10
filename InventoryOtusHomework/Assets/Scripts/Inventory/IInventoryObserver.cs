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
    
    public class HeroItemsEffectsController : IInventoryObserver
    {
        private Inventory _inventory;
        private Hero _hero;
        
        public void Construct(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
            
            inventory.OnItemAdded += OnItemAdded;
            inventory.OnItemAddStacked += OnItemAdded;
            
            inventory.OnItemRemoved += OnItemRemoved;
            inventory.OnItemRemoveStacked += OnItemRemoved;
        }

        public void OnDispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemAddStacked -= OnItemAdded;
            
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemRemoveStacked -= OnItemRemoved;
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

        private bool HasEffect(InventoryItem item)
        {
            return (item.Flags & ItemFlags.Effectable) == ItemFlags.Effectable;
        }
    }
}