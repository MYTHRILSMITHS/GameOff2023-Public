using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public Transform StartPos;
    public Transform EndPos;
    public Transform Pickups;
    public GameObject Pickup;
    public int PickupNum = 10;
    private LayerMask _mask;
    private int _currentNum = 10;
    public static PickupManager Instance { get; private set; }
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

        _mask = LayerMask.GetMask("Glass");
    }

    public void PickupRemoved(BallSizeController bsc)
    {
        _currentNum--;
        if(_currentNum <= 0)
        {
            ScoreManager.score += 4500;
            AudioManager.instance.PlayOneShot("GemGroup");
            bsc.GrowBall();
            Spawn(PickupNum);
        }
        else
        {
            bsc.ShrinkBall();
        }
    }

    public void RemoveAllPickups()
    {
        foreach(Transform child in Pickups)
        {
            Destroy(child.gameObject);
        }
    }

    public void Spawn(int num)
    {
        _currentNum = num;
        float xdist = EndPos.position.x - StartPos.position.x;
        float xinc = xdist / (float)num;
        float zdist = EndPos.position.z - StartPos.position.z;
        float zinc = zdist / (float)num;
        Vector3 position = Vector3.zero;
        for (int i = 0; i < num; i++)
        {
            var zpos = (i*zinc) + StartPos.position.z;
            bool done = false;
            RaycastHit hit;
            Vector3 origin;
            while (!done)
            {
                origin = new Vector3(Random.Range(StartPos.position.x,EndPos.position.x), StartPos.position.y + 10f, zpos);
                if (Physics.SphereCast(origin, 1.4f, Vector3.down, out hit, Mathf.Infinity, ~_mask))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        position = hit.point;
                        position.y = 0.1f;
                        done = true;
                    }
                }
            }
            Instantiate(Pickup, position, Quaternion.Euler(-90,0,0), Pickups);
        }
    }
}
