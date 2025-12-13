using UnityEngine;

public class PlayerController : MonoBehaviour
{
 
    public int score = 0;
    public float timeSurvived = 0f;

   
    private CharacterController characterController; 
    private Animator animator;                    

    
    public float forwardSpeed = 10f; 
    public float laneChangeSpeed = 5f;
    public float[] laneXPositions = new float[] { -2f, 2f }; 
    private int currentLaneIndex = 0;
    private Vector3 targetPosition;

    void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

  
        currentLaneIndex = 0; 
        targetPosition = new Vector3(laneXPositions[currentLaneIndex], transform.position.y, transform.position.z);
        transform.position = targetPosition; // Initialize position

        score = 0;
        timeSurvived = 0f;
        
       
        if (animator != null)
        {
            animator.SetFloat("Speed", forwardSpeed);
        }

        if (GameManager.Instance != null && GameManager.Instance.currentState == GameManager.GameState.MainMenu)
        {
            GameManager.Instance.SetState(GameManager.GameState.MainMenu);
        }
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
        {
            
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance != null)
            {
                GameManager.Instance.TogglePause();
            }
            return;
        }

        timeSurvived += Time.deltaTime; 
        
        HandleInput();
        MovePlayer();
    }

    void HandleInput()
    {
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }
    }

    void ChangeLane(int direction)
    {
        int newLaneIndex = currentLaneIndex + direction;
        if (newLaneIndex >= 0 && newLaneIndex < laneXPositions.Length)
        {
            currentLaneIndex = newLaneIndex;
            targetPosition.x = laneXPositions[currentLaneIndex];
        }
    }

    void MovePlayer()
    {
    
        Vector3 horizontalMove = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
        float deltaX = horizontalMove.x - transform.position.x;
        
        Vector3 forwardMove = Vector3.forward * forwardSpeed * Time.deltaTime;

       
        if (characterController != null)
        {
            Vector3 finalMove = new Vector3(deltaX, 0, forwardMove.z);
            characterController.Move(finalMove);
        }
        else 
        {
            
            transform.position = new Vector3(horizontalMove.x, transform.position.y, transform.position.z + forwardMove.z);
        }
    }


  
    private void OnTriggerEnter(Collider other)
    {
        
        Collectible collectible = other.GetComponent<Collectible>();
        if (collectible != null)
        {
            score += collectible.data.scoreValue;
            Destroy(other.gameObject); 
            Debug.Log("Score: " + score);
            return;
        }

    
        if (other.CompareTag("Obstacle")) 
        {
            
            if (animator != null) { animator.SetTrigger("Hit"); } 
            
            GameManager.Instance.finalScore = score;
            GameManager.Instance.finalTime = timeSurvived;
            
            GameManager.Instance.SetState(GameManager.GameState.GameOver);
            enabled = false; 
        }
    }
}