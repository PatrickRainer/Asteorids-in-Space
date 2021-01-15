using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Bullets
{
    public class ClusterBomb : WeaponBase
    {
        [SerializeField] [Required] [AssetsOnly]
        GameObject miniCluster;
        
        [SerializeField] [Required]  [SceneObjectsOnly]
        List<Transform> miniClusterPositions = new List<Transform>(); 

        Vector2 _startPosition;
        Vector2 _endPosition; //Should move until this position and then stop
        float percentOfWay = 0;

        protected override void Awake()
        {
            // Set Start and Target position
            var position = transform.position;
            _startPosition = position;
            _endPosition = position + new Vector3(0, 2);

            base.Awake();
        }

        protected override void OnDestroy()
        {
            //Create mini clusters
            foreach (var position in miniClusterPositions)
            {
                var go = Instantiate(miniCluster, position.position, Quaternion.identity);
            }

            _rb.AddExplosionForce(3, transform.position, 3);

            base.OnDestroy();
        }

        protected override void Move()
        {
            _rb.DOMove(_endPosition, 2);
        }
    }
}