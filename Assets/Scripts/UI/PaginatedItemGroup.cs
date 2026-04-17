using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PaginatedItemGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _slots = new List<GameObject>();
    [SerializeField] private GameObject _itemHolder;
    private List<GameObject> _items = new List<GameObject>();

    public int CurrPage { get; private set; } = 1;
    private int _maxPage = 1;
    private int _itemsPerPage = 9;

    public void Awake()
    {
    }

    public void Start()
    {
       
    }

    public void SetItems(List<GameObject> items)
    {
        foreach (GameObject item in _items)
        {
            Destroy(item);
        }
        _items = items;
        foreach (GameObject item in _items)
        {
            item.transform.SetParent(_itemHolder.transform);
        }
        _maxPage = (int) Mathf.Ceil((float)_items.Count / _itemsPerPage);
        SetPage(1);
    }

    public void AddItem(GameObject item)
    {
        _items.Insert(0, item);
        item.transform.SetParent(_itemHolder.transform);
        _maxPage = (int) Mathf.Ceil((float)_items.Count / _itemsPerPage);
        SetPage(CurrPage);
    }

    public void RemoveItem(GameObject item)
    {
        _items.Remove(item);
        Destroy(item);
        _maxPage = (int) Mathf.Ceil((float)_items.Count / _itemsPerPage);
        SetPage(CurrPage);
    }

    public void ToNextPage()
    {
        SetPage(CurrPage + 1);
    }

    public void ToPreviousPage()
    {
        SetPage(CurrPage - 1);
    }

    public void SetPage(int page)
    {
        CurrPage = Mathf.Clamp(page, 1, _maxPage);

        int startIndex = (CurrPage - 1) * _itemsPerPage;
        int endIndex = (CurrPage * _itemsPerPage) - 1;

        for (int i = 0; i < _items.Count; i++)
        {
            bool show = i >= startIndex && i <= endIndex;
            int photoIndex = i - startIndex;

            _items[i].SetActive(show);

            if (show)
            {
                _items[i].transform.SetParent(_slots[photoIndex].transform, false);
                _items[i].transform.position = _slots[photoIndex].transform.position;
            }
        }
    }
}
