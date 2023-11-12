using UnityEngine;
using DG.Tweening;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;

    private Vector2 _startScale;

    private void Start()
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].Initialize(this, i);
            _skins[i].UpdateMe();
        }
        int skin = PlayerPrefs.GetInt("CurrentSkin", 1);

        if(skin < _skins.Length)
            SelectLevel(_skins[skin]);
        else
            SelectLevel(_skins[1]);
    }
    public void SelectLevel(Skin skin)
    {
        PlayerPrefs.SetInt("CurrentSkin", skin.SkinId);

        AnimateSelection(skin.transform);

        foreach (Skin s in _skins)
            s.UpdateMe();
    }
    private void AnimateSelection(Transform skin)
    {
        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Swap, 1f);

        skin.DOScale(Vector3.one * 1.2f, 0.2f).SetLink(skin.gameObject).SetEase(Ease.OutBack);
        skin.DORotate(new Vector3(0, 0, 30), 0.15f).SetLink(skin.gameObject).SetEase(Ease.OutBack);
        skin.DORotate(new Vector3(0, 0, 0), 0.15f).SetLink(skin.gameObject).SetEase(Ease.OutBack).SetDelay(0.25f).OnKill(() => {
            skin.transform.localScale = Vector3.one;
            skin.transform.rotation = Quaternion.Euler(Vector3.zero);
        });
        skin.DOScale(Vector3.one, 0.15f).SetLink(skin.gameObject).SetEase(Ease.OutBack).SetDelay(0.2f);
    }
}
