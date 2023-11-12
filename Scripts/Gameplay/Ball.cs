using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    private Rigidbody2D _rigidBody;
    private bool _isOnEdge = false;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rigidBody.velocity *= 0.99f;
    }
    private void DestroyBall()
    {
        transform.DOScale(0, 0.15f).SetEase(Ease.InBack).SetLink(gameObject);

        BallManager.Instance.Remove(this);

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallCompleted, Random.Range(0.9f, 1.1f));

        Camera.main.DOShakePosition(0.4f, 0.2f, fadeOut: true).SetUpdate(true);
        Camera.main.DOShakeRotation(0.4f, 0.2f, fadeOut: true).SetUpdate(true);

        SpawnParticle();
        Destroy(gameObject, 0.3f);
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();
        particle.transform.position = transform.position;
        particle.Play();

        Destroy(particle.gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameState.Instance.CurrentState == GameState.State.InGame)
        {
            if (collision.CompareTag("Finish"))
                DestroyBall();
        }
        if (collision.CompareTag("Edge"))
        {
            _isOnEdge = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Edge"))
            _isOnEdge = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallHit, Random.Range(0.9f, 1.1f));
            //_rigidBody.velocity = Vector3.Reflect(_rigidBody.velocity, collision.contacts[0].normal);
        }
        if (collision.gameObject.CompareTag("MainBall"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallHit, Random.Range(0.9f, 1.1f));
            if (_isOnEdge)
            {
                _rigidBody.velocity = Vector3.Reflect(collision.gameObject.GetComponent<Rigidbody2D>().velocity, collision.contacts[0].normal);
            }
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallHit, Random.Range(0.9f, 1.1f));
        }
    }
}
