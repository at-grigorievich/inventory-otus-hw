using System;
using System.Collections.Generic;
using System.Linq;
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
            var copiedComponents = new IItemComponent[Components.Length];
            
            for (var i = 0; i < copiedComponents.Length; i++)
            {
                IItemComponent component = Components[i];
                copiedComponents[i] = component.Clone();
            }
            
            return new InventoryItem()
            {
                Id = Id,
                MetaData = MetaData.Clone(),
                Components = copiedComponents,
                Flags = Flags
            };
        }
        
        public bool TryGetComponent<T>(out T component) where T : IItemComponent
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

        public bool TryGetComponents<T>(out IEnumerable<T> components) where T : IItemComponent
        {
            List<T> result = new List<T>();
            
            foreach (var itemComponent in Components)
            {
                if (itemComponent is T element)
                {
                    result.Add(element);
                }
            }

            components = result;

            return result.Count > 0;
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