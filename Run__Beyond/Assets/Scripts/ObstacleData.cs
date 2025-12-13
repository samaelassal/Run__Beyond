using UnityEngine;


[CreateAssetMenu(fileName = "NewObstacleData", menuName = "Game Data/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    [Header("Model & Movement")]
    public GameObject prefab;
    public float speedModifier = 1f;

    [Header("Gameplay Impact")]
    public int damage = 1; 
    public string obstacleName = "Car";
}