using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject[] GroundPreFabs;
    public float zSpawn = 0;
    public float groundLength = 30;
    public int numberOfGround = 6;
    private List<GameObject> activeGround = new List<GameObject>();

    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
       for(int i = 0; i < numberOfGround; i++)
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
        if(playerTransform.position.z - 35 > zSpawn -(numberOfGround * groundLength))
        {
            SpawnGround(Random.Range(0, GroundPreFabs.Length));
            DeleteGround();
        }
    }

    public void SpawnGround(int groundIndex)
    {
        GameObject go = Instantiate(GroundPreFabs[groundIndex], transform.forward * zSpawn,
            transform.rotation);
        activeGround.Add(go);
        zSpawn += groundLength;
    }

    private void DeleteGround()
    {
        Destroy(activeGround[0]);
        activeGround.RemoveAt(0);
    }
}
