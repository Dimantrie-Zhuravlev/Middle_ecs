using DefaultNamespace;
using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{
    public Collider Collider;
    public List<MonoBehaviour> collisionsActions = new List<MonoBehaviour>();
    public List<IAbilityTarget> collisionsActionsAbilities = new List<IAbilityTarget>();

    [HideInInspector] public List<Collider> collisions;

    private void Start()
    {
        foreach (var action in collisionsActions)
        {
            if (action is IAbilityTarget ability)
            {
                collisionsActionsAbilities.Add(ability);
            }
            else
            {
                Debug.LogError("Ошибка");
            }
        }
    }
    public void Execute()
    {
        foreach (var action in collisionsActionsAbilities)
        {
            action.Targets = new List<GameObject>();
            collisions.ForEach(c =>
            {
                if (c != null) action.Targets.Add(c.gameObject);
            });
            if (action != null)
            {
                action.Execute();
            }
  
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new LocalToWorld
        {
            Value = gameObject.transform.localToWorldMatrix
        });
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case UnityEngine.SphereCollider sphereCollider:
                sphereCollider.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderTypeEnum.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    initialTakeOff = true,
                });
                break;

            case UnityEngine.CapsuleCollider capsuleCollider:
                capsuleCollider.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderTypeEnum.Capsule,
                    CapsuleStart = capsuleStart,
                    CapsuleEnd = capsuleEnd,
                    CapsuleRadius = capsuleRadius,
                    initialTakeOff = true,
                });
                break;

            case UnityEngine.BoxCollider boxCollider:
                boxCollider.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtends, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderTypeEnum.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtends = boxHalfExtends,
                    BoxOrientation = boxOrientation,
                    initialTakeOff = true,
                });
                break;

        }
        //Collider.enabled = false;
    }
}



public struct ActorColliderData : IComponentData
{
    public ColliderTypeEnum ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtends;
    public Quaternion BoxOrientation;
    public bool initialTakeOff;
}

public enum ColliderTypeEnum
{
    Sphere = 0,
    Capsule = 1,
    Box = 2
}