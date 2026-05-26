using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerkAbility : MonoBehaviour, IAbility
{
    public void Execute()
    {
        print("Jerk work");
    }
}
