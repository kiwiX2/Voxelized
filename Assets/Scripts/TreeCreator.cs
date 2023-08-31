using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeCreator : MonoBehaviour
{
    public Material woodMaterial;

    string axiom = "V";
    int iterations = 3;

    public void CreateTree()
    {
        string tree = axiom;   
        for (int i = 1; i < iterations; i++)
        {
            tree = GenerateNext(tree);
            Debug.Log(tree);
        }        
    }

    string GenerateNext(string tree)
    {
        string output = "";

        foreach (char c in tree) 
        {
            if (c == 'V') 
            {
                output += "V[LL]V";
            } 

            else if (c == 'L')
            {
                output += "LL[V]L";
            }
        }

        return output;
    }

    void ExampleName() 
    {
        /*
        Vector3 position = new Vector3(1, 1, 1);

        GameObject empty = new GameObject("Tree");
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = voxelObject.GetComponent<Renderer>();
        Material material = renderer.material;

        voxelObject.transform.position = position;
        renderer.material = woodMaterial;
        material.SetFloat("_Smoothness", 0.2f);
        voxelObject.transform.SetParent(empty.transform);

        PrefabUtility.SaveAsPrefabAsset(empty, "Assets/Prefabs/Tree.prefab");
        */
    }
}
