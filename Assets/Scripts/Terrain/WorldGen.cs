using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen
{
    private Dictionary<Vector3, GameObject> voxelObjects; 

    public WorldGen()
    {
        voxelObjects = new Dictionary<Vector3, GameObject>();
    }

    public void Chunkify(Vector2 chunkCoordinate, int chunkSize, float heightScale, float pointDistance, bool create)
    {
        float[,] map = new float[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                Vector2 chunkInWorld = new Vector2(chunkCoordinate.x * chunkSize, chunkCoordinate.y * chunkSize); 
                map[i, j] = Mathf.PerlinNoise((i + chunkInWorld.x) * pointDistance * 0.1f, (j + chunkInWorld.y) * pointDistance * 0.1f);
                Vector3 voxelPosition = new Vector3(
                    i + chunkInWorld.x, 
                    Mathf.Floor(map[i, j] * heightScale), 
                    j + chunkInWorld.y); 

                if (create) 
                {
                    CreateVoxel(voxelPosition, Color.green);
                } else 
                {
                    RemoveVoxel(voxelPosition);
                }
            }
        }
    }

    public void CreateVoxel(Vector3 position, Color color)
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        voxelObject.transform.position = position;
        voxelObject.GetComponent<Renderer>().material.color = color;

        voxelObjects[position] = voxelObject;
    }

    public void RemoveVoxel(Vector3 position) 
    {
        Object.Destroy(voxelObjects[position]);
        voxelObjects.Remove(position);
    }
}