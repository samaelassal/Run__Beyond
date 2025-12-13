using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; } 

 
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState currentState { get; private set; }

 
    public int finalScore = 0;
    public float finalTime = 0f;

    private void Awake()
    {
     
        if (Instance == null)
        {
            Instance = this;
           
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Game State changed to: " + newState.ToString());

        if (newState == GameState.Paused || newState == GameState.MainMenu || newState == GameState.GameOver)
        {
            Time.timeScale = 0f; 
        }
        else 
        {
            Time.timeScale = 1f; 
        }

       
    }

    
    public void RestartGame()
    {
        
        SetState(GameState.Playing); 

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    
    public void TogglePause()
    {
        if (currentState == GameState.Playing)
        {
            SetState(GameState.Paused);
        }
        else if (currentState == GameState.Paused)
        {
            SetState(GameState.Playing);
        }
    }
}