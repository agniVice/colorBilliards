using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Level : MonoBehaviour
{
    private bool _unlocked;

    private TextMeshProUGUI _levelNumber;
    private Image _lockImage;
    private Button _button;

    private LevelManager _levelManager;

    private int _levelId;
    public int LevelId => _levelId;
    public bool Unlocked => _unlocked;

    public void Initialize(LevelManager levelManager, bool unlocked, int levelId)
    {
        _levelNumber = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _lockImage = transform.GetChild(1).GetComponentInChildren<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(SelectThis);

        _levelManager = levelManager;
        _levelId = levelId;
        _unlocked = unlocked;

        if (_unlocked)
        {
            _levelNumber.gameObject.SetActive(true);
            _levelNumber.text = (_levelId + 1).ToString();
            _lockImage.gameObject.SetActive(false);
        }
        else
        {
            _levelNumber.gameObject.SetActive(false);
            _lockImage.gameObject.SetActive(true);
        }
    }
    private void SelectThis()
    { 
        _levelManager.SelectLevel(this);
    }
    public void HideAll()
    {
        _levelNumber.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(false);
    }
}
