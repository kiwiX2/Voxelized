using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeCreator
{
    GameObject empty;

    string axiom = "V";
    int iterations = 3;

    public void CreateTree(Material woodMaterial)
    {
        empty = new GameObject("Tree");
        string tree = axiom;   

        for (int i = 1; i <= iterations; i++)
        {
            tree = GenerateNext(tree);
            Debug.Log(tree);
        }

        Treeifyinator(tree, woodMaterial);
        PrefabUtility.SaveAsPrefabAsset(empty, "Assets/Prefabs/Tree.prefab");
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

    void Treeifyinator(string tree, Material woodMaterial)
    {
        Vector3 currentPosition = new Vector3(0, 0, 0);

        foreach (char c in tree)
        {
            if (c == 'V')
            {
                Prefabify(currentPosition, woodMaterial);
                currentPosition += Vector3.up;
            }

            else if (c == 'L')
            {

            }

            else if (c == '[')
            {

            }

            else if (c == ']')
            {
                
            }
        }
    }

    void Prefabify(Vector3 position, Material woodMaterial) 
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = voxelObject.GetComponent<Renderer>();
        Material material = renderer.material;

        voxelObject.transform.position = position;
        renderer.material = woodMaterial;
        material.SetFloat("_Smoothness", 0.2f);
        voxelObject.transform.SetParent(empty.transform);
    }
}
