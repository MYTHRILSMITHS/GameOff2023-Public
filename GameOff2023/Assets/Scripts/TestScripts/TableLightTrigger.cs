using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLightTrigger : MonoBehaviour
{
    [Tooltip("set all related lights to the same number")]
    public int groupID = 0;
    public static List<TableLightTrigger> tLights = new List<TableLightTrigger>();
    public float resetDelay = 0.5f;
    public Color offLight = Color.red;
    public Color onLight = Color.green;
    [Tooltip("if true light will turn off if ball triggers it a second time")]
    public bool flipSwitch = false;
    [Tooltip("make entire group have same value, only last light in group will trigger points")]
    public int groupPoints = 1000;

    private bool isActive = false;
    private SpriteRenderer SR;

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        tLights.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!isActive)
            {
                SR.color = onLight;
                isActive = true;
                AudioManager.instance.PlayOneShot("TableLight");
                bool resetLights = true;
                foreach (TableLightTrigger target in tLights)
                {
                    if (target.groupID == groupID)
                    {
                        if (!target.isActive)
                        {
                            resetLights = false;
                        }
                    }
                }
                if (resetLights)
                {
                    AudioManager.instance.PlayOneShot("TableLightGroup");
                    Invoke("ResetGroup", resetDelay);
                }
            }
            else if (isActive && flipSwitch)
            {
                isActive = false;
                SR.color = offLight;
            }
        }
    }
    void ResetGroup()
    {
        foreach (TableLightTrigger target in tLights)
        {
            if (target.groupID == groupID)
            {
                target.SR.color = offLight;
                target.isActive = false;
                ScoreManager.score += groupPoints;
            }
        }
    }

    public void ResetAll()
    {
        foreach (TableLightTrigger target in tLights)
        {
                target.SR.color = offLight;
                target.isActive = false;
        }
    }
}
