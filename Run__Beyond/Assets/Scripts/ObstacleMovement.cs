using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float currentSpeed = 10f; 
    private float destroyZ = -5f; 

    public void SetSpeed(float modifier)
    {
        currentSpeed = 10f * modifier; 
    }

    void Update()
    {
        
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime, Space.World);

        
        if (transform.position.z < destroyZ) 
        {
            Destroy(gameObject);
        }
    }
}