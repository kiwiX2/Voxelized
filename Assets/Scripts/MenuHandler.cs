using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuHandler : MonoBehaviour
{
    public Material woodMaterial;
    
    public void GrowTrees()
    {
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
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
