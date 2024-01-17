using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
using UnityEngine.UI;
using System.Xml;

public class DialogSystem : MonoBehaviour
{

    public static DialogSystem Instance { get; set; }

    [SerializeField]
    public string[] DialogText
    {
        get { return dialogText; }
        set { dialogText = value; }
    }

    [SerializeField] FadeScript fade;

    [SerializeField] TextMeshProUGUI tempText;
    [SerializeField] float timeForChar;
    [SerializeField] float timeForChar_Fast;

    [SerializeField] string[] dialogText;


    [SerializeField] float charTime;

    [SerializeField] float timer;

    [SerializeField] string saves;

    [SerializeField] bool isDialogEnd = false;
    [SerializeField] bool isTypingEnd = false;

    [SerializeField] bool isEnding = false;

    [SerializeField] int dialogNumber = 0;

    [SerializeField] TMP_Text EndingDate;

    Coroutine coroutine = null;
    public bool IsTypingEnd()
    {
        return isTypingEnd;
    }


    public void Typing(string dialogs, TextMeshProUGUI text)
    {
        if (coroutine != null)  // 이미 실행 중인 코루틴이 있다면 중복 호출 방지
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

        isTypingEnd = true;  // 이 부분을 수정하여 코루틴이 완료될 때마다 isTypingEnd를 true로 설정

        // 대화 번호가 배열의 범위를 초과하지 않도록 보호
        if (dialogNumber < dialogText.Length)
        {
            dialogNumber++;
            coroutine = null;
        }

        coroutine = null;  // 코루틴이 완료되면 coroutine 변수를 null로 설정

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
        if(AudioManager.instance != null)
            AudioManager.instance.PlayMusic("IntroSound");
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
                if (!isEnding)
                {
                    SceneManager.LoadScene("TutorialScene");

                }
                if (isEnding)
                {
                    SceneManager.LoadScene("Title");

                }
                return;
            }
            if (!isTypingEnd) EndTyping();
            else Typing(dialogText[dialogNumber], tempText);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}