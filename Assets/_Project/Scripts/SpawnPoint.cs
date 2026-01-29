using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _direction;

    public Vector3 Position => transform.position;

    public Vector3 MoveDirection
    {
        get
        {
            if (_direction == null)
                return transform.forward;

            Vector3 dir = ( _direction.position - transform.position );
            return dir.sqrMagnitude > 0.0001f ? dir.normalized : transform.forward;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 dir = MoveDirection;
        Gizmos.DrawLine(transform.position, transform.position + dir * 2f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
