using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


//키 입력 이벤트 클래스
public class KeyInputEvents
{
    private Dictionary<Func<bool>, Action> keyEvents = new();

    public void AddKeyEvent(Func<bool> key, System.Action value)
    {
        keyEvents.Add(key, value);
    }

    public void RemoveKeyEvent(Func<bool> key)
    {
        keyEvents.Remove(key);
    }

    public System.Action GetKeyEvent(Func<bool> key)
    {
        return keyEvents[key];
    }

    public Dictionary<Func<bool>, System.Action> GetKeyEvents()
    {
        return keyEvents;
    }
}

public enum TreeState
{
    Small = 0,
    Medium = 1,
    Big = 2,
    Large = 3,
    Huge = 4
}

public class Tree : MonoBehaviour
{
    public static Tree Instance { get; private set; }


    [Header("Stats")]
    
    public int increaseGrowth = 1;
    [SerializeField] private TreeState state;
    [SerializeField] private int growth = 0;
    [SerializeField] private int fallLeafChance;
    [SerializeField] private int doubleExpChance;
    [SerializeField] private int[] growthLevelLimits = new int[5];
    [SerializeField] private float[] growthLevelScales = new float[5];

    [Header("Objects")]
    [SerializeField] private Exp expObj;
    [SerializeField] private Exp doubleExpObj;
    [SerializeField] private GameObject lighting;
    [SerializeField] private Camera cam;

    [Header("Settings")]
    [SerializeField] Vector2 expSpwanRadius;
    [SerializeField] List<Color> levelUpColors = new();
    [SerializeField] float colorChangeDelay;

    private KeyInputEvents keyInputEvents = new();
    private Vector3 originPos;
    private Vector3 baseScale = Vector3.zero;
    private SpriteRenderer spriteRenderer;
    private int colorIndex = 0;
    private bool isShowLevelUpEffect = false;

    public bool IsLevelUp()
    {
        return growth == growthLevelLimits[(int)state];
    }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
        keyInputEvents.AddKeyEvent(() => Input.GetKeyDown(KeyCode.Space), GainExp);
        keyInputEvents.AddKeyEvent(() => Input.GetMouseButtonDown(0), () => {
            GainExp();
            LevelUp();
        });
        originPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        InputKey();
        UpdateGrowth();
        
    }

    private void LevelUp()
    {
        if (!IsLevelUp()) return;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0);
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Tree"))
            {
                state++;
                baseScale = Vector3.one * growthLevelScales[(int)state];
            }
        }
    }
    //레벨업 가능할 시 효과를 주는 함수
    private void ShowLevelUp()
    {
        isShowLevelUpEffect = true;
        if (!IsLevelUp())
        {
            spriteRenderer.DOColor(Color.white, colorChangeDelay);
            lighting.SetActive(false);
            isShowLevelUpEffect = false;
            return;
        }
        if(!lighting.activeSelf)
        {
            lighting.SetActive(true);
        }
        if (levelUpColors.Count > 0)
        {
            spriteRenderer.DOColor(levelUpColors[colorIndex], colorChangeDelay).OnComplete(() => { NextColor(); ShowLevelUp(); });  
        }
    }

    private void NextColor()
    {    
        if(colorIndex +1 >= levelUpColors.Count)
        {
            colorIndex = 0;
            return;
        }
        colorIndex++;
    }

    private void GainExp()
    {
        float x = UnityEngine.Random.Range(-expSpwanRadius.x, expSpwanRadius.x);
        float y = UnityEngine.Random.Range(-expSpwanRadius.y, expSpwanRadius.y);
        Vector2 spawnPos = new Vector2(x,y);
        int chance = UnityEngine.Random.Range(0, 100);
        if (chance <= doubleExpChance)
        {
            var exp = Instantiate(doubleExpObj, spawnPos, Quaternion.identity);
        } 
        else
        {
            var exp = Instantiate(expObj, spawnPos, Quaternion.identity);
        }
    }

    private void EarnExp(int value)
    {
        if((growth + value) > growthLevelLimits[(int)state])
        {
            growth = growthLevelLimits[(int)state];
            if (IsLevelUp() && !isShowLevelUpEffect) ShowLevelUp();
            return;
        }
        growth += value;
        if (IsLevelUp() && !isShowLevelUpEffect) ShowLevelUp();
    }

    private void UpdateGrowth()
    {
        float multiple = ((float)growth / (growthLevelLimits[(int)state] * 2));
        Vector3 scale = Vector3.one + baseScale + (Vector3.one * multiple);
        transform.DOScale(scale, 0.2f);
        transform.position = originPos + new Vector3(0,scale.y/2,0);
    }

    private void InputKey()
    {
        foreach(var ev in keyInputEvents.GetKeyEvents())
        {
            if(ev.Key.Invoke())
            {
                ev.Value.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, expSpwanRadius * 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EXP"))
        {
            if(collision.TryGetComponent(out Exp exp) == true)
            {
                if (exp.IsDouble()) {
                    EarnExp(increaseGrowth * 2);
                } else
                {
                    EarnExp(increaseGrowth);
                }
                Destroy(collision.gameObject);
            }
        }
    }

}
