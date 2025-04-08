using System;

namespace ATG.OtusHW.Inventory
{
    public interface IItemComponent { }
    
    [Serializable]
    public class StackableComponents: IItemComponent
    {
        public int Count;
        public int MaxCount;
    }
}