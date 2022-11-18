using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 defaultVelocity;
    public float forceOnTap, rotateSpeed;

    private bool isRotating;
    private Rigidbody rb;

    private void Awake()
    {
        GlobalEventManager.swipe.AddListener(Swipe);
        GlobalEventManager.doubleTap.AddListener(DoubleTap);
    }

    private void DoubleTap()
    {
        rb.AddForce(new Vector3(-forceOnTap, 0, 0));
    }

    private void Swipe(Vector2 dir)
    {
        if (!isRotating)
        {
            StartCoroutine(RotateMe(dir * 90, rotateSpeed));
        }
    }

    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.velocity = defaultVelocity;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            print(rb.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector3.zero;
        }
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        isRotating = true;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation =  toAngle;
        isRotating = false;
    }
}
