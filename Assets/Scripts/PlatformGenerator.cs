using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public List<GameObject> platforms;
    public int numOfPlatforms = 10;
    public GameObject board;
    public GameObject doodle;
    public GameObject background;
    public List<GameObject> backgrounds;
    public GameObject gameOver;
    public AudioSource looseSound;
    private bool check = true;

    private int count = 0;

    public void PlatformsMove()
    {
        if (doodle.GetComponent<RectTransform>().anchoredPosition.y > backgrounds[backgrounds.Count-1].GetComponent<RectTransform>().anchoredPosition.y + 100)
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
        float y = bg.GetComponent<RectTransform>().anchoredPosition.y;
        float rangeX = Random.Range(-200f, 200f);
        float rangeY = Random.Range(y + 20, y + bg.GetComponent<RectTransform>().sizeDelta.y / numOfPlatforms);
        if (i > 0)
        {
            float previousBackground = bg.GetComponent<RectTransform>().anchoredPosition.y;
            rangeY = Random.Range(previousBackground + bg.GetComponent<RectTransform>().sizeDelta.y / numOfPlatforms * i + 20, previousBackground + bg.GetComponent<RectTransform>().sizeDelta.y / numOfPlatforms * (i+1) - 20);
        }
        p.GetComponent<RectTransform>().anchoredPosition = new Vector3(rangeX, rangeY, 0);
        p.transform.localScale = new Vector3(1f, 1f, 1f);
        p.GetComponent<PlatformCollider>().generator = this;
        platforms.Add(p);

    }
    void Generate()
    {
        GenerateBackground();
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GeneratePlatform(backgrounds[backgrounds.Count-1], i);
        }
        count++;
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
            Debug.Log("das");
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
        for (int i = 0; i < platforms.Count; i++)
        {
            if (doodle && platforms[i].GetComponent<RectTransform>().anchoredPosition.y < doodle.GetComponent<RectTransform>().anchoredPosition.y - 540)
            {
                DeletePlatform(platforms[i]);
            }
            if (backgrounds.Count > 3)
            {
                DeleteBackground(backgrounds[0]);
            }
        }
    }
}
