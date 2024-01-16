using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static float Money = 0;
    public static int Leaf = 1000;



    [SerializeField] private Camera cam;
    [SerializeField] private int month;
    [SerializeField] private int year;
    private Vector3 mousePosition;

    private SeasonBase curSeason;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Destroy(this.gameObject);
        }
        curSeason = new Spring(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        curSeason.Passive();
    }

    public Vector3 GetMousePos()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
}
