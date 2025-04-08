using UnityEngine;

namespace ATG.OtusHW.Inventory
{
    [CreateAssetMenu(fileName = "InventoryItemConfig", menuName = "Configs/Ne Inventory Item Config", order = 0)]
    public class InventoryItemConfig : ScriptableObject
    {
        public InventoryItem Prototype;
    }
}