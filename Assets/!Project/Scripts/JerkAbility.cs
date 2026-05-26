using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerkAbility : MonoBehaviour, IAbility
{
    private float bigSpeed = 1f;
    public void Execute()
    {
        Debug.Log("Jerk activate");
    }
}
