using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _victoryPanel;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthBarFill;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Image _coinProgressFill;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _timerFill;

    private int _coinCount = 0;
    private int _coinGoal = 0;
    private float _totalTime = 1f;

    private void Awake()
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }

        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Start()
    {
        UpdateCoin();
    }

    public void SetGoalData(int coinGoal, float totalTime)
    {
        _coinGoal = coinGoal;
        _totalTime = totalTime;
    }

    public void UpdateHealth(int current, int max)
    {
        if (_healthText != null)
        {
            _healthText.text = $"HP: {current}/{max}";
        }

        if (_healthBarFill != null)
        {
            _healthBarFill.fillAmount = (float)current / max;
        }
    }

    public void AddCoins(int amount)
    {
        _coinCount += amount;
        UpdateCoin();
    }

    private void UpdateCoin()
    {
        if (_coinText != null)
        {
            _coinText.text = $"{_coinCount}/{_coinGoal}";
        }

        if (_coinProgressFill != null)
        {
            if (_coinGoal > 0)
            {
                _coinProgressFill.fillAmount = (float)_coinCount / _coinGoal;
            }
            else
            {
                _coinProgressFill.fillAmount = 0f;
            }
        }
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (_timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            _timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }

        if (_timerFill != null)
        {
            if (_totalTime > 0f)
            {
                _timerFill.fillAmount = timeRemaining / _totalTime;
            }
            else
            {
                _timerFill.fillAmount = 0f;
            }
        }
    }

    public void ShowGameOver()
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowVictory()
    {
        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
