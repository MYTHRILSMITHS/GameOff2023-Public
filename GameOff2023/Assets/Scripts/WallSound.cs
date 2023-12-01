using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSound : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float speed = collision.rigidbody.velocity.magnitude;
            AudioManager.instance.PlayOneShot("Wall", speed/80f);
        }
    }
}
