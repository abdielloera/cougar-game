using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject[] GroundPreFabs;
    public GameObject powerUpPrefab; // Add a public variable for the power-up prefab
    public float zSpawn = 0;
    public float groundLength = 30;
    public int numberOfGround = 6;
    private List<GameObject> activeGround = new List<GameObject>();

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfGround; i++)
        {
            if (i == 0)
                SpawnGround(0);
            else
                SpawnGround(Random.Range(1, GroundPreFabs.Length - 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numberOfGround * groundLength))
        {
            SpawnGround(Random.Range(0, GroundPreFabs.Length));
            DeleteGround();

            // Optionally, spawn a power-up along with the road
            SpawnPowerUp();
        }
    }

    public void SpawnGround(int groundIndex)
    {
        GameObject go = Instantiate(GroundPreFabs[groundIndex], transform.forward * zSpawn, transform.rotation);
        activeGround.Add(go);
        zSpawn += groundLength;
    }

    private void DeleteGround()
    {
        Destroy(activeGround[0]);
        activeGround.RemoveAt(0);
    }

    private void SpawnPowerUp()
    {
        // Randomly decide whether to spawn a power-up
        if (Random.value < 0.1f) // Adjust the probability as needed
        {
            // Spawn a power-up prefab along the road
            Instantiate(powerUpPrefab, new Vector3(GetRandomX(), 1.26f, zSpawn), Quaternion.identity);
        }
    }

    private float GetRandomX()
    {
        // Array of specific X-coordinate values
        float[] xValues = { -1f, 0f, 1f };

        // Randomly select one of the values
        float randomX = xValues[Random.Range(0, xValues.Length)];

        return randomX;
    }

}
