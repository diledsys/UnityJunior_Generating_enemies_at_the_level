using UnityEngine;

public sealed class EnemyMover : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float MinDirectionSqrMagnitude = 0.0001f;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _turnSpeedDeg = 720f;
    [SerializeField] private float _walkAnimSpeedValue = 1f;
    [SerializeField] private Animator _animator;

    private Vector3 _moveDirection;
    private bool _isMoving;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!_isMoving)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeedDeg * Time.deltaTime);

        transform.Translate(_moveDirection * ( _moveSpeed * Time.deltaTime ), Space.World);

        if (_animator != null)
            _animator.SetFloat(SpeedHash, _walkAnimSpeedValue);
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < MinDirectionSqrMagnitude)
            return;

        _moveDirection = direction.normalized;
        _isMoving = true;

        if (_animator != null)
            _animator.SetFloat(SpeedHash, _walkAnimSpeedValue);
    }

    public void StopMoving()
    {
        _isMoving = false;

        if (_animator != null)
            _animator.SetFloat(SpeedHash, 0f);
    }
}
