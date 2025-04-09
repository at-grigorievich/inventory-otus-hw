using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.OtusHW.Inventory
{
    [Serializable]
    public class Inventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem> OnItemConsumed;
        public event Action<InventoryItem> OnItemAddStacked; 
        public event Action<InventoryItem> OnItemRemoveStacked; 
        
        public List<InventoryItem> Items = new();
        
        public void NotifyItemAdded(InventoryItem item)
        {
            OnItemAdded?.Invoke(item);
        }

        public void NotifyItemRemoved(InventoryItem item)
        {
            OnItemRemoved?.Invoke(item);
        }

        public void NotifyItemConsumed(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }

        public void NotifyItemAddStacked(InventoryItem item)
        {
            OnItemAddStacked?.Invoke(item);
        }
        
        public void NotifyItemRemoveStacked(InventoryItem item)
        {
            OnItemRemoveStacked?.Invoke(item);
        }
    }

    public class InventoryUseCases
    {
        public static void AddItem(Inventory inventory, InventoryItem item)
        {
            if (TryAddStackItem(inventory, item) == true) return;
            
            inventory.Items.Add(item);
            inventory.NotifyItemAdded(item);
        }
        
        public static void AddItems(Inventory inventory, InventoryItem item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddItem(inventory, item);
            }
        }
        
        public static InventoryItem RemoveItem(Inventory inventory, InventoryItem item, bool removeByRef = false)
        {
            InventoryItem res = removeByRef == false
                ? inventory.Items.FirstOrDefault(i => i.Id == item.Id)
                : inventory.Items.FirstOrDefault(i => ReferenceEquals(item, i));
            
            if(res == null) return null;
            
            if (TryRemoveStackItem(inventory, res) == true) return res;
            
            inventory.Items.Remove(res);
            inventory.NotifyItemRemoved(res);

            return res;
        }
        
        public static InventoryItem RemoveItem(Inventory inventory, InventoryItemConfig config)
        {
            var item = config.Prototype.Clone();
            
            var res = inventory.Items.FirstOrDefault(i => i.Id == item.Id);
            
            if(res == null) return null;
            
            if (TryRemoveStackItem(inventory, res) == true) return res;
            
            inventory.Items.Remove(res);
            inventory.NotifyItemRemoved(res);

            return res;
        }
        
        public static void RemoveItems(Inventory inventory, InventoryItem item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveItem(inventory, item);
            }
        }

        public static void ConsumeItem(Inventory inventory, InventoryItemConfig itemConfig)
        {
            var proto = itemConfig.Prototype;

            if (CanConsume(proto) == true)
            {
                var removed = RemoveItem(inventory, itemConfig);
                if(removed == null) return;
                
                inventory.NotifyItemConsumed(removed);
            }
        }

        
        private static bool TryAddStackItem(Inventory inventory, InventoryItem item)
        {
            if (CanStack(item) == true)
            {
                foreach (var inventoryItem in inventory.Items)
                {
                    if(item.Id != inventoryItem.Id) continue;
                    if(inventoryItem.TryGetComponent(out StackableItemComponent component) == false) continue;
                    
                    Debug.Log(component.Count == component.MaxCount);
                    if (component.Count == component.MaxCount) continue;

                    component.Count++;
                    
                    inventory.NotifyItemAddStacked(inventoryItem);
                    return true;
                }
            }

            return false;
        }

        private static bool TryRemoveStackItem(Inventory inventory, InventoryItem item)
        {
            if (CanStack(item) == true)
            {
                if(item.TryGetComponent(out StackableItemComponent component) == false) return false;
                
                if(component.Count <= 1) return false;

                component.Count--;
                
                inventory.NotifyItemRemoveStacked(item);
                return true;
            }

            return false;
        }

        public static bool CanConsume(InventoryItem item)
        {
            return HasFlag(item, ItemFlags.Consumable);
        }

        public static bool CanStack(InventoryItem item)
        {
            return HasFlag(item, ItemFlags.Stackable);
        }

        public static bool CanEquip(InventoryItem item)
        {
            return HasFlag(item, ItemFlags.Equippable);
        }

        public static bool HasFlag(InventoryItem item, ItemFlags itemFlag)
        {
            return (item.Flags & itemFlag) == itemFlag;
        }
    }
}