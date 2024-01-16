using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static float Money = 0;
    public static int Leaf = 1000;

    public WeatherType weatherType = WeatherType.None;

    public List<Leaf> leafList = new List<Leaf>();

    [SerializeField] private Camera cam;
    [SerializeField] private int month = 1;
    [SerializeField] private int year = 2077;
    [SerializeField] private float time;
    [SerializeField] private Transform stormPoint;
    private Vector3 mousePosition;

    private SeasonBase curSeason;
    private SeasonBase[] seasons = new SeasonBase[4];
    private float curTime = 0;

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
        seasons[0] = new Spring(1, 5);
        seasons[1] = new Summer(20, 1.25f);
        seasons[2] = new Fall(4, 0.7f);
        seasons[3] = new Winter(1, 0.4f);
        curSeason = seasons[0];
    }

    // Update is called once per frame
    void Update()
    {
        curSeason.Passive();
        
        UpdateTime();
        
    }

    void UpdateTime()
    {
        curTime += Time.deltaTime;
        if(curTime >= time)
        {
            curTime = 0;
            month += 3;
            ChangeSeason();
        }
        if(month > 12)
        {
            month -= 12;
            year++;
        }
    }

    private void ChangeSeason()
    {
        if(month < 3)
        {
            curSeason = seasons[0];
        }
        else if(month > 3 && month <= 6)
        {
            curSeason = seasons[1];
        }
        else if(month > 6 && month <= 9)
        {
            curSeason = seasons[2];
        }
        else if(month > 9 && month <= 12)
        {
            curSeason = seasons[3];
        }

        if(weatherType != WeatherType.None) weatherType = WeatherType.None;

        curSeason.SeasonEvent();
    }

    public Vector3 GetMousePos()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

    public Vector3 GetStormPos()
    {
        return stormPoint.position;
    }
}
