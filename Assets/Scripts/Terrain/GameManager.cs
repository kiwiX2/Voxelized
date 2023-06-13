using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 playerPosition;
    private Vector2 playerChunk;
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
        for (int i = -renderDistance; i <= renderDistance; i++)
        {
            for (int j = -renderDistance; j <= renderDistance; j++)
            {
                Vector2 chunkCoordinate = playerChunk + new Vector2(i, j);
                if (!chunkMap.ContainsKey(chunkCoordinate)) 
                {
                    chunkMap[chunkCoordinate] = true;
                    worldGen.GenerateChunk(chunkCoordinate, chunkSize, heightScale, pointDistance);
                }
            }
        }
    }
}
