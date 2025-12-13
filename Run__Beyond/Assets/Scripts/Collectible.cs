using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleData data; 

    
    void Update()
    {
        transform.Rotate(Vector3.up, 100f * Time.deltaTime);
    }
}