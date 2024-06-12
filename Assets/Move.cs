using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [Tooltip("speed of the capsule")]
    public float speed;

    public Rigidbody rb;

    public float playerFSpeed = 3;
    public float playerLRSpeed = 4;
    int currentLane; 
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    private bool isPaused = false;
    public GameObject pauseMenu;

    private bool canJump = true;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public float swipeThreshold = 100.0f; 


    private float[] lanePositions = { -10.0f, 0.0f, 10.0f };
    void Start()
    {
        currentLane = 1;

        rb = this.GetComponent<Rigidbody>();

        pauseMenu.SetActive(false);
    
    }
  
    public void pause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
       else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }





    }
    private void FixedUpdate()


    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);

        HandleSwipeInput();



    }
    void HandleSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    touchEndPos = touch.position;

                    float swipeDistance = (touchEndPos - touchStartPos).magnitude;

                    if (swipeDistance > swipeThreshold)
                    {
                        Vector2 swipeDirection = touchEndPos - touchStartPos;

                        if (swipeDirection.x > 0)
                        {
                            ChangeLane(1);
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            ChangeLane(-1);
                            Debug.Log("Left Swipe");
                        }
                    }

                    break;
            }
        }
    
}


    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    void ChangeLane(int direction)
    {
        int newLane = currentLane + direction;
        if (newLane > 2)
        {
            newLane = 2;
        }
        else if (newLane < 0)
        {
            newLane = 0;
        }

        currentLane = newLane;
        Vector3 newPosition = transform.position;
        newPosition.x = lanePositions[currentLane];
        transform.position = newPosition;


    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        
        pauseMenu.SetActive(false);
    }

    public void ResumeButtonClicked()
    {
        ResumeGame();
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void MainMenu()
    {
       
        SceneManager.LoadScene("menu");

    }
 
}
