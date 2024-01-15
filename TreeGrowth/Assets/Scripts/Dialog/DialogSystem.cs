using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; set; }

    [SerializeField]
    public string[] DialogText
    {
        get { return dialogText; }
        set { dialogText = value; }
    }

    [SerializeField] TextMeshProUGUI tempText;
    [SerializeField] float timeForChar;
    [SerializeField] float timeForChar_Fast;

    [SerializeField] string[] dialogText;

    [SerializeField] float charTime;

    [SerializeField] float timer;

    [SerializeField] string saves;

    [SerializeField] bool isDialogEnd = false;
    [SerializeField] bool isTypingEnd = false;

    [SerializeField] int dialogNumber = 0;

    Coroutine coroutine = null;

    public bool IsTypingEnd()
    {
        return isTypingEnd;
    }


    public void Typing(string dialogs, TextMeshProUGUI text)
    {
        if (coroutine != null)  // �̹� ���� ���� �ڷ�ƾ�� �ִٸ� �ߺ� ȣ�� ����
        {
            return;
        }

        isDialogEnd = false;
        saves = dialogs;
        tempText = text;
        coroutine = StartCoroutine(Typer(dialogs.ToCharArray(), text));

    }

    IEnumerator Typer(char[] chars, TextMeshProUGUI text)
    {
        int currentChar = 0;
        charTime = timeForChar;
        int charLength = chars.Length;
        isTypingEnd = false;
        text.text = "";
        while (currentChar < charLength)
        {
            if (timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
            else
            {
                text.text += chars[currentChar].ToString();
                currentChar++;
                timer = charTime;
            }
        }
        if (currentChar >= charLength)
        {
            isTypingEnd = true;
            dialogNumber++;
            coroutine = null;
            yield break;
        }

        isTypingEnd = true;  // �� �κ��� �����Ͽ� �ڷ�ƾ�� �Ϸ�� ������ isTypingEnd�� true�� ����

        // ��ȭ ��ȣ�� �迭�� ������ �ʰ����� �ʵ��� ��ȣ
        if (dialogNumber < dialogText.Length)
        {
            dialogNumber++;
            coroutine = null;
        }

        coroutine = null;  // �ڷ�ƾ�� �Ϸ�Ǹ� coroutine ������ null�� ����

    }

    public void EndTyping()
    {
        if (!isTypingEnd && !tempText.Equals(null) && coroutine != null)
        {
            tempText.text = saves;
            isTypingEnd = true;
            StopCoroutine(coroutine);
            coroutine = null;
        }
        if (!isTypingEnd)
        {
            charTime = timeForChar_Fast;
        }

        if (dialogNumber >= dialogText.Length)
        {
            isDialogEnd = true;
        }

        if (dialogNumber < dialogText.Length)
        {
            dialogNumber++;
            coroutine = null;
        }
    }


    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        if (dialogText.Length > 0)
        {
            Typing(dialogText[0], tempText);
        }
    }

    void Update()
    {
        if (dialogNumber >= dialogText.Length) isDialogEnd = true;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isDialogEnd && isTypingEnd)
            {
                SceneManager.LoadScene("Ingame");
                return;
            }
            if (!isTypingEnd) EndTyping();
            else Typing(dialogText[dialogNumber], tempText);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Skip();
        }
    }

    public void Skip()
    {
        isDialogEnd = true;
        isTypingEnd = true;
    }

    public void OnClickSkipButton()
    {
        Skip();
    }
}