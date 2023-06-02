using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen
{
    public float[,] GenerateMap(int chunkSize, float pointDistance, float heightScale)
    {
        float[,] map = new float[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                map[i, j] = Mathf.PerlinNoise(i * pointDistance, j * pointDistance);
                CreateVoxel(new(i, Mathf.Floor(map[i, j] * heightScale), j), Color.green);
            }
        };
        return map;
    }

    public void CreateVoxel(Vector3 position, Color color)
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        voxelObject.transform.position = position;
        voxelObject.GetComponent<Renderer>().material.color = color;
    }
}