using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{

    // This method is called automatically when the object is no longer visible by any camera
    private void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }
}