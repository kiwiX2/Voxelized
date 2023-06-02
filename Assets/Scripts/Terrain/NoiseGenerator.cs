using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator
{
    public float[,] GenerateNoise(int chunkSize, float pointDistance) 
    {
        float[,] noiseMap = new float[chunkSize, chunkSize];

        for(int i = 0; i < chunkSize; i++) 
        {
            for(int j = 0; j < chunkSize; j++) 
            {
                float x = i * pointDistance;
                float y = j * pointDistance;
                noiseMap[i, j] = Mathf.PerlinNoise(x, y);
            }
        }
        return noiseMap;
    }
}
