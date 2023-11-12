using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseWindow : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private List<Transform> _transforms = new List<Transform>();

    [SerializeField] private TextMeshProUGUI _score;


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

        Hide();

        _isInitialized = true;
    }
    public void SubscribeAll()
    {
        GameState.Instance.GamePaused += Show;
        GameState.Instance.GameUnpaused += Hide;
        GameState.Instance.GameFinished += Hide;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GamePaused -= Show;
        GameState.Instance.GameUnpaused -= Hide;
        GameState.Instance.GameFinished -= Hide;
    }
    private void Show()
    {

        _score.text = PlayerScore.Instance.Score.ToString();

        foreach (Transform transform in _transforms)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
        _panel.SetActive(true);
    }
    private void Hide()
    {
        _panel.SetActive(false);
    }
    public void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
        GameState.Instance.UnpauseGame();
    }
    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadScene("Menu");
    }
}