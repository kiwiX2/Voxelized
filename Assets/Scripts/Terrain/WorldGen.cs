using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen
{
    public void GenerateChunk(Vector2 chunkCoordinate, int chunkSize, float heightScale, float pointDistance)
    {
        float[,] map = new float[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                Vector2 chunkInWorld = new Vector2(chunkCoordinate.x * chunkSize, chunkCoordinate.y * chunkSize); 
                map[i, j] = Mathf.PerlinNoise((i + chunkInWorld.x) * pointDistance, (j + chunkInWorld.y) * pointDistance);
                CreateVoxel(new Vector3(
                    i + chunkInWorld.x, 
                    Mathf.Floor(map[i, j] * heightScale), 
                    j + chunkInWorld.y), Color.green);
            }
        }
    }

    public void CreateVoxel(Vector3 position, Color color)
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        voxelObject.transform.position = position;
        voxelObject.GetComponent<Renderer>().material.color = color;
    }
}