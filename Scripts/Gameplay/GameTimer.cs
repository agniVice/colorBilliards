using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float DefaultTimer;
    public float Timer;

    private bool _isEnabled;

    private void Awake()
    {
        if(Instance != null && Instance != this) 
            Destroy(gameObject); 
        else
            Instance = this;

        ResetTimer();
    }
    private void FixedUpdate()
    {
        if (_isEnabled)
        {
            if (Timer > 0)
            {
                Timer -= Time.fixedDeltaTime;
            }
            else
            {
                _isEnabled = false;
                GameState.Instance.FinishGame();
            }
        }
    }
    public void ResetTimer()
    {
        _isEnabled = false;
        Timer = DefaultTimer;
    }
    public void StartTimer()
    {
        _isEnabled  = true;
    }
}
