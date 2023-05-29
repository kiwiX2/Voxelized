using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator
{
    public float[,] GenerateNoise(int width, int height, float pointDistance) 
    {
        float[,] noiseMap = new float[width, height];

        for(int i = 0; i < width; i++) 
        {
            for(int j = 0; j < height; j++) 
            {
                float x = i * pointDistance;
                float y = j * pointDistance;
                noiseMap[i, j] = Mathf.PerlinNoise(x, y);
            }
        }
        return noiseMap;
    }
}
