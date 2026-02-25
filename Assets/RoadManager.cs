using System.Collections.Generic;   
using UnityEngine;                 

public class RoadSpawner : MonoBehaviour   
{
    public GameObject roadPrefab;     

    public float scrollSpeed = 3f;    
    public int chunksOnScreen = 5;    
    private float chunkHeight = 4f;   
    private List<GameObject> activeChunks = new List<GameObject>();

    void Start()
    {
        float bottomOfScreen = -7.5f; ; // camera bottom
        for (int i = 0; i < chunksOnScreen; i++)
        {
            float spawnY = bottomOfScreen + (i * chunkHeight);
            SpawnChunk(spawnY);
        }
    }

    void Update()  
    {
        MoveChunks();

        if (activeChunks.Count > 0)
        {
            GameObject firstChunk = activeChunks[0];

            if (firstChunk.transform.position.y <= -7.5f)
            {
                Destroy(firstChunk);
                activeChunks.RemoveAt(0);
                float newYRoadPosition = activeChunks[activeChunks.Count - 1].transform.position.y + chunkHeight;
                SpawnChunk(newYRoadPosition);
            }
        }
    }
    void SpawnChunk(float yPosition)  
    {
        GameObject newChunk = Instantiate(roadPrefab, new Vector3(0, yPosition, 0), Quaternion.identity);
        newChunk.layer = LayerMask.NameToLayer("Background");
        activeChunks.Add(newChunk);

    }

    void MoveChunks()   
    {
        foreach (GameObject chunk in activeChunks)
        {
            chunk.transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        }
    }
}