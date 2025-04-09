using System;
using ATG.OtusHW.Inventory.UI;
using UnityEngine;

namespace ATG.OtusHW.Inventory
{
    public sealed class InventoryViewPresenter: IInventoryObserver, IDisposable
    {
        private readonly Inventory _inventory;
        private readonly InventoryView _inventoryView;

        public InventoryViewPresenter(Inventory inventory, InventoryView inventoryView)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
            
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemAddStacked += OnItemStacked;
            
            _inventory.OnItemRemoved += OnItemRemoved;
            _inventory.OnItemRemoveStacked += OnItemStacked;
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemAddStacked -= OnItemStacked;
            
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemRemoveStacked -= OnItemStacked;
        }
        
        public void OnItemAdded(InventoryItem item)
        {
            _inventoryView.AddItem(item);
        }

        public void OnItemRemoved(InventoryItem item)
        {
            _inventoryView.RemoveItem(item);
        }
        
        private void OnItemStacked(InventoryItem obj)
        {
            _inventoryView.ChangeItem(obj);
        }
    }
}