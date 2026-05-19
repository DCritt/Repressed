using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterLockMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _slots = new TextMeshProUGUI[4];
    private LetterLock _letterLock;

    public void UpSlot(int slot)
    {
        char character = _slots[slot].text[0];

        int offset = character - 'A';
        offset = (offset - 1 + 26) % 26;

        _slots[slot].text = ((char)('A' + offset)).ToString();
    }

    public void DownSlot(int slot)
    {
        char character = _slots[slot].text[0];

        int offset = character - 'A';
        offset = (offset + 1) % 26;

        _slots[slot].text = ((char)('A' + offset)).ToString();
    }

    public string GetCode()
    {
        return (_slots[0].text + _slots[1].text + _slots[2].text + _slots[3].text);
    }

    public void CloseMenu()
    {
        MenuManager.Instance.SetActiveMenu(gameObject);
    }

    public void TrySubmit()
    {
        if (_letterLock.TryOpen(GetCode()))
        {
            CloseMenu();
            Destroy(_letterLock);
            Destroy(gameObject);
        }
    }

    public void SetLock(LetterLock letterLock)
    {
        _letterLock = letterLock;
    }
}
