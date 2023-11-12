using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    private void Start()
    {
        PlayerPrefs.SetInt("MaxLevels", (_levels.Length - 1));
        PlayerPrefs.SetInt("Level0", 1);

        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].Initialize(this, Convert.ToBoolean(PlayerPrefs.GetInt("Level" + i, 0)), i);
        }
    }
    public void SelectLevel(Level level)
    {
        if (!level.Unlocked)
        {
            level.transform.DOShakeRotation(1f, 10).SetUpdate(true).SetLink(level.gameObject).OnKill(() => {
                level.transform.DORotate(Vector3.zero, 0.1f).SetLink(level.gameObject);
            });
            return;
        }

        PlayerPrefs.SetInt("CurrentLevel", level.LevelId);

        //level.HideAll();
        //AnimateLoading(level.transform);

        FindObjectOfType<MenuUserInterface>().OnPlayButtonClicked();
        //Invoke("LoadLevel", 1.2f);
    }
    private void AnimateLoading(Transform level)
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Swap, 1f);

        level.transform.SetAsLastSibling();
        level.GetComponent<Image>().DOColor(Color.black, 0.7f).SetLink(level.gameObject).SetDelay(0.2f);
        level.transform.DORotate(new Vector3(0, 0, 90), 0.5f).SetLink(level.gameObject);
        level.transform.DOScale(10, 1f).SetLink(level.gameObject).SetDelay(0.2f);
        level.transform.DOMove(Vector3.zero, 0.2f).SetLink(level.gameObject);
    }
    private void LoadLevel()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
}
