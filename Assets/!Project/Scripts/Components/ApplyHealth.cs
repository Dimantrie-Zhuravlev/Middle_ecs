using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ApplyHealth : MonoBehaviour, IAbilityTarget
{
    public int Health = 200;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var health = target.GetComponent<CharacterHealth>();
            if (health != null)
            {
                health.Health += Health;
                gameObject.SetActive(false);
            }
        }
    }
}
