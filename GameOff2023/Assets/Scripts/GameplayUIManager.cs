using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TextMeshProUGUI _UserText;
    [SerializeField] private TextMeshProUGUI _HighScoreText;
    [SerializeField] private GameObject _PauseMenu;
    [SerializeField] private Image[] BallImagesOn;
    [SerializeField] private Image[] BallImagesOff;
    private int _currentScore = 0;
    private int _currentBall = 0;
    private bool _paused = false;

    public static GameplayUIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _ScoreText.SetText("Score: " + _currentScore);
        try
        {
            _UserText.SetText(AuthenticationService.Instance.PlayerName);
        }
        catch
        {
            _UserText.SetText("Offline: No User Name");
        }
        _currentBall = 0;
    }

    private void OnEnable()
    {
        InputManager.PauseMenuTriggered += PauseMenu;
    }

    private void OnDisable()
    {
        InputManager.PauseMenuTriggered -= PauseMenu;
    }

    private void Update()
    {
        if(_currentScore != ScoreManager.score)
        {
            _currentScore = ScoreManager.score;
            _ScoreText.SetText("Score: " +  _currentScore);
        }
    }

    public void UpdateHighScore()
    {
        _HighScoreText.SetText("High Score: " + ScoreManager.HighestScore);
    }

    public void ResetBalls()
    {
       foreach(Image image in BallImagesOn)
       {
            image.enabled = true;
       }
        foreach (Image image in BallImagesOff)
        {
            image.enabled = false;
        }

        _currentBall = 0;
    }

    public void RemoveBall()
    {
        BallImagesOn[_currentBall].enabled = false;
        BallImagesOff[_currentBall].enabled = true;
        _currentBall++;
    }

    private void PauseMenu()
    {
        _paused = !_paused;

        if (_paused)
        {
            _PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
