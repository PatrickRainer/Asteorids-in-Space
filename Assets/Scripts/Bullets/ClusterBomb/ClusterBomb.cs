using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bullets
{
    public class ClusterBomb : WeaponBase
    {
        [SerializeField] [Required] [AssetsOnly]
        GameObject miniCluster;

        [SerializeField] [Required] List<Transform> miniClusterPositions = new List<Transform>();
        [SerializeField] int flyingDistance = 2;

        [SerializeField] int clusterBombFlyingDuration = 1;

        Vector2 _endPosition; //Should move until this position and then stop


        protected override void Awake()
        {
            // Set Start and Target position
            var position = transform.position;
            _endPosition = position + new Vector3(0, flyingDistance);

            base.Awake();
        }

        protected override void OnDestroy()
        {
            gameObject.SetActive(false);

            //Create mini clusters
            foreach (var trf in miniClusterPositions)
            {
                var unused = Instantiate(miniCluster, trf.position, Quaternion.identity);
            }

            base.OnDestroy();
        }

        protected override void Move()
        {
            _rb.DOMove(_endPosition, clusterBombFlyingDuration);
        }
    }
}