using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Camera camera;
    public Transform player;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = player.position + offset;
    }
}
