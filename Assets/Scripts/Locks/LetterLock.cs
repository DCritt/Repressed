using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LetterLock : MonoBehaviour, ILock
{
    [SerializeField] private GameObject _letterLockMenuPrefab;
    [SerializeField] private string _code;
    private GameObject _lockMenu;
    private LetterLockMenu _lockMenuScript;

    private UnityEvent<Entity> _useDoor = new UnityEvent<Entity>();
    private Entity _entity;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _lockMenu = MenuManager.Instance.CreateMenu(_letterLockMenuPrefab);
        _lockMenuScript = _lockMenu.GetComponent<LetterLockMenu>();
        _lockMenuScript.SetLock(this);
    }

    public bool CanOpen(Entity entity)
    {
        return true;
    }

    public void UseLock(Entity entity, UnityAction<Entity> useDoor)
    {
        _useDoor.RemoveAllListeners();
        _useDoor.AddListener(useDoor);
        _entity = entity;
        MenuManager.Instance.SetActiveMenu(_lockMenu);
    }

    public bool TryOpen(string code)
    {
        Debug.Log(code);
        if (code.ToLower().Equals(_code.ToLower()))
        {
            _useDoor?.Invoke(_entity);
            
            return true;
        }
        return false;
    }
}
