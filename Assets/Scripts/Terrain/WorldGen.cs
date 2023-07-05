using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen
{
    private Color colorMod = new Color(0.05f, 0.05f, 0.05f);
    private Color voxelColor;
    private float snowOffset;
    private float snowThreshold = 0.85f;
    private float stoneOffset;
    private float stoneThreshold = 0.65f;
    private Dictionary<Vector3, GameObject> voxelObjects; 

    public WorldGen()
    {
        voxelObjects = new Dictionary<Vector3, GameObject>();
    }

    public void Chunkify(Vector2 chunkCoordinate, int chunkSize, float heightScale, float pointDistance, bool create)
    {
        snowOffset = heightScale * 0.8f;
        stoneOffset = heightScale * 0.5f;
        int mapOffset = 10000;
        float[,] map = new float[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                Vector2 chunkInWorld = new Vector2(chunkCoordinate.x * chunkSize, chunkCoordinate.y * chunkSize); 

                map[i, j] = Mathf.PerlinNoise(
                    (i + chunkInWorld.x + mapOffset) * pointDistance * 0.1f, 
                    (j + chunkInWorld.y + mapOffset) * pointDistance * 0.1f);

                Vector3 voxelPosition = new Vector3(
                    i + chunkInWorld.x, 
                    Mathf.Floor(map[i, j] * heightScale), 
                    j + chunkInWorld.y); 

                if (create) 
                {
                    voxelColor = GetVoxelColor(voxelPosition.y);
                    CreateVoxel(voxelPosition, voxelColor);
                } else 
                {
                    RemoveVoxel(voxelPosition);
                }
            }
        }
    }

    Color GetVoxelColor(float yValue)
    {    
        int colorVariant = Random.Range(0, 3);
        bool isWhite = Random.Range(snowThreshold, 1f) <= yValue / snowOffset;
        bool isGrey = Random.Range(stoneThreshold, snowThreshold) <= yValue / stoneOffset;
        
        switch ((isWhite, isGrey)) 
        {
            case (true, _):
                voxelColor = Color.white;
                break;

            case (_, true):
                voxelColor = Color.grey;
                break;

            default:
                voxelColor = Color.green;
                break;
        }

        switch (colorVariant)
        {
            case 0:
                return voxelColor + colorMod;
            case 1:
                return voxelColor - colorMod;
            default:
                return voxelColor;
        }
    }

    public void CreateVoxel(Vector3 position, Color color)
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = voxelObject.GetComponent<Renderer>();
        Material material = renderer.material;

        voxelObject.transform.position = position;
        renderer.material.color = color;
        material.SetFloat("_Smoothness", 0.2f);

        voxelObjects[position] = voxelObject;
    }

    public void RemoveVoxel(Vector3 position) 
    {
        Object.Destroy(voxelObjects[position]);
        voxelObjects.Remove(position);
    }
}