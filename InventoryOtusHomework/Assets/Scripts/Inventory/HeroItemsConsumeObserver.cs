using System;
using System.Collections.Generic;

namespace ATG.OtusHW.Inventory
{
    public class HeroItemsConsumeObserver: IDisposable
    {
        private readonly Inventory _inventory;
        private readonly Hero _hero;
        
        public HeroItemsConsumeObserver(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
            
            _inventory.OnItemConsumed += OnItemConsumed;
        }
        
        private void OnItemConsumed(InventoryItem item)
        {
            if(item.TryGetComponents(out IEnumerable<HeroEffectComponent> effects) == false) return;
            
            foreach (var heroEffectComponent in effects)
            {
                heroEffectComponent.AddEffect(_hero);
            }
        }
        
        public void Dispose()
        {
            _inventory.OnItemConsumed -= OnItemConsumed;
        }
    }
}