using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 playerPosition;
    private Vector2 playerChunk;
    private Vector2 previousChunk = new Vector2(1, 1);
    private Vector2 chunkCoordinate;
    private WorldGen worldGen;

    public int chunkSize = 16;
    public float heightScale = 5;
    public float pointDistance = 0.2f;
    public int renderDistance = 2;

    private Dictionary<Vector2, bool> chunkMap;

    void Start()
    {
        worldGen = new WorldGen();
        chunkMap = new Dictionary<Vector2, bool>();
    }

    void Update()
    {
        playerPosition = playerObject.transform.position;
        playerChunk = new Vector2(Mathf.Floor(playerPosition.x / chunkSize), Mathf.Floor(playerPosition.z / chunkSize));
        if (previousChunk == playerChunk) 
        {
            return;
        }
        previousChunk = playerChunk;

        CallGeneration();
        NukeChunks();
    }

    void CallGeneration()
    {
        for (int i = -renderDistance; i <= renderDistance; i++)
        {
            for (int j = -renderDistance; j <= renderDistance; j++)
            {
                chunkCoordinate = playerChunk + new Vector2(i, j);
                if (!chunkMap.ContainsKey(chunkCoordinate)) 
                {
                    chunkMap[chunkCoordinate] = true;
                    worldGen.Chunkify(chunkCoordinate, chunkSize, heightScale, pointDistance, true);
                }
            }
        }
    }

    void NukeChunks()
    {
        List<Vector2> chunksToNuke = new List<Vector2>();
        foreach (var chunk in chunkMap.Keys) 
        {
            int distanceX = Mathf.Abs((int)(playerChunk.x - chunk.x));
            int distanceY = Mathf.Abs((int)(playerChunk.y - chunk.y));

            if (distanceX > renderDistance || distanceY > renderDistance) 
            {
                chunksToNuke.Add(chunk);
            }
        }
        foreach (Vector2 chunk in chunksToNuke) 
        {
            worldGen.Chunkify(chunk, chunkSize, heightScale, pointDistance, false);
            chunkMap.Remove(chunk);
        }
    }
}
