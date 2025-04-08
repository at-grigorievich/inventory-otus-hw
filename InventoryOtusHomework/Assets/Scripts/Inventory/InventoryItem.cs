using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ATG.OtusHW.Inventory
{
    [Serializable, ShowOdinSerializedPropertiesInInspector]
    public class InventoryItem
    {
        public string Id;

        public InventoryItemMetaData MetaData;
        public ItemFlags Flags;
        
        [SerializeReference]
        public IItemComponent[] Components;
        
        public InventoryItem Clone()
        {
            return new InventoryItem()
            {
                Id = Id,
                MetaData = MetaData.Clone()
            };
        }
        
        public bool TryGetComponent<T>(out T component)
        {
            foreach (var itemComponent in Components)
            {
                if (itemComponent is T result)
                {
                    component = result;
                    return true;
                }
            }
            
            component = default(T);
            return false;
        }
    }

    [Serializable]
    public class InventoryItemMetaData
    {
        public string Name;
        public string Description;
        public Sprite Icon;

        public InventoryItemMetaData Clone()
        {
            return new InventoryItemMetaData()
            {
                Name = Name,
                Description = Description,
                Icon = Icon,
            };
        }
    }
}