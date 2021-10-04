using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _livesText;
    private float _timer;
    
    // Start is called before the first frame update
    void Start()
    {        
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public void Timer()
    {
        _timer += Time.deltaTime;
        int seconds = (int)(_timer % 60);
        int minutes = (int)(_timer / 60);
        string _timerString = $"{minutes:00}:{seconds:00}";
        if(_timer < (60*60))
        {
            _timeText.text = "Time: " + _timerString;
        }
        else
        {
            _timeText.text = "Time: 59:59";
        }        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int playerLives)
    {
        _livesText.text = "Lives: " + playerLives.ToString();
    }
}
