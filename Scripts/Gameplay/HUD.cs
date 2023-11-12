using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class HUD : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _levelNumberText;

    [SerializeField] private Image _fadeIn;
    [SerializeField] private Image _fadeOut;

    private bool _isInitialized;

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        SubscribeAll();
    }
    private void OnDisable()
    {
        UnsubscribeAll();
    }
    public void Initialize()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;

        Show();

        _fadeOut.gameObject.SetActive(true);
        _fadeOut.transform.localPosition = Vector3.zero;
        _fadeOut.rectTransform.DOLocalMoveY(2500, 0.4f).SetLink(_fadeOut.gameObject);

        _isInitialized = true;

        _levelNumberText.text = "LEVEL " + (PlayerPrefs.GetInt("CurrentLevel", 0) +1);

        UpdateHeader();
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameLosed += Hide;
        GameState.Instance.GameFinished += Hide;

        GameState.Instance.Shot += UpdateHeader;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GameLosed -= Hide;
        GameState.Instance.GameFinished -= Hide;

        GameState.Instance.Shot -= UpdateHeader;
    }
    private void UpdateHeader()
    {

    }
    private void Show()
    {
        _panel.SetActive(true);
    }
    private void Hide()
    {
        _panel.SetActive(false);
    }
    public void OnRestartButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        AnimateIn();
        Invoke("LoadGamePlay", 0.4f);
    }
    public void OnPauseButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        GameState.Instance.PauseGame();
    }
    private void AnimateIn()
    {
        _fadeIn.gameObject.SetActive(true);
        _fadeIn.transform.localScale = Vector3.zero;
        _fadeIn.transform.localPosition = new Vector3(0, -2500, 0);
        _fadeIn.rectTransform.DOLocalMoveY(0, 0.4f).SetLink(_fadeIn.gameObject);
        _fadeIn.transform.DOScale(2.6f, 0.4f).SetLink(_fadeIn.gameObject);
    }
    public void OnHomeButtonClicked()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        AnimateIn();
        Invoke("LoadMenu", 0.4f);
    }
    private void LoadMenu()
    {
        SceneLoader.Instance.LoadScene("Menu");
    }
    private void LoadGamePlay()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
}