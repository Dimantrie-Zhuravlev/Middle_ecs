using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery _collisionQuery;
        private Collider[] _results = new Collider[50];

        protected override void OnCreate()
        {
            _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_collisionQuery).ForEach((Entity entity, CollisionAbility abilityCollision, ref ActorColliderData colliderData) =>
            {
                var gameObject = abilityCollision.gameObject;
                float3 position = gameObject.transform.position;
                Quaternion rotation = gameObject.transform.rotation;


                abilityCollision.collisions?.Clear();

                int size = 0;

                switch (colliderData.ColliderType)
                {
                    case ColliderTypeEnum.Sphere:
                        size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, _results);
                        break;
                    case ColliderTypeEnum.Capsule:
                        var center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                        var point1 = colliderData.CapsuleStart + position;
                        var point2 = colliderData.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, _results);
                        break;
                    case ColliderTypeEnum.Box:
                        size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtends, _results, colliderData.BoxOrientation * rotation);
                        break;
                }

                if (size > 1)
                {
                    foreach (var result in _results)
                    {
                        abilityCollision?.collisions?.Add(result);
                    }
                    abilityCollision.Execute();
                }
            });
        }
    }

}
