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
        if (tree != null && tree.Growth >= 10000000) // ������ ���嵵�� 1���� �Ǹ�
        {
            ShowEndingScreen();
        }
    }

    void ShowEndingScreen()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
