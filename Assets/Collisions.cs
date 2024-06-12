using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
   
    public GameObject greenOrb;
    public GameObject blueOrb;
    public GameObject redOrb;

    public GameObject obstacle;
    public TMP_Text t;

    public TMP_Text greenLevel;
    public TMP_Text redLevel;
    public TMP_Text blueLevel;
    public TMP_Text ability;
    public TMP_Text finalScore;
    public GameObject gameOver;
    public AudioSource soundEffectSource;
    public AudioSource microEffectSource;
    public AudioClip collectOrbSound;
    public AudioClip titleScreenMusic;
    public AudioClip gameMusic;
    public AudioClip hitObstacle;
    public AudioClip switchforms;
    public AudioClip usePower;
    public AudioClip invalidAction;
    private bool isAlive = true;

    int score = 0;
    int redEnergy = 0;
    int blueEnergy = 0;
    int greenEnergy = 0;
    public int currentLane = 1;
    private float[] lanePositions = { -10.0f, 0.0f, 10.0f };
    string playerColor = "white";
    public GameObject playerMaterial ;
    private MeshRenderer meshRenderer;
    Material material;
    private bool isMultiplierActive = false;
    private bool isShieldActive = false;
    private bool isInvincible;
    public Camera mainCamera; // The main camera following the player
    public Camera alternativeCamera;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
      material = meshRenderer.material;
        gameOver.gameObject.SetActive(false);
        soundEffectSource.clip = gameMusic;
        soundEffectSource.volume = 0.0001f;
        
        microEffectSource.volume = 1000.0f;
        soundEffectSource.Play();
        mainCamera.enabled = true;
        alternativeCamera.enabled = false;

    }
    public void SwitchToAlternativeCamera()
    {
        // Disable the main camera
        mainCamera.enabled = false;

        // Enable the alternative camera
        alternativeCamera.enabled = true;

        // Optionally, set the alternative camera as the main camera
    }

    public void SwitchToMainCamera()
    {
        // Disable the alternative camera
        alternativeCamera.enabled = false;

        // Enable the main camera
        mainCamera.enabled = true;

        // Optionally, set the main camera as the main camera (in case you changed it to the alternative camera)
    }
    public void red()
    {
        if (redEnergy >= 5)
        {

            microEffectSource.PlayOneShot(switchforms);

            isMultiplierActive = false;
            isShieldActive = false;

            redEnergy -= 1;
            if (redEnergy == 0)
            {
                material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            redLevel.text = "Red  = " + redEnergy;
            material.color = new Color(1.0f, 0.0f, 0.0f);
            playerColor = "red";
        }
    }
    public void blue()
    {
        if (blueEnergy >= 5)
        {
            microEffectSource.PlayOneShot(switchforms);

            isMultiplierActive = false;
            isShieldActive = false;
            blueEnergy -= 1;
            if (blueEnergy == 0)
            {
                material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            blueLevel.text = "Blue  = " + blueEnergy;
            material.color = new Color(0.0f, 0.0f, 1.0f);
            playerColor = "blue";
        }
    }
    public void green()
    {
        if (greenEnergy >= 5)
        {
            microEffectSource.PlayOneShot(switchforms);

            isMultiplierActive = false;
            isShieldActive = false;
            greenEnergy -= 1;
            if (greenEnergy == 0)
            {
                material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            greenLevel.text = "Green  = " + greenEnergy;
            material.color = new Color(0.0f, 1.0f, 0.0f);
            playerColor = "green";
        }
    }

    public void power()
    {
        if (!isMultiplierActive && !isShieldActive)
        {
            microEffectSource.PlayOneShot(usePower);

            if (playerColor == "red" && redEnergy > 0)
            {
                nuke();
                redEnergy -= 1;
                redLevel.text = "red  = " + redEnergy;
                if (redEnergy == 0)
                {
                    material.color = new Color(1.0f, 1.0f, 1.0f);
                }

            }
            else if (playerColor == "blue" && blueEnergy > 0)
            {

                shield();
                blueEnergy -= 1;
                blueLevel.text = "Blue  = " + blueEnergy;
                if (blueEnergy == 0)
                {
                    material.color = new Color(1.0f, 1.0f, 1.0f);
                }
            }
            else if (playerColor == "green" && greenEnergy > 0)
            {

                multiplier();
                greenEnergy -= 1;
                greenLevel.text = "green  = " + greenEnergy;
                if (greenEnergy == 0)
                {
                    material.color = new Color(1.0f, 1.0f, 1.0f);
                }
            }
        }
    }
        void Update()


    {

        if (Input.GetKeyDown(KeyCode.J) && redEnergy >= 5)

        {

            microEffectSource.PlayOneShot(switchforms);

            isMultiplierActive = false;
            isShieldActive = false;
           
            redEnergy -= 1;
            if (redEnergy == 0)
            {
                material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            redLevel.text = "Red  = " + redEnergy;
            material.color = new Color(1.0f, 0.0f, 0.0f);
            playerColor = "red";
        }
        else if (Input.GetKeyDown(KeyCode.K) && greenEnergy >= 5)

        {
            microEffectSource.PlayOneShot(switchforms);

            isMultiplierActive = false;
            isShieldActive = false;
            greenEnergy -= 1;
            if (greenEnergy == 0)
            {
                material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            greenLevel.text = "Green  = " + greenEnergy;
            material.color = new Color(0.0f, 1.0f, 0.0f);
            playerColor = "green";
        }
        else if (Input.GetKeyDown(KeyCode.L) && blueEnergy >= 5)
        {
            microEffectSource.PlayOneShot(switchforms);

            isMultiplierActive = false;
            isShieldActive = false;
            blueEnergy -= 1;
            if (blueEnergy == 0)
            {
                material.color = new Color(1.0f, 1.0f, 1.0f);
            }
            blueLevel.text = "Blue  = " + blueEnergy;
            material.color = new Color(0.0f, 0.0f, 1.0f);
            playerColor = "blue";
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isMultiplierActive && !isShieldActive)
            {
                microEffectSource.PlayOneShot(usePower);

                if (playerColor == "red" && redEnergy > 0)
                {
                    nuke();
                    redEnergy -= 1;
                    redLevel.text = "red  = " + redEnergy;
                    if (redEnergy == 0)
                    {
                        material.color = new Color(1.0f, 1.0f, 1.0f);
                    }

                }
                else if (playerColor == "blue" && blueEnergy > 0)
                {

                    shield();
                    blueEnergy -= 1;
                    blueLevel.text = "Blue  = " + blueEnergy;
                    if (blueEnergy == 0)
                    {
                        material.color = new Color(1.0f, 1.0f, 1.0f);
                    }
                }
                else if (playerColor == "green" && greenEnergy > 0)
                {

                    multiplier();
                    greenEnergy -= 1;
                    greenLevel.text = "green  = " + greenEnergy;
                    if (greenEnergy == 0)
                    {
                        material.color = new Color(1.0f, 1.0f, 1.0f);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleInvincibility();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseRedEnergy();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            IncreaseGreenEnergy();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseBlueEnergy();
        }

    }
    private void ToggleInvincibility()
    {
        isInvincible = !isInvincible;
        Debug.Log("Invincibility: " + isInvincible);
    }

    private void IncreaseRedEnergy()
    {
        redEnergy++;
        redLevel.text = "Red  = " + redEnergy;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        isAlive = false;
        gameObject.SetActive(false);
        this.transform.position = new Vector3(-0.367f,-0.2f,-9.851f);
        ResetGameVariables();
        SceneManager.LoadScene("SampleScene");
        isAlive = true;
        gameObject.SetActive(true);
        Time.timeScale = 1;



    }
    void ResetGameVariables()
    {
        score = 0;
        t.text = "Score : 0";
        greenEnergy = 0;
        blueEnergy = 0;
        redEnergy = 0;
        redLevel.text = "Red : 0";
        blueLevel.text = "Blue: 0";
        greenLevel.text = "Green:0";
    }
    private void IncreaseGreenEnergy()
    {
        greenEnergy++;
        greenLevel.text = "Green  = " + greenEnergy;
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("menu");
    }
    private void IncreaseBlueEnergy()
    {
        blueEnergy++;
        blueLevel.text = "Blue  = " + blueEnergy;
    }
    private void shield()
    {
        isShieldActive = true;
        isMultiplierActive = false;
    }
    private void multiplier()
    {
        isMultiplierActive = true;
        isShieldActive = false; 

    }
    private void nuke()
    {
        float range = 200.0f;

        Vector3 playerPosition = transform.position;

        Vector3 frontPosition = playerPosition + transform.forward * range;

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            float distance = Vector3.Distance(obstacle.transform.position, playerPosition);

            if (distance <= range)
            {
                Destroy(obstacle);
            }
        }

    }
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

  
    private void OnCollisionEnter(Collision collision)


    {

        if (collision.gameObject.CompareTag("blueOrb"))
        {
            if (playerColor == "blue")
            {
                if (isMultiplierActive)
                {
                    score += 10;
                    isMultiplierActive = false;
                }
                else
                {
                    score += 2;
                }
            }
            else
            {
                if (isMultiplierActive)
                {
                    score += 5;
                    if (blueEnergy < 4)
                    {
                        blueEnergy += 2;
                    }
                    isMultiplierActive = false;
                }
                else
                {


                    score += 1;
                    if (blueEnergy < 5)
                    {
                        blueEnergy += 1;
                    }
                }

                blueLevel.text = "Blue  = " + blueEnergy;
            }
            microEffectSource.PlayOneShot(collectOrbSound);

            Destroy(collision.gameObject.gameObject);
            
            t.text = "Score = " + score;
        }
        if (collision.gameObject.CompareTag("redOrb"))
        {
            if (playerColor == "red")
            {
                if (isMultiplierActive)
                {
                    score += 10;
                    isMultiplierActive = false;
                }
                else
                {
                    score += 2;
                }
            }
            else
            {
                if (isMultiplierActive)
                {
                    score += 5;
                    if (redEnergy < 4)
                    {
                        redEnergy += 2;
                    }
                    isMultiplierActive= false;
                }
                else
                {
                    

                    score += 1;
                    if (redEnergy < 5)
                    {
                        redEnergy += 1;
                    }
                }
           
            redLevel.text = "Red  = " + redEnergy;
        }
            microEffectSource.PlayOneShot(collectOrbSound);


            Destroy(collision.gameObject.gameObject);
           
            t.text = "Score = " + score;
    }
        if (collision.gameObject.CompareTag("greenOrb"))
        {
            if (playerColor == "green")
            {
                if (isMultiplierActive)
                {
                    score += 10;
                    isMultiplierActive = false;
                }
                else
                {
                    score += 2;
                }
            }
            else
            {
                if (isMultiplierActive)
                {
                    score += 5;
                    if (greenEnergy < 4)
                    {
                        greenEnergy += 2;
                    }
                    isMultiplierActive = false;
                }
                else
                {


                    score += 1;
                    if (greenEnergy < 5)
                    {
                        greenEnergy += 1;
                    }
                }

               
            }
            microEffectSource.PlayOneShot(collectOrbSound);

            Destroy(collision.gameObject.gameObject);
            greenLevel.text = "Green  = " + greenEnergy;
            t.text = "Score = " + score;
        }

       if (collision.gameObject.CompareTag("obstacle"))
        {
            isMultiplierActive = false;
            if (isShieldActive )
            {
                isShieldActive = false;
                ability.text = " - ";
            } 
            else if (!isInvincible)
            {
                microEffectSource.PlayOneShot(hitObstacle);

                if (playerColor == "white")
                {
                    SwitchToAlternativeCamera();
                    isAlive = false;
                     this.gameObject.SetActive(false);
                    gameOver.gameObject.SetActive(true);
                    finalScore.text= " Final Score:  "+score;
                }
                else
                {
                    playerColor = "white";
                    Destroy(collision.gameObject);
                    material.color = new Color(1.0f, 1.0f, 1.0f);
                }
            }
          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        {
            for (int i = 0; i < 10; i++)
            {
                int x = Random.Range(-35, 35);
                int z = Random.Range(0, 60);
                Instantiate(obstacle, new Vector3(x, 2, z), obstacle.transform.rotation);
            }
        }
    }
   
}
