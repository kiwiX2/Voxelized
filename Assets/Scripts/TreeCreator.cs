using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class TreeCreator
{
    GameObject empty;

    string[] rules = {
        "V[V[L]]",
        "V[]",
        "V[L[L]]",
        "V[L]L"
    };
    string axiom = "V";
    int iterations = 1;
    int leanX = 0;
    int leanZ = 0;
    int tempLean;

    public void CreateTree(Material woodMaterial, Material leafMaterial)
    {
        for (int i = 1; i < Random.Range(2, 5); i++)
        {
            axiom += "V";
        }
        
        string tree = axiom;   
        empty = new GameObject("Tree");

        for (int i = 1; i <= iterations; i++)
        {
            tree = GenerateNext(tree);
        }

        Treeifyinator(tree, woodMaterial, leafMaterial);
        PrefabUtility.SaveAsPrefabAsset(empty, "Assets/Prefabs/Tree.prefab");
    }

    string GenerateNext(string tree)
    {
        string output = "";

        foreach (char c in tree) 
        {
            int rngRule = Random.Range(0, rules.Length);
            if (c == 'V') 
            {
                output += "VV[VLL]V";
            } 

            // USE RANDOM IOÖASFUOPDVOIUGOHAISUGVSYAUOUASYGVYUADSCILUGAGILSUCXAUILGLGKUGUICLÖVSALIUV

            else if (c == 'L')
            {
                output += "L[VV]";
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
                case 'L':
                    currentPosition += new Vector3(leanX, 0, leanZ);
                    Prefabify(currentPosition, woodMaterial, 1);
                    currentPosition += Vector3.up;
                    break;

                case '[':
                    leanX = Random.Range(-1, 2);
                    leanZ = Random.Range(-1, 2);

                    currentPosition += new Vector3(leanX, 0, leanZ);
                    branches.Add(currentPosition);
                    break;

                case ']':
                    leaves.Add(currentPosition);
                    CreateLeaves(leaves, leafMaterial); 

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

    void CreateLeaves(List<Vector3> leaves, Material leafMaterial)
    {
        Vector3 originPos = leaves.Last();
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
