using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidBot : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private int flipX = 1;
    [SerializeField] private float distance;
    [SerializeField] private float pickCooltime;
    [SerializeField] private float expCooltime;
    [SerializeField] private Vector2 offsetSize;

    public int maxCount;
    private int curCount = 0;

    private bool isPick = false;
    private float curPickTime = 0;
    private float curExpTime = 0;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Skill();
    }

    void Movement()
    {
        transform.Translate(Vector2.left * flipX * Time.deltaTime * moveSpeed);
        Flip();
    }

    private void Skill()
    {
        curPickTime += Time.deltaTime;
        if(curPickTime > pickCooltime) { 
            PickUpLeaf();

            Tree.Instance.GainExp(maxCount, true);
            
            curPickTime = 0;
        }
    }

    private void PickUpLeaf()
    {
        List<Leaf> leaves = new List<Leaf>();
        var g = GameManager.Instance;
        for (int i = 0; i < maxCount; i++)
        {
            int random = Random.Range(0, g.leafList.Count);
            if(random >= g.leafList.Count)
            {
                continue;
            }
            try
            {
                while (!leaves.Contains(g.leafList[random]))
                {
                    random = Random.Range(0, g.leafList.Count);
                    leaves.Add(g.leafList[random]);
                    g.leafList[i].SetTarget(transform);
                }

            } catch
            {
                continue;
            }
            
            if(g.leafList.Count <= i) {
                break;
            }
            if (i <= g.leafList.Count) {
                break;
            }
        }
    }

    private void Flip()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.left * flipX,distance,LayerMask.GetMask("Wall"));
        Debug.DrawRay(transform.position, Vector2.left * flipX, Color.green,distance);
        if(hit.collider != null)
        {
            
            //Debug.Log(hit.collider.gameObject.name);
            flipX *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, offsetSize);
    }
}
