using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public static class LeaderboardClientAPI
{
    /// <summary>
    /// Add a score to the leaderboard. Returns a LeaderboardEntry if successfull or null if not.
    /// </summary>
    public static async Task<LeaderboardEntry> AddScore(float score, string LeaderboardID)
    {
        try
        {
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardID, score);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
            return scoreResponse;
        }
        catch (Exception e)
        {
            Debug.Log("Error adding scores: " + e);
            return null;
        }
    }

    /// <summary>
    /// Get player score from online leaderboard if it exists. Otherwise 0.
    /// </summary>
    public static async Task<double> GetPlayerScore(string LeaderboardID)
    {
        LeaderboardEntry scoreResponse = null;
        try
        {
            scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardID);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }
        catch (Exception e)
        {
            Debug.Log("Error getting player score: " + e);
            scoreResponse = null;
        }

        if (scoreResponse != null)
        {
            return scoreResponse.Score;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Returns scores from leaderboard starting from "offset" until the limit is reached. Returns null if fails.
    /// </summary>
    public static async Task<LeaderboardScoresPage> GetScores(int offset, int limit, string LeaderboardID)
    {
        try
        {
            GetScoresOptions options = new GetScoresOptions { Offset = offset, Limit = limit };
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardID, options);
            Debug.Log(JsonConvert.SerializeObject(scoresResponse));
            return scoresResponse;
        }
        catch (Exception e)
        {
            Debug.Log("Error getting scores: " + e);
            return null;
        }
    }
}


