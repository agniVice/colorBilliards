using TMPro;
using UnityEngine;

public class LocalizedString : MonoBehaviour
{
    [SerializeField] private int _keyId;

    private TextMeshProUGUI _text;

    private void Start()
    {
        LanguageManager.Instance.LocalizedStrings.Add(this);
        _text = GetComponent<TextMeshProUGUI>();
        LocalizeMe();
    }
    public void LocalizeMe()
    {
        GetLocalized(_keyId);
    }
    private void GetLocalized(int keyId)
    {
        if (LanguageManager.Instance != null)
            _text.text = LanguageManager.Instance.GetTranslate(keyId);
        else
            _text.text = "ERROR";
    }
}
