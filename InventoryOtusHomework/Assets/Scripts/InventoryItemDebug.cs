using System;
using ATG.OtusHW.Inventory.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ATG.OtusHW.Inventory
{
    public class InventoryItemDebug: MonoBehaviour
    {
        public InventoryItemConfig Config;
        public Inventory Inventory;
        public Hero Hero;
        public InventoryView InventoryView;
        
        private HeroItemsEffectsController _heroItemsEffectsController = new();
        private HeroItemsConsumeObserver _heroItemsConsumeObserver = new();
        private InventoryViewPresenter _inventoryViewPresenter;
        
        [ReadOnly] public InventoryItem Item;

        private void Awake()
        {
            _heroItemsEffectsController.Construct(Inventory, Hero);
            _heroItemsConsumeObserver.Construct(Inventory, Hero);

            _inventoryViewPresenter = new InventoryViewPresenter(Inventory, InventoryView);
        }

        private void OnDestroy()
        {
            _heroItemsEffectsController.OnDispose();
            _inventoryViewPresenter.Dispose();
        }

        [Button]
        public void AddItem(InventoryItemConfig config)
        {
            var item = config.Prototype.Clone();
            InventoryUseCases.AddItem(Inventory, item);
        }

        [Button]
        public void RemoveItem(InventoryItemConfig config)
        {
            var item = config.Prototype.Clone();
            InventoryUseCases.RemoveItem(Inventory, item);
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig config)
        {
            InventoryUseCases.ConsumeItem(Inventory, config);
        }
    }
}