using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlipperScript : MonoBehaviour
{
    [Tooltip ("right side + left side - ")]
    public float maxAngle = 35f;
    public float flipTime = 0.1f;
    public string buttonName = "Fire1";

    private Quaternion initialState;
    private Quaternion endState;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        initialState = transform.rotation;
        endState.eulerAngles = new Vector3(initialState.eulerAngles.x, initialState.eulerAngles.y + maxAngle, initialState.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButton(buttonName))
        {
            transform.rotation = Quaternion.Slerp(initialState, endState, t / flipTime);
            t += Time.deltaTime;
            if(t > flipTime)
            {
                t = flipTime;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(initialState, endState, t / flipTime);
            t -= Time.deltaTime;
            if(t < 0)
            {
                t = 0;
            }
        }
    }
}
