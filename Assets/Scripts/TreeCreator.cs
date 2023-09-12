using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class TreeCreator
{
    Material woodMaterial;
    Material leafMaterial;
    GameObject empty;

    string[] rules = {
        "V[VV][V]V",
        "VV[VVV]V",
        "V[V[V]V]",
        "V[V]V",
        "V[VV]"
    };
    string axiom = "V";
    int leanX = 0;
    int leanZ = 0;
    int tempLean;

    public void CreateTree(Material woodMat, Material leafMat, int iteration)
    {
        woodMaterial = woodMat;
        leafMaterial = leafMat;

        for (int i = 1; i < Random.Range(2, 6); i++)
        {
            axiom += "V";
        }
        
        string tree = axiom;
        empty = new GameObject("Tree" + iteration);

        tree = GenerateNext(tree);

        Treeifyinator(tree, woodMaterial, leafMaterial);
        PrefabUtility.SaveAsPrefabAsset(empty, "Assets/Resources/Tree" + iteration + ".prefab");
        empty.transform.localPosition = new Vector3(-15f + 15f * iteration, -10f, 25f);
    }

    string GenerateNext(string tree)
    {
        string output = "";

        foreach (char c in tree) 
        {
            int rngRule = Random.Range(0, rules.Length);
            if (c == 'V') 
            {
                output += rules[rngRule];
            } 
        }

        return output;
    }

    void Treeifyinator(string tree, Material woodMaterial, Material leafMaterial)
    {
        Vector3 currentPosition = new Vector3(0, 0, 0);
        List<Vector3> branches = new List<Vector3>();
        List<Vector3> leaves = new List<Vector3>();

        tree = tree.Insert(0, "VV");
        tree = tree.Insert(tree.Length, "]");

        foreach (char c in tree)
        {
            switch (c)
            {
                case 'V':
                    currentPosition += new Vector3(leanX, 0, leanZ);
                    Prefabify(currentPosition, woodMaterial, 0.8f);
                    currentPosition += Vector3.up;
                    break;

                case '[':
                    leanX = Random.Range(-1, 2);
                    leanZ = Random.Range(-1, 2);

                    currentPosition += new Vector3(leanX, 0, leanZ);
                    branches.Add(currentPosition);
                    break;

                case ']':
                    CreateLeaves(leaves, currentPosition, leafMaterial); 

                    GetNewLean();
                    currentPosition = branches.Last() + new Vector3(leanX, 0, leanZ);
                    break;
            }

            Prefabify(currentPosition, woodMaterial, 1);
        }
    }

    void GetNewLean()
    {
        tempLean = Random.Range(-1, 2);

        for (; tempLean == leanX;)
        {
            tempLean = Random.Range(-1, 2);
        }

        leanX = tempLean;

        for (; tempLean == leanZ || tempLean == leanX;)
        {
            tempLean = Random.Range(-1,2);
        }

        leanZ = tempLean;
    }

    void CreateLeaves(List<Vector3> leaves, Vector3 originPos, Material leafMaterial)
    {
        int leafAmount = 75;

        for (int i = 0; i < leafAmount; i++)
        {
            float leafScale = Random.Range(0.3f, 0.7f);
            Vector3 leafPosition = originPos + new Vector3(
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)
            );
            Prefabify(leafPosition, leafMaterial, leafScale);
        }
    }

    void Prefabify(Vector3 position, Material voxelMaterial, float voxelScale) 
    {
        GameObject voxelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = voxelObject.GetComponent<Renderer>();
        Material material = renderer.material;

        voxelObject.transform.localScale = new Vector3(voxelScale, voxelScale, voxelScale);
        voxelObject.transform.position = position;
        renderer.material = voxelMaterial;
        material.SetFloat("_Smoothness", 0.2f);
        voxelObject.transform.SetParent(empty.transform);
    }
}
