using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private VoxelGeneration voxelGeneration;
    private NoiseGenerator noiseGenerator;
    public GameObject playerObject;
    public float heightScale = 5;
    public float pointDistance = 0.2f;
    public int chunkSize = 16;

    void Start()
    {
        noiseGenerator = new NoiseGenerator();
        voxelGeneration = new VoxelGeneration();

        float[,] heightMap = noiseGenerator.GenerateNoise(chunkSize, chunkSize, pointDistance);
        voxelGeneration.GenerateVoxels(playerObject, heightMap, heightScale);
    }

    void Update()
    {
        
    }
}
