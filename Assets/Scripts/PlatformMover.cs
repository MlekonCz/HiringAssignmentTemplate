using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float platformSpeed = 5f;
    private void Update()
    {
        // if (transform.position.z < -50f)
        // {
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     transform.Translate(0,0,-platformSpeed * Time.deltaTime);
        // }
    }

}
