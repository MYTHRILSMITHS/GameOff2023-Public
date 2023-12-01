using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DropTarget : MonoBehaviour
{
    public float dropDist = 1f;
    [Tooltip("set all targets in a fuctional group to the same number")]
    public int setID = 0;
    public float resetDelay = 0.5f;
    public static List<DropTarget> dropTargets = new List<DropTarget>();
    [Tooltip("points when hit")]
    public int basePoints = 100;
    [Tooltip("points for full set, should be the same for whole setID group but doesn't have to be")]
    public int setPoints = 5000;

    public bool isDropped = false;

    // Start is called before the first frame update
    void Start()
    {
        dropTargets.Add(this );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter()
    {
        //drops target
        if (!isDropped)
        {
            AudioManager.instance.PlayOneShot("DT");
            transform.position += Vector3.down * dropDist;
            isDropped = true;
            ScoreManager.score += basePoints;
            // checks if rest of set has been dropped
            bool resetSet = true;
            foreach(DropTarget target in dropTargets)
            {
               if(target.setID == setID)
                {
                   if (!target.isDropped)
                    {
                       resetSet = false;
                    }
               }
            }
            //triggers set reset if all targets dropped
            if (resetSet)
            {
                ScoreManager.score += setPoints;
                AudioManager.instance.PlayOneShot("DropTargetGroup");
                Invoke("ResetSet", resetDelay);
            }
        }
    }
    void ResetSet()
    {
        
        foreach(DropTarget target in dropTargets)
            { 
                if(target.setID == setID)
                {
                    target.transform.position += Vector3.up * dropDist;
                    target.isDropped = false;
                }
            }
    }

    public void ResetAll()
    {
        foreach (DropTarget target in dropTargets)
        {
            if (target.isDropped)
            {
                target.transform.position += Vector3.up * dropDist;
                target.isDropped = false;
            }
        }
    }

}
