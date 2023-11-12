using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, IInitializable, ISubscriber
{ 
    public static PlayerInput Instance;

    public Action PlayerMouseDown;
    public Action PlayerMouseUp;

    public bool IsEnabled;// { get; private set; }

    private bool _isInitialized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
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
        SubscribeAll();
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameStarted += EnableInput;
        GameState.Instance.GameUnpaused += EnableInput;
        GameState.Instance.GamePaused += DisableInput;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GameStarted -= EnableInput;
        GameState.Instance.GameUnpaused -= EnableInput;
        GameState.Instance.GamePaused -= DisableInput;
    }
    public void EnableInput()
    { 
        IsEnabled= true;
    }
    public void DisableInput() 
    {
        IsEnabled = false;
    }
    private void OnMouseDown()
    {
        if (!IsEnabled)
            return;

        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        PlayerMouseDown?.Invoke();   
    }
    private void OnMouseUp()
    {
        if (!IsEnabled)
            return;

        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        PlayerMouseUp?.Invoke();
    }
}