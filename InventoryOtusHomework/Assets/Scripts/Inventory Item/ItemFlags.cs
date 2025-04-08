using System;

namespace ATG.OtusHW.Inventory
{
    [Flags]
    public enum ItemFlags
    {
        None = 0,
        Stackable = 1,
        Consumable = 2,
        Equippable = 4,
        Effectable = 8
    }
}