using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Skin : MonoBehaviour
{
    private Image _image;

    private Button _button;

    private SkinManager _skinManager;

    private int _skinId;
    public int SkinId => _skinId;

    public void Initialize(SkinManager skinManager, int skinId)
    {
        _image = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(SelectThis);

        _skinManager = skinManager;
        _skinId = skinId;
    }
    private void SelectThis()
    {
        _skinManager.SelectLevel(this);
    }
    public void UpdateMe()
    {
        if (PlayerPrefs.GetInt("CurrentSkin", 0) != _skinId)
            _image.DOFade(0.3f, 0.5f).SetLink(gameObject);
        else
            _image.DOFade(1f, 0.5f).SetLink(gameObject);
    }
}
