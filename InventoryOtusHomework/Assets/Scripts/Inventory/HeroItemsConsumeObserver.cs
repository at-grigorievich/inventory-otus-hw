using System.Collections.Generic;

namespace ATG.OtusHW.Inventory
{
    public class HeroItemsConsumeObserver
    {
        private Inventory _inventory;
        private Hero _hero;
        
        public void Construct(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
            
            _inventory.OnItemConsumed += OnItemConsumed;
        }

        public void OnDispose()
        {
            _inventory.OnItemConsumed -= OnItemConsumed;
        }
        
        private void OnItemConsumed(InventoryItem item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;
            
            foreach (var heroEffectComponent in effects)
            {
                heroEffectComponent.AddEffect(_hero);
            }
        }
    }
}