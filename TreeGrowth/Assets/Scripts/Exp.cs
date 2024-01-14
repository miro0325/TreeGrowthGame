using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{   
    
    [SerializeField] private bool isDouble = false;
    [SerializeField] private int growth;
    [SerializeField] private float power;
    [SerializeField] private float time;
    [SerializeField] private float speed;
    private Vector3 startPos;
    private Vector3 middlePos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        middlePos = transform.position + ((Vector3)Random.insideUnitCircle * power);
        endPos = Tree.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        transform.position = CalculateBezier(startPos, middlePos, endPos,time);
    }
    //2배 경험치인지 확인용 함수
    public bool IsDouble()
    {
        return isDouble;
    }

    private Vector3 CalculateBezier(Vector3 pos1, Vector3 pos2, Vector3 pos3, float t)
    {
        var p12 = Vector3.Lerp(pos1, pos2, t);
        var p23 = Vector3.Lerp(pos2, pos3, t);

        var p1223 = Vector3.Lerp(p12, p23, t);

        return p1223;
    }
}
