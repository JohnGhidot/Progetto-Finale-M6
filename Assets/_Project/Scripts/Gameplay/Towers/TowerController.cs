using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private ProjectileType _type = ProjectileType.Simple;

    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private PoolManager _poolManager;

    private float _attackTimer = 1f;

    void Update()
    {
        if (_targetPlayer == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _targetPlayer.position);

        if (distanceToPlayer <= _attackRange && _attackTimer <= 0f)
        {
            FireProjectile();
            _attackTimer = _attackCooldown;
        }

        _attackTimer -= Time.deltaTime;
    }

    private void FireProjectile()
    {
        Vector3 dir = (_targetPlayer.position - _firePoint.position).normalized;

        GameObject projectile = _poolManager.SpawnFromPool(_type, _firePoint.position, Quaternion.LookRotation(dir));        
            
         if (projectile == null)
         {
             return;
         }

         Rigidbody rb = projectile.GetComponent<Rigidbody>();
         if (rb != null)
         {
             float projectileSpeed = 15f;
             rb.velocity = dir * projectileSpeed;
         }        
    }
}
