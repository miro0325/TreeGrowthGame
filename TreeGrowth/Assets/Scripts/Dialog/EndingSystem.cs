using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSystem : MonoBehaviour
{
    Tree tree;

    private void Start()
    {
        tree = Tree.Instance;
    }

    void Update()
    {
        CheckEndingCondition();
    }
    void CheckEndingCondition()
    {
        if (tree != null && tree.Growth >= 1000000000000) // 나무의 성장도가 1조가 되면
        {
            ShowEndingScreen();
        }
    }

    void ShowEndingScreen()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
