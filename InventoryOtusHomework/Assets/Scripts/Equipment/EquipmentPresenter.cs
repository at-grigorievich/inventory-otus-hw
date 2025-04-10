using System;
using ATG.OtusHW.Inventory.UI;

namespace ATG.OtusHW.Inventory
{
    public class EquipmentPresenter: IEquipmentObserver, IDisposable
    {
        private readonly Equipment _equipment;
        private readonly EquipmentSetView _view;

        public EquipmentPresenter(Equipment equipment, EquipmentSetView view)
        {
            _equipment = equipment;
            
            _view = view;
            
            _equipment.OnItemTakeOn += OnItemTakeOn;
            _equipment.OnItemTakeOff += OnItemTakeOff;
        }
        
        public void OnItemTakeOn(InventoryItem item)
        {
            _view.AddItem(item);
        }

        public void OnItemTakeOff(InventoryItem item)
        {
            _view.RemoveItem(item);
        }

        public void Dispose()
        {
            _equipment.OnItemTakeOn -= OnItemTakeOn;
            _equipment.OnItemTakeOff -= OnItemTakeOff;
        }
    }
}