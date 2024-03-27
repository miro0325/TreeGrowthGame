using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public SeasonType CurrentSeason
    {
        get
        {
            return seasonType;
        } 
        set
        {
            seasonType = value;
            if((int)value > (int)SeasonType.Winter)
            {
                seasonType = SeasonType.Spring;
            }
        }
    }

    [SerializeField] FadeScript fade;

    public static float Money = 0;
    public static int Leaf = 1000;

    public WeatherType weatherType = WeatherType.None;

    public List<Leaf> leafList = new List<Leaf>();
    public SeasonType seasonType = SeasonType.Spring;
    [SerializeField] private Camera cam;
    [SerializeField] private int month = 1;
    [SerializeField] private int year = 2077;
    [SerializeField] private float time;
    [SerializeField] private Transform stormPoint;
	[SerializeField] private Text date;
    [SerializeField] private ParticleSystem[] weatherEffects;
    [SerializeField] private ParticleSystem StormEffects;
    [SerializeField] private SpriteRenderer ground;
    [SerializeField] private MaidBot maidBot;
    private Vector3 mousePosition;

    private ISeasonBase curSeason;
    private ISeasonBase[] seasons = new ISeasonBase[4];
    private object[] seasonInfos = new object[4];
    private float curTime = 0;
    private int seasonMonth = 0;
    private int seasonIndex = 0;

    public MaidBot GetMaidBot()
    {
        return maidBot;
    }

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
        InitSeason();
    }

    private void InitSeason()
    {
        seasonInfos[0] = new SpringInfo
        {
            coolTime = 1,
            count = 10,
            spriteRenderer = ground,
        };
        seasonInfos[1] = new SummerInfo
        {
            chance = 20,
            multiply = 1.25f
        };
        seasonInfos[2] = new FallInfo
        {
            count = 4,
            multiply = 0.7f
        };
        seasonInfos[3] = new WinterInfo
        {
            count = 0,
            multiply = 0.4f,
            renderer = ground
        };
        seasons[0] = new Spring();
        seasons[1] = new Summer();
        seasons[2] = new Fall();
        seasons[3] = new Winter();
        for(int i = 0; i < seasons.Length; i++)
        {
            seasons[i].Init(seasonInfos[i]);
        }
        curSeason = seasons[seasonIndex];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();   
    }

    void UpdateTime()
    {
        curSeason?.Passive();
        curTime += Time.deltaTime;
        if(curTime >= time)
        {
            curTime = 0;
            month += 1;
            seasonMonth += 1;
            Debug.Log(seasonMonth);
            if (year >= 2097 || Tree.Instance.State == TreeState.Clear)
            {
                Invoke(nameof(ChangeScene), 1);
            }
            if(seasonMonth >= 3)
            {
                seasonMonth = 0;
                if(seasonIndex >= seasons.Length -1)
                {
                    seasonIndex = 0;
                } else
                {
                    seasonIndex++;
                }
                ChangeSeason();
            }
        }
        if(month > 12)
        {
            month -= 12;
            year++;
        }
        if(month < 10) 
            date.text = $"Date : {year} - 0{month}";
        else
            date.text = $"Date : {year} - {month}";

        
    }

    public void ChangeScene()
    {
        if ((int)Tree.Instance.State <= 2 ) SceneManager.LoadScene("Ending 2");
        else if ((int)Tree.Instance.State <= 4) SceneManager.LoadScene("Ending 1");
        else SceneManager.LoadScene("Ending");

    }

    private void ChangeSeason()
    {
        curSeason = seasons[seasonIndex];
        Debug.Log(curSeason);
        var par = weatherEffects[(int)seasonType];
        var parStorm = StormEffects;
        CurrentSeason++;
        
        par.Stop();
        StartCoroutine(ParticleSmoothDisable(par.gameObject, 1));
        
        if(weatherType != WeatherType.None) 
        {
            weatherType = WeatherType.None;
            Leaf /= 2;
            parStorm.Stop();
            StartCoroutine(ParticleSmoothDisable(parStorm.gameObject, 3));
        }

        curSeason?.Reset();
        curSeason?.SeasonEvent();
        if(weatherType == WeatherType.Storm) 
        {
            parStorm.gameObject.SetActive(true);
            parStorm.Play();
        }
        par = weatherEffects[(int)seasonType];
        par.gameObject.SetActive(true);
        
        par.Play();
    }

    private IEnumerator ParticleSmoothDisable(GameObject obj, int second)
    {
        yield return new WaitForSeconds(second);
        obj.SetActive(false);
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
