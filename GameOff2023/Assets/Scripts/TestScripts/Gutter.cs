using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            GameplayManager.Instance.BallLost(other.transform.localScale, other.gameObject.GetComponent<BallSizeController>().ScaleGoal);
            Destroy(other.gameObject);      
        }
    }
}
