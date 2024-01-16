
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Leaf : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float time;
    [SerializeField] private float speed;

    private KeyInputEvents keyEvent = new KeyInputEvents();
    private Vector3 startPos,endPos;
    private Rigidbody2D rigid;
    private bool isEarn = false;
    private Transform target;

    private bool isEarning = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }



    public void SetTarget(Transform _target)
    {
        target = _target;
        time = 0;
        rigid.gravityScale = 0;
        GameManager.Instance.leafList.Remove(this);
    }

    private void Init()
    {
        keyEvent.AddKeyEvent(() => Input.GetMouseButton(0), GetLeaf);
        keyEvent.AddKeyEvent(() => Input.GetMouseButtonDown(0), InitMousePos);
        keyEvent.AddKeyEvent(() => Input.GetMouseButtonUp(0), ResetLeaf);
        rigid = GetComponent<Rigidbody2D>();
    }

    private void InitMousePos()
    {
        if(target != null)
        {
            return;
        }
        startPos = transform.position;
        endPos = GameManager.Instance.GetMousePos();
        var dist = Vector2.Distance(startPos, endPos);
       
        if (dist > distance || isEarn)
        {
            return;
        }
        rigid.gravityScale = 0;
        time = 0;
        startPos = transform.position;
        endPos = GameManager.Instance.GetMousePos();
    }

    private void ResetLeaf()
    {
        if (target != null)
        {
            return;
        }
        rigid.gravityScale = 1;
        time = 0;
    }

    private void GetLeaf()
    {
        if (target != null)
        {
            return;
        }
        startPos = transform.position;
        endPos = GameManager.Instance.GetMousePos();
        var dist = Vector2.Distance(startPos, endPos);
        Debug.Log(dist);
        if (dist > distance || isEarn)
        {
            ResetLeaf();
            return;
        }
        rigid.gravityScale = 0;
        time += Time.deltaTime * speed * (0.5f/dist);
        var pos = Vector3.MoveTowards(startPos, endPos, time);
        pos.z = 0;
        transform.position = pos;
        if (dist < 0.5f)
        {
            isEarn = true;
            EarnLeaf();
        }
    }
    
    public bool IsEarning()
    {
        return isEarning;
    }

    public void EarnLeaf()
    {
        isEarning = true;
        GameManager.Instance.leafList.Remove(this);
        transform.DOScale(Vector2.zero, 0.3f).OnComplete(() =>
        {
            GameManager.Leaf++;
            Tree.Instance.SubtractLeafCount();
            Destroy(this.gameObject);
        });

    }

    private void KeyInput()
    {
        foreach (var ev in keyEvent.GetKeyEvents())
        {
            if (ev.Key.Invoke())
            {
                ev.Value.Invoke();
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        KeyInput(); 
        if(target != null)
        {
            startPos = transform.position;
            endPos = target.position;
            rigid.gravityScale = 0;
            var dist = Vector2.Distance(startPos, endPos);
            time += Time.deltaTime * speed * (0.5f / dist);
            var pos = Vector3.MoveTowards(startPos, endPos, time);
            pos.z = 0;
            transform.position = pos;
            if (dist < 0.5f)
            {
                isEarn = true;
                EarnLeaf();
            }
        }
    }
}
