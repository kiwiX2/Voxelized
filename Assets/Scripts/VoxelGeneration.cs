using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGeneration
{
    private VoxelCreation voxelCreation;

    public void GenerateVoxels(GameObject playerObject, float [,] heightMap, float heightScale) 
    {
        voxelCreation = new VoxelCreation();

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color color = Color.green;
        Vector3 size = new Vector3(1, 1, 1);

        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++) 
            {
                float heightValue = Mathf.Floor(heightMap[i, j] * heightScale);
                Vector3 position = new Vector3(i, heightValue, j);
                voxelCreation.CreateVoxel(position, size, color);
            }
        }
    }
}
