using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private float _totalTime = 120f;
    [SerializeField] private int _coinsToWin = 15;

    private float _timeRemaining;
    private int _currentCoins = 0;
    private bool _gameEnded = false;

    private void Start()
    {
        _timeRemaining = _totalTime;
        _uiManager.SetGoalData(_coinsToWin, _totalTime);
        _uiManager.UpdateTimer(_timeRemaining);
    }

    void Update()
    {
        if (_gameEnded)
        {
            return;
        }

        _timeRemaining -= Time.deltaTime;
        _timeRemaining = Mathf.Max(_timeRemaining, 0f);
        _uiManager.UpdateTimer(_timeRemaining);

        if (_timeRemaining <= 0f)
        {
            EndGame(false);
        }
    }

    public void CollectCoin(int amount)
    {
        if (_gameEnded)
        {
            return;
        }

        _currentCoins += amount;
        _uiManager.AddCoins(amount);

        if (_currentCoins >= _coinsToWin)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool won)
    {
        _gameEnded = true;
        Time.timeScale = 0f;

        if (won)
        {
            _uiManager.ShowVictory();
        }
        else
        {
            _uiManager.ShowGameOver();
        }
    }
}
