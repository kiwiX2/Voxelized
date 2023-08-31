using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    TreeCreator treeCreator;
    
    void Start()
    {
        treeCreator = new TreeCreator();
    }

    public void GrowTrees()
    {
        treeCreator.CreateTree();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
