using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVibrationManager : MonoBehaviour
{
    public static AudioVibrationManager Instance;

    public Action SoundChanged;
    public Action MusicChanged;
    public Action VibrationChanged;

    public bool IsSoundEnabled { get; private set; }
    public bool IsMusicEnabled { get; private set; }
    public bool IsVibrationEnabled { get; private set; }

    [SerializeField] private string SoundGroup = "Sound";
    [SerializeField] private string MusicGroup = "Music";

    [SerializeField] private AudioMixer _mixer;

    public AudioClip ScoreAdd;
    public AudioClip Swap;
    public AudioClip Burst;
    public AudioClip PopUp;
    public AudioClip Lose;
    public AudioClip Win;
    public AudioClip ButtonClick;
    public AudioClip BallShot;
    public AudioClip BallHit;
    public AudioClip BallCompleted;


    [SerializeField] private GameObject _soundPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        Initialize();
    }
    private void Initialize()
    {
        IsSoundEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("IsSoundEnabled", 1));
        IsMusicEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("IsMusicEnabled", 1));
        IsVibrationEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("IsVibrationEnabled", 1));

        Invoke("InitializeAudioMixer", 0.1f);
    }
    private void InitializeAudioMixer()
    {
        _mixer.SetFloat(SoundGroup, -80 * Convert.ToInt32(!IsSoundEnabled));
        _mixer.SetFloat(MusicGroup, -80 * Convert.ToInt32(!IsMusicEnabled));
    }
    public void ToggleSound()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        IsSoundEnabled = !IsSoundEnabled;

        _mixer.SetFloat(SoundGroup, -80 * Convert.ToInt32(!IsSoundEnabled));

        SoundChanged?.Invoke();

        PlayerPrefs.SetInt("IsSoundEnabled", Convert.ToInt32(IsSoundEnabled));
    }
    public void ToggleMusic()
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ButtonClick, 1f, 0.4f);
        IsMusicEnabled = !IsMusicEnabled;

        _mixer.SetFloat(MusicGroup, -80 * Convert.ToInt32(!IsMusicEnabled));

        MusicChanged?.Invoke();

        PlayerPrefs.SetInt("IsMusicEnabled", Convert.ToInt32(IsMusicEnabled));
    }
    public void ToggleVibration()
    {
        IsVibrationEnabled = !IsVibrationEnabled;

        VibrationChanged?.Invoke();

        PlayerPrefs.SetInt("IsVibrationEnabled", Convert.ToInt32(IsVibrationEnabled));
    }
    public void PlaySound(AudioClip clip, float pitch = 1f, float volume = 1f)
    {
        Instantiate(_soundPrefab).GetComponent<Sound>().PlaySound(clip, pitch, volume);
    }
}
