using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class JerkAbility : MonoBehaviour, IAbility
{
    private float _jerkSpeed = 4f;
    private float animationDuration = 1f;
    private Coroutine jerkAnimation = null;
    public void Execute()
    {
        if (jerkAnimation == null)
        {
            jerkAnimation = StartCoroutine(jerkPlayer());
        }
    }

    public void Update()
    {
        if (jerkAnimation == null) return;

        var pos = transform.position;
        pos += new Vector3(0, 0, _jerkSpeed * Time.deltaTime);
        transform.position = pos;
    }

    private IEnumerator jerkPlayer()
    {
        yield return new WaitForSeconds(animationDuration);
        jerkAnimation = null;
    }
}
