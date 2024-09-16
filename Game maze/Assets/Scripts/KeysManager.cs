using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeysManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyText;
    private int _keyCount = 0;

    private void Start()
    {
        UpdateKeyText();
    }

    public void AddKey(int amount)
    {
        _keyCount += amount;
        UpdateKeyText();
    }

    public void SpendKey(int amount)
    {
        _keyCount -= amount;
        if (_keyCount < 0) _keyCount = 0;
        UpdateKeyText();
    }

    private void UpdateKeyText()
    {
        _keyText.text = _keyCount.ToString() + "/3";
    }
}
