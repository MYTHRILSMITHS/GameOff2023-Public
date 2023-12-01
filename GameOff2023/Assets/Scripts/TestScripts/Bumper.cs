using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float force = 2000f;
    public float forceRadius = 1f;
    public int points = 100;
    public bool isRound = false;
    public float growthAmount = 1.5f;
    public float growthTime = 0.5f;

    private Vector3 initialSize;

    
    private void Start()
    {
        initialSize = transform.localScale;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = (collision.collider.transform.position-collision.contacts[0].point).normalized;
        direction.y = 0;
        collision.collider.attachedRigidbody.AddForce(direction * force, ForceMode.Impulse);
        ScoreManager.score += points;
        AudioManager.instance.PlayOneShot("Bumper");
        if (isRound)
        {
            HitGrowth();
            Invoke("HitShrink", growthTime *Time.deltaTime);
        }
    }
    private void HitGrowth()
    {
       gameObject.transform.localScale = initialSize * growthAmount;
    }
    private void HitShrink()
    {
        transform.localScale = initialSize; 
    }
}
