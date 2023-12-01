using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BallSizeController : MonoBehaviour
{
    [SerializeField] private Vector3 _MinScale;
    [SerializeField] private Vector3 _MaxScale;
    [SerializeField] private float _ScaleRate;

    public Vector3 ScaleGoal;
    private bool _shrinkBall = false;
    private bool _growBall = false;

    //NOTE: Parameters not used at the moment - for a different mode where ball doesnt reset on respawn
    public void SetSize(Vector3 scalegoal, Vector3 scalesize)
    {
        ScaleGoal = _MaxScale;
        this.transform.localScale = ScaleGoal;
        /*ScaleGoal = scalegoal;
        this.transform.localScale = scalesize;
        */
    }

    private void FixedUpdate()
    {
        if (_growBall)
        {
            GrowBallSize();
        }
        else if (_shrinkBall)
        {
            ShrinkBallSize();
        }
    }

    public void ShrinkBall()
    {
        var scaledif = _MaxScale - _MinScale;
        var increment = scaledif / (float)PickupManager.Instance.PickupNum;
        ScaleGoal -= increment;
        _shrinkBall = true;     
    }

    public void GrowBall()
    {
        ScaleGoal = _MaxScale;
        _growBall = true;
    }

    private void ShrinkBallSize()
    {
        Vector3 scale = this.transform.localScale;
        scale = scale + (Time.fixedDeltaTime * _ScaleRate * -1 * Vector3.one);
        if (scale.x <= ScaleGoal.x)
        {
            _shrinkBall = false;
            scale = ScaleGoal;
        }
        this.transform.localScale = scale;
    }

    private void GrowBallSize()
    {
        Vector3 scale = this.transform.localScale;
        scale = scale + (Time.fixedDeltaTime * _ScaleRate * 1 * Vector3.one);
        if (scale.x >= ScaleGoal.x)
        {
            _growBall = false;
            scale = ScaleGoal;
        }
        this.transform.localScale = scale;
    }

}
