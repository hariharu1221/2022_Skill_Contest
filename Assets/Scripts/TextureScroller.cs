using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public List<GameObject> texture;
    public float minY;
    public float speed;
    private Vector3 ResetPos;
    public void Awake()
    {
        ResetPos = texture[0].transform.position;
    }

    public void Update()
    {
        foreach(GameObject ob in texture)
        {
            ob.transform.position += Vector3.back * speed * Time.deltaTime;
            if (ob.transform.position.z <= minY) ob.transform.position = ResetPos; 
        }
    }
}
