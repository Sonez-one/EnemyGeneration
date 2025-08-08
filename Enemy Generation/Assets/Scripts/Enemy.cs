using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private readonly float _speed = 3.8f;

    private Vector3 _startPosition;
    private Rigidbody _rigidbody;
    private Target _target;

    public Vector3 StartPosition => _startPosition;

    public event Action<Enemy> ReachedTarget;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Target>(out Target target))
        {
            if (target.StartPosition == _startPosition) 
            {
                ReachedTarget?.Invoke(this);
            }
        }
    }

    public void Init(Vector3 startPositon, Target target)
    {
        _startPosition = startPositon;
        transform.position = _startPosition;
        _target = target;
    }

    public IEnumerator Move()
    {
        while (enabled)
        {
            transform.LookAt(_target.transform);
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            
            yield return null;
        }
    }
}
