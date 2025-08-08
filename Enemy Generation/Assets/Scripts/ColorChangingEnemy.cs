using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingEnemy : Enemy
{
    private readonly float _changeRate = 0.5f;

    private ColorChanger _colorChanger;

    protected override void Awake()
    {
        base.Awake();

        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeColor());
    }

    private void OnDisable()
    {
        StopCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        WaitForSeconds wait = new(_changeRate);

        while (enabled)
        {
            _colorChanger.ChangeColor();

            yield return wait;
        }
    }
}
