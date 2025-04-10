using ATG.OtusHW.Inventory.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ATG.OtusHW.Inventory
{
    public class InventoryItemDebug: MonoBehaviour
    {
        public Inventory Inventory;
        public Equipment Equipment;
        
        public Hero Hero;
        
        public InventoryView InventoryView;
        public EquipmentSetView EquipmentView;
        
        private HeroItemsEffectsController _heroItemsEffectsController = new();
        private HeroItemsConsumeObserver _heroItemsConsumeObserver = new();

        private HeroEquipEffectObserver _heroEquipEffectObserver;
        
        private InventoryPresenter _inventoryPresenter;
        private EquipmentPresenter _equipmentPresenter;

        private InventoryToEquipmentProvider _provider;

        private void Awake()
        {
            _heroItemsEffectsController.Construct(Inventory, Hero);
            _heroItemsConsumeObserver.Construct(Inventory, Hero);

            _inventoryPresenter = new InventoryPresenter(Inventory, InventoryView);
            _equipmentPresenter = new EquipmentPresenter(Equipment, EquipmentView);

            _provider = new InventoryToEquipmentProvider(Inventory, Equipment, InventoryView, EquipmentView);
            
            _heroEquipEffectObserver = new HeroEquipEffectObserver(Equipment, Hero);
        }

        private void OnDestroy()
        {
            _heroItemsEffectsController.OnDispose();
            _heroItemsConsumeObserver.OnDispose();
            
            _inventoryPresenter.Dispose();
            _equipmentPresenter.Dispose();
            
            _provider.Dispose();
            
            _heroItemsConsumeObserver.OnDispose();
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