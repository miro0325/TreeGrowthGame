using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using DG.Tweening;


//Ű �Է� �̺�Ʈ Ŭ����
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
    Huge = 4,
    Giant = 5,
    Clear = 6,
}

public class Tree : MonoBehaviour
{
    public static Tree Instance { get; private set; }

    [Header("Stats")]
    
    public int increaseGrowth = 1;
    [SerializeField] private TreeState state;
    [SerializeField] private int growth = 0;
    [SerializeField] private float fallLeafChance;
    [SerializeField] private int doubleExpChance;
    [SerializeField] private float leafDropDelay;
    [SerializeField] private int[] growthLevelLimits = new int[5];
    [SerializeField] private float[] growthLevelScales = new float[5];

    [Header("Objects")]
    [SerializeField] private Exp expObj;
    [SerializeField] private Exp doubleExpObj;
    [SerializeField] private Leaf[] leaves = new Leaf[3];
    [SerializeField] private Sprite[] trees = new Sprite[5];
    [SerializeField] private GameObject lighting;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private Camera cam;

    [Header("Settings")]
    [SerializeField] Vector2 expSpwanRadius;
    [SerializeField] List<Color> levelUpColors = new();
    [SerializeField] float colorChangeDelay;
    [SerializeField] int maxLeafCount;
    

    private KeyInputEvents keyInputEvents = new();
    private Vector3 originPos;
    private Vector3 baseScale = Vector3.zero;
    private SpriteRenderer spriteRenderer;
    private int colorIndex = 0;
    private float curLeafDropTime = 0;
    private bool isShowLevelUpEffect = false;
    private int curLeafCount;
    private float extraLeafChance;
    private float expMultiply;
    private int extraCount = 2;

    public int Growth
    {
        get
        {
            return growth;
        }
    }

    public TreeState State
    {
        get
        {
            return state;
        }
    }

    public bool IsLevelUp()
    {
        if(state == TreeState.Clear) return false;
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
        keyInputEvents.AddKeyEvent(() => Input.GetKeyDown(KeyCode.Space), () => GainExp(1,false));
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
        DropLeaf(extraCount + (int)state);
        curLeafDropTime += Time.deltaTime;
    }



    public void SubtractLeafCount()
    {
        curLeafCount--;
    }

    public void SetExtraLeafCount(int value)
    {
        extraCount = value; 
    }

    public Vector2 GetExpSpawnSize()
    {
        return expSpwanRadius;
    }

    public void SetExpMultiplier(float value)
    {
        expMultiply = value;
    }

    public void SetExtraLeafChance(float value)
    {
        extraLeafChance = value;
    }

    public void AddLeafChance(float value)
    {
        fallLeafChance += value;
    }

    public void DropLeaf(int count)
    {
        
        if(curLeafDropTime >= leafDropDelay)
        {
            curLeafDropTime = 0;
            if(curLeafCount >= maxLeafCount)
            {
                return;
            }
            float chance = UnityEngine.Random.Range(0,100f);
            
            if((fallLeafChance + extraLeafChance) >= chance)
            {
                
                if((fallLeafChance + extraLeafChance)/2 >= chance)
                {
                    count *= 2;
                }
            } else
            {
                return;
            }
            //Debug.Log(count);
            float x, y;
            for(int i = 0; i < count; i++)
            {
                x = UnityEngine.Random.Range(-transform.localScale.x, transform.localScale.x);
                y = UnityEngine.Random.Range(-transform.localScale.y, transform.localScale.y);
                var spawnPos = transform.position + new Vector3(x, y, 0);
                var _leaf = Instantiate(leaves[(int)GameManager.Instance.seasonType], spawnPos, Quaternion.identity);
                curLeafCount++;
                GameManager.Instance.leafList.Add(_leaf);
                _leaf.transform.localScale = Vector3.one * 0.5f + Vector3.one * (((float)state / 6));
            }
        }
    }

    private void LevelUp()
    {
        if (!IsLevelUp() && EventSystem.current.IsPointerOverGameObject()) return;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0);
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Tree"))
            {
                if(IsLevelUp())
                {
                    state++;
                    if(trees.Length > (int)state)
                        spriteRenderer.sprite = trees[(int)state];
                    baseScale = Vector3.one * growthLevelScales[(int)state];
                }
            }
        }
    }
    //������ ������ �� ȿ���� �ִ� �Լ�
    private void ShowLevelUp()
    {
        isShowLevelUpEffect = true;
        if (!IsLevelUp())
        {
            spriteRenderer.DOColor(Color.white, colorChangeDelay);
            lighting.SetActive(false);
            
            levelUpEffect.gameObject.SetActive(false);

            isShowLevelUpEffect = false;
            return;
        }
        if(!lighting.activeSelf)
        {
            lighting.SetActive(true);
            levelUpEffect.gameObject.SetActive(true);
            levelUpEffect.Play();
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

    public void GainExp(int count = 1,bool isAuto = false)
    {
        if(!isAuto)
            if (EventSystem.current.IsPointerOverGameObject()) return;
        
        for(int i = 0; i < count; i++)
        {
            float x = UnityEngine.Random.Range(-expSpwanRadius.x, expSpwanRadius.x);
            float y = UnityEngine.Random.Range(-expSpwanRadius.y, expSpwanRadius.y);
            Vector2 spawnPos = new Vector2(x, y);
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
        
    }

    private void EarnExp(int value)
    {
        if((growth + Mathf.RoundToInt(value * expMultiply)) > growthLevelLimits[(int)state])
        {
            growth = growthLevelLimits[(int)state];
            if (IsLevelUp() && !isShowLevelUpEffect) ShowLevelUp();
            return;
        }
        growth += Mathf.RoundToInt(value * expMultiply);
        if (IsLevelUp() && !isShowLevelUpEffect) ShowLevelUp();
    }

    private void UpdateGrowth()
    {
        float multiple = ((float)growth / (growthLevelLimits[(int)state] * 2));
        Vector3 scale = Vector3.one * 3 + baseScale + (Vector3.one * multiple);
        transform.DOScale(scale, 0.2f);
        transform.position = originPos + new Vector3(0,scale.y/4,0);
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
