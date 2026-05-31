using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Settings settings;
    [HideInInspector] public int Health = 100;

    private void Start()
    {
        Health = settings.HeroHealth;
    }
}
