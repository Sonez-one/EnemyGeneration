using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangingEnemy : Enemy
{
    private readonly float _changeRate = 1f;
    private readonly float _minSize = 0.5f;
    private readonly float _maxSize = 3f;

    private bool _resizingUp = true;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeSize());
    }

    private void OnDisable()
    {
        StopCoroutine(ChangeSize());
    }

    private IEnumerator ChangeSize()
    {
        while (enabled)
        {
            if (_resizingUp)
            {
                transform.localScale += _changeRate * Time.deltaTime * Vector3.one;

                if (transform.localScale.x >= _maxSize)
                {
                    _resizingUp = false;
                }
            }
            else
            {
                transform.localScale -= _changeRate * Time.deltaTime * Vector3.one;

                if (transform.localScale.x <= _minSize)
                {
                    _resizingUp = true;
                }
            }

            yield return null;
        }
    }
}
