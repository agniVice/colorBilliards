using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    [SerializeField] private List<Ball> _balls = new List<Ball>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void Remove(Ball ball)
    {
        _balls.Remove(ball);
        CheckForFinish();
    }
    private void CheckForFinish()
    {
        if (_balls.Count == 0)
            GameState.Instance.FinishGame();
    }
}
