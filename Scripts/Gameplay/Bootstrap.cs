using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<GameObject> _initializableSystemsPrefabs = new List<GameObject>();

    private List<IInitializable> _initializableSystems = new List<IInitializable>();
    private List<ISubscriber> _subscribers = new List<ISubscriber>();

    private void Awake()
    {
        SpawnAllSystems();
        SubscribeAllSystems();
        InitializeAllSystems();
    }
    private void SpawnAllSystems()
    {
        foreach (GameObject gameObject in _initializableSystemsPrefabs)
        { 
            var spawnedObject = Instantiate(gameObject);

            if(spawnedObject.TryGetComponent(out IInitializable initializable))
                _initializableSystems.Add(spawnedObject.GetComponent<IInitializable>());

            if (spawnedObject.TryGetComponent(out ISubscriber subscriber))
                _subscribers.Add(spawnedObject.GetComponent<ISubscriber>());
        }
    }
    private void SubscribeAllSystems()
    {
        foreach (ISubscriber subscriber in _subscribers)
            subscriber.SubscribeAll();
    }
    private void InitializeAllSystems()
    {
        foreach (IInitializable system in _initializableSystems)
            system.Initialize();
    }
}
