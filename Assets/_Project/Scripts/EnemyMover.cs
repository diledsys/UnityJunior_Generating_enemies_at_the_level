using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _lifeTime = 55f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _walkSpeedParam = 3f;
    [SerializeField] private float _turnSpeed = 720f;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private Vector3 _moveDirection = Vector3.forward;
    private bool _hasDirection;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
        UpdateAnimationSpeed();
    }

    public void SetMoveDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.0001f)
            return;

        _moveDirection = direction.normalized;
        _hasDirection = true;

        UpdateAnimationSpeed();
    }

    private void Update()
    {
        if (!_hasDirection)
            return;

        Quaternion targetRot = Quaternion.LookRotation(_moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRot,_turnSpeed * Time.deltaTime);

        float delta = _moveSpeed * Time.deltaTime;
        transform.Translate(_moveDirection * delta, Space.World);
    }

    private void UpdateAnimationSpeed()
    {
        if (_animator == null)
            return;

        float speedValue = _hasDirection ? _walkSpeedParam : 0f;
        _animator.SetFloat(SpeedHash, speedValue);
    }
}
