using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDPanel : MonoBehaviour
{
    [Header ("Health")]
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


    public void SetGoalData(int coinGoal, float totalTime)
    {
        _coinGoal = coinGoal;
        _totalTime = Mathf.Max(0.0001f, totalTime);
        UpdateCoinUI();
    }


    public void UpdateHealth(int current, int max)
    {
        if (_healthText != null)
        {
            _healthText.text = $"HP: {current}/{max}";
        }

        if (_healthBarFill != null)
        {
            float fill = (max > 0) ? (float)current / max : 0f;
            _healthBarFill.fillAmount = Mathf.Clamp01(fill);
        }
    }

    public void AddCoins(int amount)
    {
        _coinCount += amount;
        if (_coinCount < 0)
        {
            _coinCount = 0;
        }

        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (_coinText != null)
        {
            _coinText.text = $"Coins: {_coinCount}/{_coinGoal}";
        }

        if (_coinProgressFill != null)
        {
            float fill = (_coinGoal > 0) ? (float)_coinCount / _coinGoal : 0f;
            _coinProgressFill.fillAmount = Mathf.Clamp01(fill);
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
            float fill = (_totalTime > 0f) ? timeRemaining / _totalTime : 0f;
            _timerFill.fillAmount = Mathf.Clamp01(fill);
        }
    }

}
