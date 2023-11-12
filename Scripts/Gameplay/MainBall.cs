using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MainBall : MonoBehaviour
{
    [Header("Prefabs")]

    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _particlePrefab;

    [Space]
    [Header("BallSettings")]

    [SerializeField] private float _speed = 20f;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;
    private LineRenderer _lineRenderer;

    private Vector3 _direction;

    private bool _isMoving = false;
    private bool _isAimed = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _lineRenderer = GetComponent<LineRenderer>();

        SpawnLine();
    }
    private void OnEnable()
    {
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
        PlayerInput.Instance.PlayerMouseUp += OnPlayerMouseUp;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
        PlayerInput.Instance.PlayerMouseUp += OnPlayerMouseUp;
    }
    private void SpawnLine()
    {
        _lineRenderer = Instantiate(_linePrefab).GetComponent<LineRenderer>();
        _lineRenderer.GetComponent<LineMovement>().Ball = transform;
    }
    private void FixedUpdate()
    {
        Stop();
        UpdateTrajectory();
    }
    private void Stop()
    {
        if (_isMoving)
        {
            _rigidbody.velocity *= 0.99f;
        }
    }
    private void UpdateTrajectory()
    {
        if (_isAimed)
        {
            Vector2 differenve = _lineRenderer.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _direction = differenve.normalized;
            _lineRenderer.SetPosition(1, _direction * 2f);
        }
    }
    private void HideTrajectory()
    {
        _lineRenderer.SetPosition(1, Vector2.zero);

        _isAimed = false;
    }
    private void OnPlayerMouseDown()
    {
        if (GameState.Instance.CurrentState == GameState.State.InGame)
        {
            _isMoving = false;
            _rigidbody.velocity = Vector2.zero;
            _isAimed = true;
        }
    }
    private void OnPlayerMouseUp()
    {
        if (GameState.Instance.CurrentState == GameState.State.InGame)
        {
            HideTrajectory();
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallShot, 1);
            _rigidbody.AddForce(_direction.normalized * _speed);
            _isMoving = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallHit, Random.Range(0.9f, 1.1f));

            //var direction = _rigidbody.velocity.normalized;
            //_rigidbody.velocity = Vector3.Reflect(_rigidbody.velocity, collision.contacts[0].normal);

            //_rigidbody.velocity = Vector2.zero;

            //_rigidbody.AddForce(direction);
        }
    }
}
