using System;
using ATG.OtusHW.Inventory;
using ATG.OtusHW.Inventory.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public readonly struct ItemViewData
{
    public readonly InventoryItem Item;
    
    public readonly string Id;
    public readonly string Name;
    public readonly Sprite Icon;
        
    public readonly bool IsStackable;
    public readonly int StackCurrent;
    public readonly int StackMax;

    public readonly bool IsConsumable;
    public readonly bool IsEquipable;

    public ItemViewData(InventoryItem item)
    {
        Item = item;
        
        Id = item.Id;
        Name = item.MetaData.Name;
        Icon = item.MetaData.Icon;
        
        IsStackable = InventoryUseCases.CanStack(item);
        IsConsumable = InventoryUseCases.CanConsume(item);
        IsEquipable = InventoryUseCases.CanEquip(item);

        if (IsStackable)
        {
            StackCurrent = 5;
            StackMax = 10;
        }
        else
        {
            StackCurrent = StackMax = 0;
        }
    }
}

[RequireComponent(typeof(CanvasGroup))]
public class ItemView : MonoBehaviour, IPointerClickHandler
{
    private CanvasGroup _canvasGroup;
    
    [SerializeField] private TMP_Text itemNameOutput;
    [SerializeField] private Image icon;
    [SerializeField] private CounterView counter;
    [SerializeField] private Image selectedVisual;
    
    public event Action<ItemView> OnSelected;
    
    public ItemViewData? Data { get; private set; }
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        counter.SetActive(false);
        SetSelectedStatus(false);
    }

    public void Show(ItemViewData data)
    {
        Data = data;

        itemNameOutput.text = data.Name;
        
        icon.sprite = data.Icon;
        
        counter.SetActive(data.IsStackable);
        counter.UpdateCount(data.StackCurrent, data.StackMax);
        
        _canvasGroup.alpha = 1;
    }
    
    public void Hide()
    {
        _canvasGroup.alpha = 0;
        counter.SetActive(false);

        Data = null;
    }

    public void SetSelectedStatus(bool isSelected)
    {
        selectedVisual.enabled = isSelected;
    }
    
    public void OnPointerClick(PointerEventData eventData) => OnSelected?.Invoke(this);
    
}
