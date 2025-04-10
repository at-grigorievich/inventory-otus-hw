using System;
using ATG.OtusHW.Inventory.UI;

namespace ATG.OtusHW.Inventory
{
    public class InventoryToEquipmentProvider: IDisposable
    {
        private readonly Inventory _inventory;
        private readonly Equipment _equipment;

        private readonly InventoryView _inventoryView;
        private readonly EquipmentSetView _equipmentView;

        public InventoryToEquipmentProvider(Inventory inventory, Equipment equipment, 
            InventoryView inventoryView, EquipmentSetView equipmentView)
        {
            _inventory = inventory;
            _equipment = equipment;
            
            _inventoryView = inventoryView;
            _equipmentView = equipmentView;
            
            _equipment.OnItemTakeOff += OnItemTakeOff;
            
            _inventoryView.OnEquipClicked += OnEquipClicked;
            _equipmentView.OnItemTakeOffClicked += OnTakeOffClickedClicked;
        }

        private void OnEquipClicked(InventoryItem obj)
        {
            var equipped = InventoryUseCases.RemoveItem(_inventory, obj, removeByRef: true);
            
            if(equipped == null) return;
            
            EquipmentUseCases.TakeOnItem(_equipment, equipped);
        }
        
        private void OnTakeOffClickedClicked(InventoryItem obj)
        {
            EquipmentUseCases.TakeOffItem(_equipment, obj);
        }

        private void OnItemTakeOff(InventoryItem obj)
        {
            InventoryUseCases.AddItem(_inventory, obj);
        }
        
        public void Dispose()
        {
            _inventoryView.OnEquipClicked -= OnEquipClicked;
            _equipmentView.OnItemTakeOffClicked -= OnTakeOffClickedClicked;
            
            _equipment.OnItemTakeOff -= OnItemTakeOff;
        }
    }
}