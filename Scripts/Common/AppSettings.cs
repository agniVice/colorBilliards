using UnityEngine;

public class AppSettings : MonoBehaviour
{
    public static AppSettings Instance;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else 
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
