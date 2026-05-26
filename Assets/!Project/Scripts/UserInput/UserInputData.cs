using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using DefaultNamespace.Components.Interfaces;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float speed;

    public MonoBehaviour shootAction;
    public MonoBehaviour jerkAction;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData<InputData>(entity, new InputData());
        dstManager.AddComponentData<MoveData>(entity, new MoveData { Speed = speed / 100 });
        dstManager.AddComponentData<JerkData>(entity, new JerkData());

        if (shootAction != null && shootAction is IAbility)
        {
            print(11);
            dstManager.AddComponentData<ShootData>(entity, new ShootData());
        }
    }
}

public struct InputData : IComponentData
{
    public float2 move;
    public float shoot;
    public float jerk;
}
public struct MoveData : IComponentData
{
    public float Speed;
}
public struct ShootData : IComponentData
{
}
public struct JerkData : IComponentData
{
    public float JerkSpeed;
}