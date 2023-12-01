using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Leaderboards.Models;
public class LeaderboardUIManager : MonoBehaviour
{
    [SerializeField] private string _leaderboardID;
    [SerializeField] private int _maxLeaderboardSize = 100;
    [SerializeField] private GameObject _scoreRowPrefab;
    [SerializeField] private Transform _scoreRowParent;
    [SerializeField] private GameObject _LoadingPanel;
    [SerializeField] private GameObject _NewHighScore;

    public LeaderboardScoresPage Scores = null;

    public static LeaderboardUIManager Instance { get; private set; }
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

        GetPlayerScore();
    }

    public async void AddAndPullLeaderboard(float score)
    {
        await LeaderboardClientAPI.AddScore(score, _leaderboardID);

        // Only pulls leaderboard if new score should be on it or if it hasn't been pulled before
        if (Scores == null)
        {
            PopulateLeaderboard();
        }
        else if(CheckPullLeaderboard(score))
        {
            PopulateLeaderboard();
        }
    } 

    private bool CheckPullLeaderboard(float score)
    {
        bool pull = false;
        if (Scores != null)
        {
            foreach(LeaderboardEntry entry in Scores.Results)
            {
                if (score > entry.Score)
                {
                    pull = true;
                }
            }
        }
        return pull;
    }

    public async void AddPlayerScore(float score)
    {
        await LeaderboardClientAPI.AddScore(score, _leaderboardID);
    }
    public async void GetPlayerScore()
    {
        var highscore = await LeaderboardClientAPI.GetPlayerScore(_leaderboardID);
        if ((int)highscore > ScoreManager.HighestScore) ScoreManager.HighestScore = (int)highscore;
    }

    public async void PopulateLeaderboard()
    {
        Scores = await LeaderboardClientAPI.GetScores(0, _maxLeaderboardSize, _leaderboardID);

        if (Scores != null)
        {
            // clear rows
            foreach (Transform child in _scoreRowParent)
            {
                Destroy(child.gameObject);
            }

            // populate leaderboard
            for (int i = 0; i < Scores.Total; i++)
            {
                GameObject row = Instantiate(_scoreRowPrefab, _scoreRowParent);
                RowUIController rc = row.GetComponent<RowUIController>();
                rc.SetName(Scores.Results[i].PlayerName);
                rc.SetScore(Scores.Results[i].Score.ToString());
            }
        }
    }

    public void ShowLoading(bool show)
    {
        _LoadingPanel.SetActive(show);
    }

    public void ShowNewHighScore(bool show)
    {
        _NewHighScore.SetActive(show);
    }

    public void Restart()
    {
        GameplayManager.Instance.RestartGame();
    }
}
