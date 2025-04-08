using System;
using System.Collections.Generic;
using System.Linq;

namespace ATG.OtusHW.Inventory
{
    [Serializable]
    public class Inventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem> OnItemConsumed; 
        
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
    }

    public class InventoryUseCases
    {
        public static void AddItem(Inventory inventory, InventoryItem item)
        {
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
        
        public static InventoryItem RemoveItem(Inventory inventory, InventoryItem item)
        {
            var res = inventory.Items.FirstOrDefault(i => i.Id == item.Id);
            
            if(res == null) return null;
            
            inventory.Items.Remove(res);
            inventory.NotifyItemRemoved(item);

            return res;
        }
        
        public static InventoryItem RemoveItem(Inventory inventory, InventoryItemConfig config)
        {
            var item = config.Prototype.Clone();
            
            var res = inventory.Items.FirstOrDefault(i => i.Id == item.Id);
            
            if(res == null) return null;
            
            inventory.Items.Remove(res);
            inventory.NotifyItemRemoved(item);

            return item;
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
                inventory.NotifyItemConsumed(removed);
            }
        }

        public static bool CanConsume(InventoryItem item)
        {
            return HasFlag(item, ItemFlags.Consumable);
        }

        public static bool HasFlag(InventoryItem item, ItemFlags itemFlag)
        {
            return (item.Flags & itemFlag) == itemFlag;
        }
    }
}