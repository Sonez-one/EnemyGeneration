using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly float _speed = 5f;

    private Vector3 _direction;

    private void Update()
    {
        Move();
    }

    public void Init(Vector3 direction)
    {
        _direction = direction;
    }

    private void Move() => 
        transform.Translate(_speed * Time.deltaTime * _direction);
}
