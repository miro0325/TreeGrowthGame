using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

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
    
    [Header("Objects")]
    [SerializeField] private Exp expObj;
    [SerializeField] private Exp doubleExpObj;

    [Header("Settings")]
    [SerializeField] Vector2 expSpwanRadius;

    private KeyInputEvents keyInputEvents = new();
    private Vector3 originPos;

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
        keyInputEvents.AddKeyEvent(() => Input.GetMouseButtonDown(0), GainExp);
        originPos = transform.position;

    }

    // Update is called once per frame
    private void Update()
    {
        InputKey();
        UpdateGrowth();
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

    private void UpdateGrowth()
    {
        float multiple = ((float)growth / 1000f);
        Vector3 scale = Vector3.one + (Vector3.one * multiple) + (Vector3.one * (int)state);
        transform.DOScale(scale, 0.05f);
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
                growth += (exp.IsDouble()) ? increaseGrowth * 2 : increaseGrowth;
                Destroy(collision.gameObject);
            }
        }
    }

}
