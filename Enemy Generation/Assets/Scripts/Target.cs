using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    private readonly float _speed = 3.7f;

    private List<WayPoint> _wayPoints;
    private Vector3 _startPosition;
    private Vector3 _nextWayPointPosition;
    private Rigidbody _rigidbody;

    public Vector3 StartPosition => _startPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    public void Init(Vector3 startPosition, List<WayPoint> wayPoints)
    {
        _startPosition = startPosition;
        transform.position = _startPosition;
        _wayPoints = wayPoints;
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            _nextWayPointPosition = RandomiseNextWayPoint().transform.position;

            while (transform.position != _nextWayPointPosition)
            {
                transform.LookAt(_nextWayPointPosition);
                transform.position = Vector3.MoveTowards(transform.position, _nextWayPointPosition, _speed * Time.deltaTime);

                yield return null;
            }
        }
    }

    private WayPoint RandomiseNextWayPoint()
    {
        int wayPointIndex = Random.Range(0, _wayPoints.Count);

        return _wayPoints[wayPointIndex];
    }
}
