using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

public class DogMoveSystem : ComponentSystem
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<DogMoveComponent>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Transform transform, DogMoveComponent dog) =>
        {
            var p = transform.position;
            p.y += dog.moveSpeed * Time.DeltaTime;
            transform.position = p;
        });
    }
}
