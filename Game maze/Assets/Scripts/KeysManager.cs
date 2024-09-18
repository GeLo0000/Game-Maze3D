using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeysManager : MonoBehaviour
{
    // UI text element to display the current key count
    [SerializeField] private TextMeshProUGUI _keyText;

    // Current number of keys collected by the player
    private int _keyCount = 0;

    // Maximum number of keys needed to finish the level
    private int _maxCount = 3;

    private void Start()
    {
        // Initialize the key count display on game start
        UpdateKeyText();
    }

    // Adds keys to the current count
    public void AddKey(int amount)
    {
        _keyCount += amount;
        UpdateKeyText();
    }

    // Returns the current number of keys
    public int GetCountKey()
    {
        return _keyCount;
    }

    // Returns the maximum number of keys required
    public int GetMaxCountKey()
    {
        return _maxCount;
    }

    // Updates the key count text display
    private void UpdateKeyText()
    {
        _keyText.text = _keyCount.ToString() + "/" + _maxCount.ToString();
    }
}
