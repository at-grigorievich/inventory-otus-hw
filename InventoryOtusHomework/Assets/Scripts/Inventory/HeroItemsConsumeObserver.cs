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
            if (item.TryGetComponent(out HeroDamageEffectComponent damageEffect) == true)
            {
                _hero.damage += damageEffect.DamageEffect;
            }
        }
    }
}