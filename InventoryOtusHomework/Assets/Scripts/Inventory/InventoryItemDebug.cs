using Sirenix.OdinInspector;
using UnityEngine;

namespace ATG.OtusHW.Inventory
{
    public class InventoryItemDebug: MonoBehaviour
    {
        public InventoryItemConfig Config;

        [ReadOnly] public InventoryItem Item;
        
        [Button]
        public void CloneItem()
        {
            Item = Config.Prototype.Clone();

            /*Item.MetaData = new InventoryItemMetaData()
            {
                Name = "test",
            };*/
        }
    }
}