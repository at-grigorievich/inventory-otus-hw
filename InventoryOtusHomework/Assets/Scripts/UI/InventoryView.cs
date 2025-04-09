using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.OtusHW.Inventory.UI
{
    [Serializable]
    public sealed class InventoryViewPool
    {
        [SerializeField] private RectTransform itemsGrid;
        [SerializeField] private ItemView prefab;
        [SerializeField] private int poolSize;

        private Queue<ItemView> _pool = new ();

        public void InitPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                _pool.Enqueue(CreateInstance());
            }
        }
        
        public void Enqueue(ItemView itemView)
        {
            itemView.Hide();
            _pool.Enqueue(itemView);
        }

        public ItemView Dequeue()
        {
            if (_pool.Count <= 0)
            {
                _pool.Enqueue(CreateInstance());    
            }
            
            return _pool.Dequeue();
        }

        private ItemView CreateInstance()
        {
            ItemView instance = GameObject.Instantiate(prefab, itemsGrid);
            instance.Hide();

            return instance;
        }
    }
    
    public sealed class InventoryView: MonoBehaviour
    {
        [SerializeField] private InventoryViewPool pool;

        private HashSet<ItemView> _activeItems = new HashSet<ItemView>() ;

        private ItemView _lastSelected;
        
        private void Awake()
        {
            pool.InitPool();
        }
        
        public void Show()
        {
            //TODO...
        }

        public void Hide()
        {
            //TODO...
        }

        public void AddItem(InventoryItem item)
        {
            ItemViewData viewData = new ItemViewData(item);
            
            ItemView view = pool.Dequeue();
            view.Show(viewData);

            view.OnSelected += OnSelectedView;
            
            _activeItems.Add(view);
        }

        public void ChangeItem(InventoryItem item)
        {
            ItemViewData viewData = new ItemViewData(item);
            
            foreach (var view in _activeItems)
            {
                if(view.Data.HasValue == false) continue;
                
                if(view.Data.Value.Id == item.Id == false) continue;
                
                view.Show(viewData);
                
                return;
            }
        }

        public void RemoveItem(InventoryItem item)
        {
            ItemView removedView = null;
            
            foreach (var view in _activeItems)
            {
                if(view.Data.HasValue == false) continue;
                
                if(view.Data.Value.Id == item.Id == false) continue;

                removedView = view;
                break;
            }

            if (removedView == null)
                throw new NullReferenceException($"Item {item.Id} was not found in inventory view");
            
            removedView.OnSelected -= OnSelectedView;
            
            _activeItems.Remove(removedView);
            pool.Enqueue(removedView);
        }

        private void OnSelectedView(ItemView obj)
        {
            if (_lastSelected != null)
            {
                _lastSelected.SetSelectedStatus(false);
            }

            obj.SetSelectedStatus(true);
            
            _lastSelected = obj;
        }
    }
}