using System;
using System.Collections.Generic;
using ATG.OtusHW.Inventory;

namespace ATG.OtusHW
{
    [Serializable]
    public class Equipment
    {
        public List<InventoryItem> ItemsDebug = new();
        public Dictionary<EquipType, InventoryItem> Items = new();

        public event Action<InventoryItem> OnItemTakeOn;
        public event Action<InventoryItem> OnItemTakeOff;
        
        public void NotifyItemTakeOn(InventoryItem item)
        {
            OnItemTakeOn?.Invoke(item);
        }

        public void NotifyItemTakeOff(InventoryItem item)
        {
            OnItemTakeOff?.Invoke(item);
        }
    }
    
    public static class EquipmentUseCases
    {
        public static void TakeOnItem(Equipment equipment, InventoryItem inventoryItem)
        {
            if(CanEquip(inventoryItem) == false) return;
            
            if(inventoryItem.TryGetComponent(out HeroEquipmentComponent component) == false) return;

            if (equipment.Items.ContainsKey(component.Tag) == true)
            {
                TakeOffItem(equipment, inventoryItem);
            }
            
            equipment.Items.Add(component.Tag, inventoryItem);
            equipment.ItemsDebug.Add(inventoryItem);
            
            equipment.NotifyItemTakeOn(inventoryItem);
        }

        public static InventoryItem TakeOffItem(Equipment equipment, InventoryItem inventoryItem)
        {
            if(CanEquip(inventoryItem) == false) return null;
            
            if(inventoryItem.TryGetComponent(out HeroEquipmentComponent component) == false) return null;

            if (equipment.Items.ContainsKey(component.Tag) == true)
            {
                InventoryItem result = equipment.Items[component.Tag];
                
                equipment.Items.Remove(component.Tag);
                equipment.ItemsDebug.Remove(result);
                
                equipment.NotifyItemTakeOff(result);

                return result;
            }

            return null;
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