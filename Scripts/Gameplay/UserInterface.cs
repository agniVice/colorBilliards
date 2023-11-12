using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour, IInitializable
{
    public static UserInterface Instance;

    [SerializeField] private List<GameObject> _panelPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void Initialize()
    {
        foreach (GameObject panel in _panelPrefabs)
        {
            var spawnedPanel = Instantiate(panel);

            if (spawnedPanel.TryGetComponent<ISubscriber>(out ISubscriber subscriber))
                subscriber.SubscribeAll();

            if (spawnedPanel.TryGetComponent<IInitializable>(out IInitializable initializable))
                initializable.Initialize();
        }
    }
}