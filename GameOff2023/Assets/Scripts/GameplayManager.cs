using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int StartBalls = 3;
    public TableLightTrigger TLT;
    public DropTarget DT;
    private int _currentBalls = 3;
    public GameObject ball;
    public GameObject ballSpawnLoc;

    public static GameplayManager Instance { get; private set; }
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
        RestartGame();
    }

    public void RestartGame()
    {
        LeaderboardUIManager.Instance.gameObject.SetActive(false);
        _currentBalls = StartBalls;
        ScoreManager.score = 0;
        AudioManager.instance.Play("Background");
        AudioManager.instance.Play("Background2");
        BallSpawn(new Vector3(1.95f,1.95f,1.95f), new Vector3(1.95f, 1.95f, 1.95f));
        GameplayUIManager.Instance.ResetBalls();
        GameplayUIManager.Instance.UpdateHighScore();
        TLT.ResetAll();
        DT.ResetAll();
        PickupManager.Instance.PickupNum = 8;
        PickupManager.Instance.RemoveAllPickups();
        PickupManager.Instance.Spawn(PickupManager.Instance.PickupNum);

        //TODO: reset board / visuals
    }

    public void BallLost(Vector3 lastscale, Vector3 scalegoal)
    {
        _currentBalls--;
        GameplayUIManager.Instance.RemoveBall();

        if( _currentBalls == 0 )
        {
            EndGame();
        }
        else {
            BallSpawn(lastscale, scalegoal);
            PickupManager.Instance.RemoveAllPickups();
            PickupManager.Instance.Spawn(PickupManager.Instance.PickupNum);
        }
    }
    public void BallSpawn(Vector3 lastscale, Vector3 scalegoal)
    {  
        GameObject obj = Instantiate(ball, ballSpawnLoc.transform.position,ballSpawnLoc.transform.rotation);
        obj.GetComponent<BallSizeController>().SetSize(lastscale, scalegoal);
        FollowTargetManager.Instance.UpdateTargetGroup(obj.transform);
        AudioManager.instance.PlayOneShot("BallSpawn");
    }
    private void EndGame()
    {
        LeaderboardUIManager.Instance.gameObject.SetActive(true);
        LeaderboardUIManager.Instance.ShowNewHighScore(false);
        LeaderboardUIManager.Instance.ShowLoading(true);
        if(ScoreManager.score > ScoreManager.HighestScore)
        {
            ScoreManager.HighestScore = ScoreManager.score;
            ScoreManager.Instance.SaveScore();
            GameplayUIManager.Instance.UpdateHighScore();
            LeaderboardUIManager.Instance.ShowNewHighScore(true);
            LeaderboardUIManager.Instance.AddAndPullLeaderboard(ScoreManager.score);
        }
        else if (LeaderboardUIManager.Instance.Scores == null)
        {
            LeaderboardUIManager.Instance.PopulateLeaderboard();
        }
        LeaderboardUIManager.Instance.ShowLoading(false);
    }

}
