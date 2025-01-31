using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;

    internal void InitOptionButton(string newText,Action onClickAction, Action favorabilityChange)
    {
        var go = Instantiate(buttonPrefab, transform);
        var text = go.GetComponentInChildren<TMP_Text>();
        text.text = newText;

        var button = go.GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError($"Button component missing on {buttonPrefab.name}");
            return;
        }
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {

            Debug.Log($"Button Clicked: {newText}");
            onClickAction();
            favorabilityChange();
            ClearOptions();
            });
    }
    internal bool HasOption()
    {
        return transform.childCount > 0;
    }
    public void ClearOptions()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
