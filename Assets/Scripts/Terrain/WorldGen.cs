using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    Color colorMod = new Color(0.05f, 0.05f, 0.05f);
    GameObject[] trees; 
    Color voxelColor;
    float snowOffset;
    float snowThreshold = 0.85f;
    float stoneOffset;
    float stoneThreshold = 0.65f;
    Dictionary<Vector3, GameObject> voxelObjects = new Dictionary<Vector3, GameObject>(); 

    public void Chunkify(Vector2 chunkCoordinate, int chunkSize, float heightScale, float pointDistance, GameObject[] treesArray, bool create)
    {
        trees = treesArray;
        snowOffset = heightScale * 0.8f;
        stoneOffset = heightScale * 0.5f;
        int mapOffset = 10000;
        float[,] map = new float[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                Vector2 chunkInWorld = new Vector2(chunkCoordinate.x * chunkSize, chunkCoordinate.y * chunkSize); 
                float currentXCoordinate = i + chunkInWorld.x;
                float currentZCoordinate = j + chunkInWorld.y;
                map[i, j] = Mathf.PerlinNoise(
                    (currentXCoordinate + mapOffset) * pointDistance * 0.1f, 
                    (currentZCoordinate + mapOffset) * pointDistance * 0.1f
                );

                Vector3 voxelPosition = new Vector3(
                    currentXCoordinate, 
                    Mathf.Floor(map[i, j] * heightScale), 
                    currentZCoordinate
                ); 

                if (create) 
                {
                    Vector3 currentVoxelPosition = new Vector3(currentXCoordinate, voxelPosition.y, currentZCoordinate);
                    voxelColor = GetVoxelColor(currentVoxelPosition);
                    CreateVoxel(voxelPosition, voxelColor);
                } else 
                {
                    RemoveVoxel(voxelPosition);
                }
            }
        }
    }

    Color GetVoxelColor(Vector3 voxelPos)
    {    
        int colorVariant = Random.Range(0, 3);
        bool isWhite = Random.Range(snowThreshold, 1) <= voxelPos.y / snowOffset;
        bool isGrey = Random.Range(stoneThreshold, snowThreshold) <= voxelPos.y / stoneOffset;
        
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

                //tree chance on grass voxels
                if (Random.value > 0.99f)
                {
                    int rngTree = Random.Range(0, 3);
                    Debug.Log(trees[rngTree]);
                    Instantiate(trees[rngTree], voxelPos, Quaternion.identity);
                }
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

    void CreateVoxel(Vector3 position, Color color)
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = voxelObject.GetComponent<Renderer>();
        Material material = renderer.material;

        voxelObject.transform.position = position;
        renderer.material.color = color;
        material.SetFloat("_Smoothness", 0.2f);

        voxelObjects[position] = voxelObject;
    }

    void RemoveVoxel(Vector3 position) 
    {
        Object.Destroy(voxelObjects[position]);
        voxelObjects.Remove(position);
    }

    void TreeGeneratorinator(Vector3 voxelPos)
    {
        Vector3 treeVoxelPos = new Vector3(voxelPos.x, voxelPos.y + 1, voxelPos.z);
        CreateVoxel(treeVoxelPos, Color.magenta);
    }
}