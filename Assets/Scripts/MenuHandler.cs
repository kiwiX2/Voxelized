using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Material woodMaterial;
    public Material leafMaterial;
    TreeCreator treeCreator;
    
    void Start()
    {
        treeCreator = new TreeCreator();
    }

    public void GrowTrees()
    {
        treeCreator.CreateTree(woodMaterial, leafMaterial);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
