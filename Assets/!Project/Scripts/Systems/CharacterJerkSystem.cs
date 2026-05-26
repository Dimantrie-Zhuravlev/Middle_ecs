using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterJerkSystem : ComponentSystem
{
    private EntityQuery _jerkQuery;
    protected override void OnCreate()
    {
        _jerkQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<JerkData>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_jerkQuery).ForEach((Entity entity, UserInputData inputData, ref InputData input) =>
        {
            if (input.jerk > 0f && inputData.jerkAction != null && inputData.jerkAction is IAbility ability)
            {
                Debug.Log("Jerk ability");
                ability.Execute();
            }
        });
    }
}
