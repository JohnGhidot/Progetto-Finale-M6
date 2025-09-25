using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour


{

    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _victoryPanel;

    private GameOverPanel _gameOverComp;
    private VictoryPanel _victoryComp;

    [Header("HUD Panel")]
    [SerializeField] private HUDPanel _hudPanel;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;


        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
            _gameOverComp = _gameOverPanel.GetComponent<GameOverPanel>();
        }

        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(false);
            _victoryComp = _victoryPanel.GetComponent<VictoryPanel>();
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

    public HUDPanel HUD { get { return _hudPanel; } }
    public GameOverPanel GameOver { get { return _gameOverComp; } }
    public VictoryPanel Victory { get { return _victoryComp; } }

    public void SetGoalData(int coinGoal, float totalTime)
    {
        if (_hudPanel != null)
        {
            _hudPanel.SetGoalData(coinGoal, totalTime);
        }
    }

    public void UpdateHealth(int current, int max)
    {
        if (_hudPanel != null)
        {
            _hudPanel.UpdateHealth(current, max);
        }
    }

    public void AddCoins(int amount)
    {
        if (_hudPanel != null)
        {
            _hudPanel.AddCoins(amount);
        }
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (_hudPanel != null)
        {
            _hudPanel.UpdateTimer(timeRemaining);
        }
    }

    public void ShowGameOver()
    {
        if (_gameOverComp != null)
        {
            _gameOverComp.Show();
        }
        else if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowVictory()
    {
        if (_victoryComp != null)
        {
            _victoryComp.Show();
        }
        else if (_victoryPanel != null)
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
