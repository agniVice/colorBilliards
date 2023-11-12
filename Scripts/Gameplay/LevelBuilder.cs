using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private List<GameObject> _levelPrefabs = new List<GameObject>();
    private GameObject _level;
    private void Awake()
    {
        BuildLevel();
    }
    public void Initialize()
    {
        //BuildLevel();
    }
    public void SubscribeAll()
    {
        //GameState.Instance.GameStarted += BuildLevel;
    }

    public void UnsubscribeAll()
    {
        //GameState.Instance.GameStarted -= BuildLevel;
    }

    private void BuildLevel()
    {
        _level = Instantiate(_levelPrefabs[PlayerPrefs.GetInt("CurrentLevel")]);
    }
    private void RebuildLevel()
    {
        ClearLevel();
        Invoke("BuildLevel", 1.5f);
    }
    private void ClearLevel()
    {
        Destroy(_level);
        _level = null;
    }
}
