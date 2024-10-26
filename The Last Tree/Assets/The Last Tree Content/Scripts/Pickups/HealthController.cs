using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static HealthController instance;

    public HealthPickup healthToDrop;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropHealth(Vector3 position)
    {
        HealthPickup healthDrop = Instantiate(healthToDrop, position + new Vector3(.6f, .3f, 0), Quaternion.identity);

        healthDrop.gameObject.SetActive(true);
    }
}
