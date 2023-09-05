using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class TreeCreator
{
    GameObject empty;

    string axiom = "V";
    int iterations = 3;
    int leanX = 0;
    int leanZ = 0;

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
                output += "V[VLL]V";
            } 

            else if (c == 'L')
            {
                output += "LL[VV]L";
            }
        }

        return output;
    }

    void Treeifyinator(string tree, Material woodMaterial)
    {
        Vector3 currentPosition = new Vector3(0, 0, 0);
        List<Vector3> branches = new List<Vector3>();

        foreach (char c in tree)
        {
            if (c == 'V' || c == 'L')
            {
                currentPosition += new Vector3(leanX, 0, leanZ);
                Prefabify(currentPosition, woodMaterial);
                currentPosition += Vector3.up;
            }

            else if (c == 'L')
            {

            }

            else if (c == '[')
            {
                leanX = Random.Range(-1, 2);
                leanZ = Random.Range(-1, 2);
                currentPosition += new Vector3(leanX, 0, leanZ);
                branches.Add(currentPosition);
            }

            else if (c == ']')
            {   
                leanX = leanX * -1;
                leanZ = leanZ * -1;
                currentPosition = branches.Last() + new Vector3(leanX, 0, leanZ);
            }

            Prefabify(currentPosition, woodMaterial);
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
