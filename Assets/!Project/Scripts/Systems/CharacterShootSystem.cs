using DefaultNamespace.Components.Interfaces;
using Unity.Entities;
using UnityEngine;

public class CharacterShootSystem : ComponentSystem
{
    private EntityQuery _shootQuery;
    protected override void OnCreate()
    {
        _shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<ShootData>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_shootQuery).ForEach((Entity entity, UserInputData inputData, ref InputData input) =>
        {
            if (input.shoot > 0f && inputData.shootAction != null && inputData.shootAction is IAbility ability)
            {
                Debug.Log(input.shoot);
                ability.Execute();
            }
        });
    }
}
