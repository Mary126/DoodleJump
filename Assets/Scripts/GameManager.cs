using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject platform;
    public List<GameObject> platforms;
    public int numOfPlatforms = 10;
    public int miniumPlatforms = 6;
    public GameObject board;
    public GameObject doodle;
    public GameObject background;
    public GameObject enemy;
    public Canvas canvas;
    public List<GameObject> backgrounds;
    public GameObject gameOver;
    public AudioSource looseSound;
    private bool check = true;
    private int possibl = 40;

    private int count = 0;

    public void PlatformsMove()
    {
        if (doodle.GetComponent<RectTransform>().anchoredPosition.y > backgrounds[backgrounds.Count - 1].GetComponent<RectTransform>().anchoredPosition.y + 100)
        {
            Generate();
        }
    }
    public void DeletePlatform(GameObject p)
    {
        platforms.Remove(p);
        Destroy(p);
    }
    public void GenerateBackground()
    {
        GameObject i = Instantiate(background);
        i.transform.SetParent(board.transform);
        i.GetComponent<RectTransform>().anchoredPosition = new Vector3(i.GetComponent<RectTransform>().anchoredPosition.x, 1080 * count, 0);
        i.name = "Bg" + count.ToString(); ;
        i.transform.localScale = new Vector3(1f, 1f, 1f);
        backgrounds.Add(i);
    }
    public void DeleteBackground(GameObject b)
    {
        backgrounds.Remove(b);
        Destroy(b);

    }
    public void GeneratePlatform(GameObject bg, int i)
    {
        GameObject p = Instantiate(platform);
        p.transform.SetParent(board.transform);
        float backgroundPosition = bg.GetComponent<RectTransform>().anchoredPosition.y;
        float backgroundSize = bg.GetComponent<RectTransform>().sizeDelta.y;
        float rangeX = Random.Range(-200f, 200f);
        float rangeY = Random.Range(backgroundPosition + 20, backgroundPosition + backgroundSize / numOfPlatforms);
        if (i > 0)
        {
            float previousBackground = bg.GetComponent<RectTransform>().anchoredPosition.y;
            rangeY = Random.Range(previousBackground + backgroundSize / numOfPlatforms * i + 20, previousBackground + backgroundSize / numOfPlatforms * (i + 1) - 20);
        }
        p.GetComponent<RectTransform>().anchoredPosition = new Vector3(rangeX, rangeY, 0);
        p.transform.localScale = new Vector3(1f, 1f, 1f);
        p.GetComponent<PlatformCollider>().manager = this;
        platforms.Add(p);

    }
    void GenerateEnemy(GameObject plat)
    {
        GameObject p = Instantiate(enemy);
        p.transform.SetParent(canvas.transform);
        p.GetComponent<RectTransform>().anchoredPosition = new Vector2(plat.GetComponent<RectTransform>().anchoredPosition.x, plat.GetComponent<RectTransform>().anchoredPosition.y + 50);
        p.transform.localScale = new Vector3(1f, 1f, 1f);
        p.GetComponent<EnemyControll>().enemy = p;
        p.GetComponent<EnemyControll>().doodle = doodle;
    }
    void Generate()
    {
        GenerateBackground();
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GeneratePlatform(backgrounds[backgrounds.Count - 1], i);
        }
        float possibility = Random.Range(1, possibl);
        if (possibility / possibl > 0.7)
        {
            GenerateEnemy(platforms[platforms.Count - Random.Range(1, 5)]);
        }
        count++;
        if (count == 6)
        {
            if (numOfPlatforms > miniumPlatforms)
            {
                numOfPlatforms -= 2;
            }
            possibility -= 10;
        }
        else if (count == 12)
        {
            if (numOfPlatforms > miniumPlatforms)
            {
                numOfPlatforms -= 3;
            }
            possibility -= 10;
        }
    }
    void Awake()
    {
        Generate();
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (doodle && check && doodle.GetComponent<RectTransform>().anchoredPosition.y <= platforms[0].GetComponent<RectTransform>().anchoredPosition.y - 20)
        {
            looseSound.Play(0);
            GameObject gameOverObj = Instantiate(gameOver);
            gameOverObj.transform.SetParent(board.transform);
            gameOverObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, backgrounds[0].GetComponent<RectTransform>().anchoredPosition.y - 700);
            gameOverObj.transform.localScale = new Vector3(1f, 1f, 1f);
            check = false;
        }
        if (doodle && doodle.GetComponent<RectTransform>().anchoredPosition.y < backgrounds[0].GetComponent<RectTransform>().anchoredPosition.y)
        {
            doodle.GetComponent<DoodleMovement>().loose = true;
        }
        if (doodle && doodle.GetComponent<RectTransform>().anchoredPosition.y < backgrounds[0].GetComponent<RectTransform>().anchoredPosition.y - 1080)
        {
            Destroy(doodle);
        }
        if (doodle && platforms[0].GetComponent<RectTransform>().anchoredPosition.y < doodle.GetComponent<RectTransform>().anchoredPosition.y - 540)
        {
            DeletePlatform(platforms[0]);

        }
        if (backgrounds.Count > 3)
        {
            DeleteBackground(backgrounds[0]);
        }
    }
}
