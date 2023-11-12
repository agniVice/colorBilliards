using UnityEngine;
using UnityEngine.UI;

public class BorderGenerator : MonoBehaviour
{
    [SerializeField] private bool _isTrigger = false;
    private EdgeCollider2D _triggerCollider;

    private Image _image; // —сылка на изображение в UI
    private EdgeCollider2D _edgeCollider; // —сылка на компонент EdgeCollider2D

    private RectTransform _imageRectTransform;
    private Vector3[] _imageCorners = new Vector3[4];

    private void Start()
    {
        _triggerCollider = GameObject.FindGameObjectWithTag("Edge").GetComponent<EdgeCollider2D>();
        _image = GetComponent<Image>();
        _edgeCollider = GetComponent<EdgeCollider2D>();

        if (_image == null || _edgeCollider == null)
            return;

        _imageRectTransform = _image.GetComponent<RectTransform>();

        UpdateEdgeCollider();
    }
    void UpdateEdgeCollider()
    {
        _imageRectTransform.GetWorldCorners(_imageCorners);
        Vector2[] edgePoints = new Vector2[5];

        for (int i = 0; i < 4; i++)
        {
            edgePoints[i] = _edgeCollider.transform.InverseTransformPoint(_imageCorners[i]);
        }

        edgePoints[4] = edgePoints[0];

        _edgeCollider.points = edgePoints;

        if (_isTrigger)
            _triggerCollider.points = edgePoints;
    }
}
