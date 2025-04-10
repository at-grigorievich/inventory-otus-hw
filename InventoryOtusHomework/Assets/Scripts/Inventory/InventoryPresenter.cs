using System;
using ATG.OtusHW.Inventory.UI;

namespace ATG.OtusHW.Inventory
{
    public sealed class InventoryPresenter: IInventoryObserver, IDisposable
    {
        private readonly Inventory _inventory;
        private readonly InventoryView _inventoryView;

        public InventoryPresenter(Inventory inventory, InventoryView inventoryView)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
            
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemAddStacked += OnItemStacked;
            
            _inventory.OnItemRemoved += OnItemRemoved;
            _inventory.OnItemRemoveStacked += OnItemStacked;
            
            _inventoryView.OnConsumeClicked += OnConsumeClicked;
            _inventoryView.OnDropClicked += OnDropClicked;
        }
        
        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemAddStacked -= OnItemStacked;
            
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemRemoveStacked -= OnItemStacked;
            
            _inventoryView.OnConsumeClicked -= OnConsumeClicked;
            _inventoryView.OnDropClicked -= OnDropClicked;
        }
        
        public void OnItemAdded(InventoryItem item)
        {
            _inventoryView.AddItem(item);
        }

        public void OnItemRemoved(InventoryItem item)
        {
            _inventoryView.RemoveItem(item, removeByRef: true);
        }
        
        private void OnItemStacked(InventoryItem obj)
        {
            _inventoryView.ChangeItem(obj);
        }
        
        private void OnDropClicked(InventoryItem obj)
        {
            InventoryUseCases.RemoveItem(_inventory, obj, removeByRef: true);
        }

        private void OnConsumeClicked(InventoryItem obj)
        {
            InventoryUseCases.ConsumeItem(_inventory, obj, consumeByRef: true);
        }
    }
}