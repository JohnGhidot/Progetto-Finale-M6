using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifeTime = 5f;

    private float _timer;

    private void OnEnable()
    {
        _timer = _lifeTime;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController health = other.GetComponent<LifeController>();
            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }

        if (!other.isTrigger)
        {
            gameObject.SetActive(false);
        }
    }
}

