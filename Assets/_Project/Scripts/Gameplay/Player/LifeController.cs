using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 130;
    [SerializeField] private UIManager _uiManager;

    private int _currentHealth;
    private bool _isDead = false;

    void Start()
    {
        _currentHealth = _maxHealth;
        if (_uiManager != null)
        {
            _uiManager.UpdateHealth(_currentHealth, _maxHealth);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (_isDead)
        {
            return;
        }

        _currentHealth -= dmg;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        if (_uiManager != null)
        {
            _uiManager.UpdateHealth(_currentHealth, _maxHealth);
        }

        if (_currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        _isDead = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        if (_uiManager != null)
        {
            _uiManager.ShowGameOver();
        }
    }
}
