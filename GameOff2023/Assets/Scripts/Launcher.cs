using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Vector3 _Force;
    private Rigidbody _rb;
    private bool isActive = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.LaunchTriggered += TurnOnLauncher;
    }
    private void OnDisable()
    {
        InputManager.LaunchTriggered -= TurnOnLauncher;
    }
    private void TurnOnLauncher()
    {
            isActive = true;
        AudioManager.instance.PlayOneShot("Launch");
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            _rb.AddForce(_Force, ForceMode.Impulse);
            isActive = false;
        }
    }
}
