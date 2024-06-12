using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class gameManager : MonoBehaviour
{
    public GameObject redOrbPrefab;
    public GameObject greenOrbPrefab;
    public GameObject blueOrbPrefab;
    public GameObject obstaclePrefabs;
    public GameObject dividerPrefab;

    public int maxItemsPerLine = 3;
    public int maxObstaclesPerLine = 2;
    public float itemSpacing = 10.0f;
    public float lineSpacing = 30.0f;

    private List<int[]> laneOrders = new List<int[]>();
    private int nextLineToGenerate = 0;
    public List<GameObject> pooledObstacles;
    public List<GameObject> pooledOrbs;
    int currentSection = 0;
    public GameObject floorSectionPrefab; 
    public float sectionLength = 10.0f; 

    private List<GameObject> sections = new List<GameObject>();

    private int[] laneOrder = { 0, 1, 2 }; 
    public int initialOrbPoolSize = 35;
    public int initialObstaclePoolSize = 6;
    private void Start()
    {

        InitializeLaneOrders();


        InitializeFloor();
        InstantiateNewSection();

    }

    private void InitializeLaneOrders()
    {
        int numSections = 10; 
        for (int i = 0; i < numSections; i++)
        {
            ShuffleLaneOrder(); 
            laneOrders.Add((int[])laneOrder.Clone());
        }
    }

   

    private void InstantiateNewSection()
    {
        if (nextLineToGenerate > laneOrders.Count)
        {
            GenerateLaneOrders(20);
        }

        else if (nextLineToGenerate < laneOrders.Count)
        {

            int[] currentLaneOrder = laneOrders[nextLineToGenerate];

            ShuffleLaneOrder();
            laneOrders.Add((int[])laneOrder.Clone()); 
            nextLineToGenerate += 2;

            InstantiateItems();
        }
    }
    private void GenerateLaneOrders(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ShuffleLaneOrder();
            laneOrders.Add((int[])laneOrder.Clone());
        }
    }
   

    private void ShuffleLaneOrder()
    {
        for (int i = laneOrder.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = laneOrder[i];
            laneOrder[i] = laneOrder[j];
            laneOrder[j] = temp;
        }
    }
    public void InstantiateItems()
    {
        for (int line = nextLineToGenerate; line < nextLineToGenerate + 2; line=line+3)
        {
            int orbsCount = Random.Range(0, 2);
            int obstacleCount = Random.Range(0, 3 - orbsCount);
            int[] currentLaneOrder = laneOrders[line / 2];

            for (int i = 0; i < 3; i++)
            {
                int lane = currentLaneOrder[i];
                Vector3 spawnPosition = GetLanePosition(lane, line);
                GameObject itemPrefab;

                bool isOverlap = IsOverlap(spawnPosition);

                if (!isOverlap)
                {
                    if (obstacleCount > 0)
                    {
                        itemPrefab = obstaclePrefabs;
                        obstacleCount--;
                        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                    }
                    else if (orbsCount > 0)
                    {
                        int orbType = Random.Range(0, 3);
                        switch (orbType)
                        {
                            case 0:
                                itemPrefab = redOrbPrefab;
                                break;
                            case 1:
                                itemPrefab = greenOrbPrefab;
                                break;
                            case 2:
                                itemPrefab = blueOrbPrefab;
                                break;
                            default:
                                itemPrefab = redOrbPrefab;
                                break;
                        }
                        orbsCount--;
                        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                    }
                }
            }

  
        }
    }
    private bool IsOverlap(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1.0f); 

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle") || collider.CompareTag("GreenOrb") || collider.CompareTag("BlueOrb") || collider.CompareTag("RedOrb"))
            {
                return true;
            }
        }

        return false; 
    }

    private void Update()
    {
        if (transform.position.z > sections[currentSection].transform.position.z )
        {
            
            Vector3 spawnPosition = new Vector3(0, -2.5f, sections[sections.Count - 1].transform.position.z + sectionLength);
            GameObject newSection = Instantiate(floorSectionPrefab, spawnPosition, Quaternion.identity);
            sections.Add(newSection);
            nextLineToGenerate += 2;
            DestroyObjsBehindLine();
            InstantiateNewSection();
            currentSection += 1;


        }
    }
    private void DestroyObjsBehindLine()
    {
        GameObject[] greenOrbs = GameObject.FindGameObjectsWithTag("greenOrb");
        GameObject[] blueOrbs = GameObject.FindGameObjectsWithTag("blueOrb");
        GameObject[] redOrbs = GameObject.FindGameObjectsWithTag("redOrb");
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        foreach (GameObject obj in greenOrbs)
        {
            if (obj.transform.position.z < transform.position.z - 5.0f)
            {
                Destroy(obj);
            }
        }

        foreach (GameObject obj in blueOrbs)
        {
            if (obj.transform.position.z < transform.position.z)
            {
                Destroy(obj);
            }
        }

        foreach (GameObject obj in redOrbs)
        {
            if (obj.transform.position.z < transform.position.z)
            {
                Destroy(obj);
            }
        }

        foreach (GameObject obj in obstacles)
        {
            if (obj.transform.position.z < transform.position.z)
            {
                Destroy(obj);
            }
        }
    }

    private Vector3 GetLanePosition(int lane, int line)
    {
        float xOffset = 0.0f;
        if (lane == 0)
        {
            xOffset = -10.0f;
        }
        if (lane == 1)
        {
            xOffset = 0.0f;
        }
        if (lane == 2)
        {
            xOffset = 10.0f;
        }
        float laneSpacing = 6.0f;
        float zOffset = line * lineSpacing;

        return new Vector3(xOffset, 0.0f, zOffset);
    }

  
    void InitializeFloor()
    {
        int initialSections = 1;
        for (int i = 0; i < initialSections; i++)
        {
            Vector3 spawnPosition = new Vector3(0, -2.5f, i * sectionLength);
            GameObject section = Instantiate(floorSectionPrefab, spawnPosition, Quaternion.identity);
            sections.Add(section);
        }
    }
    





}

