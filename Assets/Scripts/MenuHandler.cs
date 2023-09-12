using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Material woodMaterial;
    public Material leafMaterial;
    public Button startButton;
    TreeCreator treeCreator;

    public void GrowTrees()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject tree = GameObject.Find("Tree" + i);
            Destroy(tree);
            treeCreator = new TreeCreator();
            treeCreator.CreateTree(woodMaterial, leafMaterial, i);
        }

        startButton.interactable = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
