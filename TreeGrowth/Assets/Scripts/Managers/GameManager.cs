using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

    private SeasonBase curSeason;
    private SeasonBase[] seasons = new SeasonBase[4];
    private float curTime = 0;
    private int seasonMonth = 0;

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
        seasons[0] = new Spring(1, 10,ground);
        seasons[1] = new Summer(20, 1.25f);
        seasons[2] = new Fall(4, 0.7f);
        seasons[3] = new Winter(0, 0.4f,ground);
        curSeason = seasons[0];
        curSeason.Init();
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
            month += 1;
            seasonMonth += 1;
            Debug.Log(seasonMonth);
            if(seasonMonth >= 3)
            {
                seasonMonth = 0;
                ChangeSeason();
            }
        }
        if(month > 12)
        {
            month -= 12;
            //ChangeSeason();
            year++;
        }
        if(month < 10) 
            date.text = $"Date : {year} - 0{month}";
        else
            date.text = $"Date : {year} - {month}";

        if(year >= 2097)
        {
            Invoke(nameof(ChangeScene), 1);
        }
    }

    public void ChangeScene()
    {
        if ((int)Tree.Instance.State <= 2 ) SceneManager.LoadScene("Ending 2");
        else if ((int)Tree.Instance.State <= 4) SceneManager.LoadScene("Ending 1");
        else SceneManager.LoadScene("Ending");

    }

    private void ChangeSeason()
    {
        var par = weatherEffects[(int)seasonType];
        var parStorm = StormEffects;
        //var setting = par.main;
        //setting.loop = false;
        par.Stop();
        StartCoroutine(ParticleSmoothDisable(par.gameObject, 1));
        if(month >= 12 || month < 3)
        {
            curSeason = seasons[3];
            seasonType = SeasonType.Winter;
        }
        else if(month >= 3 && month < 6)
        {
            curSeason = seasons[0];
            seasonType = SeasonType.Spring;
        }
        else if(month >= 6 && month < 9)
        {
            curSeason = seasons[1];
            seasonType = SeasonType.Summer;
        }
        else if(month >= 9 && month < 12)
        {
            curSeason = seasons[2];
            seasonType = SeasonType.Fall;
        }

        if(weatherType != WeatherType.None) 
        {
            weatherType = WeatherType.None;
            Leaf /= 2;
            parStorm.Stop();
            StartCoroutine(ParticleSmoothDisable(parStorm.gameObject, 3));
        }

        curSeason.Init();
        curSeason.SeasonEvent();
        if(weatherType == WeatherType.Storm) 
        {
            parStorm.gameObject.SetActive(true);
            parStorm.Play();
        }
        par = weatherEffects[(int)seasonType];
        par.gameObject.SetActive(true);
        //setting = par.main;
        //setting.loop = true;
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
