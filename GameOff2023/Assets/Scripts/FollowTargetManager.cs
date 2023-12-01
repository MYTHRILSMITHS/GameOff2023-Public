using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetManager : MonoBehaviour
{
    public CinemachineTargetGroup TargetGroup;
    public Transform CenterPoint;
    [SerializeField] private Mode _mode = Mode.Average2Targets;
    private CinemachineTargetGroup.Target[] _targets;
    public static FollowTargetManager Instance { get; private set; }
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
    public enum Mode
    {
        Average2Targets
    }

    private void OnEnable()
    {
        _targets = new CinemachineTargetGroup.Target[2];
        CinemachineTargetGroup.Target target;
        target.weight = 1f;
        target.radius = 0;
        target.target = CenterPoint;
        _targets[0] = target;
        _targets[1] = target;
        TargetGroup.m_Targets = _targets;
    }

    public void UpdateTargetGroup(Transform ballt)
    {
        switch (_mode)
        {
            case Mode.Average2Targets:
                _targets[1].target = ballt;
                break;
        }
    }
}
