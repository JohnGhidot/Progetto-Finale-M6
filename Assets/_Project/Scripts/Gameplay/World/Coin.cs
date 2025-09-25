using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_gameManager != null)
            {
                _gameManager.CollectCoin(1);
            }
            Destroy(gameObject);
        }
    }
}
