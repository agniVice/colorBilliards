using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUserInterface : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;

    [Header("Settings")]
    [SerializeField] private Button _soundToggle;
    [SerializeField] private Button _musicToggle;
    [SerializeField] private Button _vibrationToggle;

    [SerializeField] private Sprite _soundEnabled;
    [SerializeField] private Sprite _soundDisabled;

    [SerializeField] private Sprite _musicEnabled;
    [SerializeField] private Sprite _musicDisabled;

    [SerializeField] private Sprite _vibrationEnabled;
    [SerializeField] private Sprite _vibrationDisabled;

    [SerializeField] private List<Transform> _transformsMenu = new List<Transform>();
    [SerializeField] private List<Transform> _transformsSettings = new List<Transform>();

    [SerializeField] private Image _fadeOut;

    [SerializeField] private Transform _dropDownPanel;

    [SerializeField] private TextMeshProUGUI _highScoreText;

    private bool _dropDownOpened = false;

    private void Start()
    {
        Initialize();

        _fadeOut.transform.localPosition = Vector3.zero;
        _fadeOut.rectTransform.DOLocalMoveY(2500, 0.4f).SetLink(_fadeOut.gameObject);
    }
    private void Initialize()
    {
        AudioVibrationManager.Instance.SoundChanged += UpdateSoundImage;
        AudioVibrationManager.Instance.MusicChanged += UpdateMusicImage;
        //AudioVibrationManager.Instance.VibrationChanged += UpdateVibrationImage;

        _soundToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleSound);
        _musicToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleMusic);
        //_vibrationToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleVibration);

        UpdateSoundImage();
        UpdateMusicImage();
        //UpdateVibrationImage();
        UpdateLanguageIcon();

        OnCloseSettingsButtonClicked();
    }
    private void OnDisable()
    {
        AudioVibrationManager.Instance.SoundChanged -= UpdateSoundImage;
        AudioVibrationManager.Instance.MusicChanged -= UpdateMusicImage;
        //AudioVibrationManager.Instance.VibrationChanged -= UpdateVibrationImage;
    }
    private void UpdateSoundImage()
    {
        if (AudioVibrationManager.Instance.IsSoundEnabled) 
            _soundToggle.transform.GetChild(0).GetComponent<Image>().sprite = _soundEnabled;
        else
            _soundToggle.transform.GetChild(0).GetComponent<Image>().sprite = _soundDisabled;
    }
    private void UpdateMusicImage()
    {
        if (AudioVibrationManager.Instance.IsMusicEnabled)
            _musicToggle.transform.GetChild(0).GetComponent<Image>().sprite = _musicEnabled;
        else
            _musicToggle.transform.GetChild(0).GetComponent<Image>().sprite = _musicDisabled;
    }
    private void UpdateVibrationImage()
    {
        if (AudioVibrationManager.Instance.IsVibrationEnabled)
            _vibrationToggle.transform.GetChild(0).GetComponent<Image>().sprite = _vibrationEnabled;
        else
            _vibrationToggle.transform.GetChild(0).GetComponent<Image>().sprite = _vibrationDisabled;
    }
    public void OnSettingsButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        _menuPanel.SetActive(false);
        _settingsPanel.SetActive(true);

        foreach (Transform transform in _transformsSettings)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    public void OnCloseSettingsButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        _menuPanel.SetActive(true);
        _settingsPanel.SetActive(false);

        //_highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        foreach (Transform transform in _transformsMenu)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    public void OnPlayButtonClicked()
    {
        _fadeOut.transform.localScale = Vector3.zero;
        _fadeOut.transform.localPosition = new Vector3(0, -2500, 0);
        _fadeOut.rectTransform.DOLocalMoveY(0, 0.4f).SetLink(_fadeOut.gameObject);
        _fadeOut.transform.DOScale(2.6f, 0.4f).SetLink(_fadeOut.gameObject);

        Invoke("LoadGameplay", 0.4f);
    }
    private void LoadGameplay()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    public void OnPrivacyPolicyButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        Application.OpenURL("https://doc-hosting.flycricket.io/sharp-balls-privacy-policy/d2f81cf8-394f-4e05-9311-64c5d7f4abfc/privacy");
    }
    public void OnInputLanguage(int value)
    {
        LanguageManager.Instance.ChangeLanguage(value);
        DropDownInteract();
    }
    public void UpdateLanguageIcon()
    {

    }
    public void DropDownInteract()
    {
        _dropDownOpened = !_dropDownOpened;
        _dropDownPanel.gameObject.SetActive(_dropDownOpened);
        UpdateLanguageIcon();
    }
    public void OnExitButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        Application.Quit();
    }
}
