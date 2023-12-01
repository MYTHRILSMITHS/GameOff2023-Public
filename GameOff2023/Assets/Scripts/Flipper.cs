using UnityEngine;
using UnityEngine.InputSystem;

public enum FlipperType
{
    Left,
    Right,
    Both
}

public class Flipper : MonoBehaviour
{
    [SerializeField] private FlipperType _Type;
    [SerializeField] private Vector3 _Force;

    private Rigidbody _rb;
    private bool ShouldAddForce = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        switch(_Type)
        {
            case FlipperType.Left:
                InputManager.LeftFlipperTriggered += TurnOnFlipper;
                break;
                case FlipperType.Right:
                InputManager.RightFlipperTriggered += TurnOnFlipper;
                break;
                case FlipperType.Both:
                InputManager.LeftFlipperTriggered += TurnOnFlipper;
                InputManager.RightFlipperTriggered += TurnOnFlipper;
                break;
        }
    }

    private void OnDisable()
    {
        switch (_Type)
        {
            case FlipperType.Left:
                InputManager.LeftFlipperTriggered -= TurnOnFlipper;
                break;
            case FlipperType.Right:
                InputManager.RightFlipperTriggered -= TurnOnFlipper;
                break;
            case FlipperType.Both:
                InputManager.LeftFlipperTriggered -= TurnOnFlipper;
                InputManager.RightFlipperTriggered -= TurnOnFlipper;
                break;
        }
    }

    private void TurnOnFlipper(bool on)
    {
        ShouldAddForce = on;
        if (ShouldAddForce)
        {
            FlipperSound();
        }
    }
    private void FlipperSound()
    {
        AudioManager.instance.PlayOneShot("Flipper");
        
    }
    private void FixedUpdate()
    {
        if (ShouldAddForce)
        {
            _rb.AddForce(_Force, ForceMode.Impulse);
            
        }
    }
}
