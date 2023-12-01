using UnityEngine;

public class PickupController : MonoBehaviour
{
    public MoveMode Mode;
    public float Speed;
    public bool UseRandomMode = true;
    public int Points = 10;
    private Rigidbody _rigidBody;
    private Vector3 _currentDirection = Vector3.zero;
    public GameObject GemExplode;



    public enum MoveMode
    {
        Stationary = 0,
        LeftRight = 1,
        UpDown = 2,
    }

    void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody>();
        if (UseRandomMode) Mode = (MoveMode)Random.Range(0, 3);
        UpdateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            AudioManager.instance.PlayOneShot("Gem");
            var bsc = other.GetComponent<BallSizeController>();
            PickupManager.Instance.PickupRemoved(bsc);
            ScoreManager.score += Points;
            Instantiate(GemExplode, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Gem")) { }
        else
        {
            UpdateMovement();
        }
    }

    public void UpdateMovement()
    {
        int num;
        int dir=1;
        switch (Mode)
        {
            case MoveMode.Stationary:
                _rigidBody.velocity = Vector3.zero;
                break;
            case MoveMode.LeftRight:
                if (_currentDirection.x == 0)
                {
                     num = Random.Range(2, 4);
                     dir = num % 2 == 0 ? 1 : -1;
                }
                else
                {
                    dir = (int)_currentDirection.x * -1;
                }
                _currentDirection.x = dir;
                _currentDirection.y = 0;
                _currentDirection.z = 0;
                _rigidBody.velocity = _currentDirection * Speed;
                break;
            case MoveMode.UpDown:
                if (_currentDirection.z == 0)
                {
                    num = Random.Range(2, 4);
                    dir = num % 2 == 0 ? 1 : -1;
                }
                else
                {
                    dir = (int)_currentDirection.z * -1;
                }
                _currentDirection.x = 0;
                _currentDirection.y = 0;
                _currentDirection.z = dir;
                _rigidBody.velocity = _currentDirection * Speed;
                break;
        }
    }
}

