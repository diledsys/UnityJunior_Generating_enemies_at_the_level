using UnityEngine;

public sealed class SpawnPoint : MonoBehaviour
{
    private const float MinDirectionSqrMagnitude = 0.0001f;

    [SerializeField] private Transform _directionTarget;

    public Vector3 Position => transform.position;

    public Vector3 Direction
    {
        get
        {
            if (_directionTarget == null)
                return transform.forward;

            Vector3 toTarget = _directionTarget.position - transform.position;

            if (toTarget.sqrMagnitude < MinDirectionSqrMagnitude)
                return transform.forward;

            return toTarget.normalized;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawLine(transform.position, transform.position + Direction * 2f);
    }
}
