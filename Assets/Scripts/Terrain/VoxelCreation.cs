using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCreation
{
    public void CreateVoxel(Vector3 position, Vector3 size, Color color) 
    {
        //creates the actual voxel game object and sets it's position, size & color
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        voxelObject.transform.position = position;
        voxelObject.transform.localScale = size;
        voxelObject.GetComponent<Renderer>().material.color = color;
    }
}
