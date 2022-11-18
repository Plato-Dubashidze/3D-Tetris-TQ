using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCubeBehaviour : MonoBehaviour
{
    public float touchForce;

    private Rigidbody rb;
    private bool isTouched;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTouched)
        {
            isTouched = true;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddExplosionForce(touchForce, collision.transform.position, 1f, 1f, ForceMode.Impulse);
            Destroy(gameObject, 1.5f);
        }
    }
}
