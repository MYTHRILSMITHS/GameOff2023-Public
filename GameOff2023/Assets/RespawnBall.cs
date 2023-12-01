using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            GameplayManager.Instance.BallSpawn(other.transform.localScale, other.gameObject.GetComponent<BallSizeController>().ScaleGoal);
        }
    }
}
