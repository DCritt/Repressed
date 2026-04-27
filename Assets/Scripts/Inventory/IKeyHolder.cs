using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeyHolder
{
    public bool HasKey(string keyId);
    public void AddKey(string keyId);
}
