using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGeneration
{
    private VoxelCreation voxelCreation;

    public void GenerateVoxels(GameObject playerObject, float [,] heightMap, int chunkSize, float heightScale) 
    {
        voxelCreation = new VoxelCreation();

        Color color = Color.green;
        Vector3 size = new Vector3(1, 1, 1);

        for (int i = 0; i < chunkSize; i++) 
        {
            for (int j = 0; j < chunkSize; j++) 
            {
                float heightValue = Mathf.Floor(heightMap[i, j] * heightScale);
                Vector3 position = new Vector3(i, heightValue, j);
                voxelCreation.CreateVoxel(position, size, color);
            }
        }
    }
}
