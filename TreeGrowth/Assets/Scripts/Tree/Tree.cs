using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

public class Tree : MonoBehaviour
{
    public static Tree Instance { get; private set; }

    [Header("Stats")]
    public int increaseGrowth = 1;
    [SerializeField] private int growth = 0;
    [SerializeField] private int fallLeafChance;
    

    [Header("Objects")]
    [SerializeField] private Exp expObj;

    [Header("Settings")]
    [SerializeField] Vector2 expSpwanRadius;

    private KeyInputEvents keyInputEvents = new();
    
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

    }

    // Update is called once per frame
    private void Update()
    {
        InputKey();
    }

    private void GainExp()
    {
        float x = UnityEngine.Random.Range(-expSpwanRadius.x, expSpwanRadius.x);
        float y = UnityEngine.Random.Range(-expSpwanRadius.y, expSpwanRadius.y);
        Vector2 spawnPos = new Vector2(x,y);

        var exp = Instantiate(expObj,spawnPos,Quaternion.identity);
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
            growth += increaseGrowth;
            Destroy(collision.gameObject);
        }
    }

}