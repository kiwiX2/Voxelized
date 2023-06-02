using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WorldGen worldGeneration;
    public GameObject playerObject;
    public int chunkSize = 16;
    public float pointDistance = 0.2f;
    public float heightScale = 5;

    void Start()
    {
        worldGeneration = new();
        worldGeneration.GenerateMap(chunkSize, pointDistance, heightScale);
    }

    void Update()
    {
        
    }
}
