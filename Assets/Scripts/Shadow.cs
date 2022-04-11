using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private float ny;

    private void Awake()
    {
        ny = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, ny, transform.position.z);
    }
}
