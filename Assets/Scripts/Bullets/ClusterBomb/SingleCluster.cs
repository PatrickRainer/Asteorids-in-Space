using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bullets
{
    public class SingleCluster : WeaponBase
    {
        [SerializeField] int miniClustersFlyingDistance = 10;
        [SerializeField] int miniClustersFlyingDuration = 2;

        void Start()
        {
            _rb.DOMove(Random.insideUnitCircle * miniClustersFlyingDistance,
                miniClustersFlyingDuration);
        }

        protected override void Move()
        {
           
        }

        protected override void OnDestroy()
        {
            //base.OnDestroy();
        }
    }
}