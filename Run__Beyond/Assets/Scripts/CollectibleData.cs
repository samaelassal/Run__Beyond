using UnityEngine;

// This line adds the menu item for creating the asset in Unity
[CreateAssetMenu(fileName = "NewCollectibleData", menuName = "Game Data/Collectible Data")]
public class CollectibleData : ScriptableObject
{
    [Header("Model & Value")]
    public GameObject prefab;
    public int scoreValue = 10;
    public string itemName = "Coin";

    [Header("Audio")]
    public AudioClip pickupSFX;
}