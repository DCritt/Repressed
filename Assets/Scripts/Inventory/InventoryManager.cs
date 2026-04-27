using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IKeyHolder
{
    private List<string> _keys = new List<string>();

    public void AddKey(string keyId)
    {
        _keys.Add(keyId);
    }

    public bool HasKey(string keyId)
    {
        return _keys.Contains(keyId);
    }
}
