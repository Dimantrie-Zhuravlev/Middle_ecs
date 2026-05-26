using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CharacterJerkSystem : ComponentSystem
{
    private EntityQuery _jerkQuery;
    protected override void OnCreate()
    {
        _jerkQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<JerkData>(), ComponentType.ReadOnly<Transform>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_jerkQuery).ForEach((Entity entity, UserInputData inputData, ref InputData input) =>
        {
            Debug.Log(input.jerk);
            if (input.jerk > 0f && inputData.jerkAction != null && inputData.jerkAction is IAbility ability)
            {
                ability.Execute();
            }
        });
    }
}
