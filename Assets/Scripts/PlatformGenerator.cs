using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public List<GameObject> platforms;
    public GameObject[] pl;
    public int numOfPlatforms = 10;
    public GameObject board;
    public GameObject doodle;
    public Image background;
    private int number = 1;
    // Start is called before the first frame update
    public void PlatformsMove()
    {
        if (doodle.GetComponent<RectTransform>().anchoredPosition.y >= background.GetComponent<RectTransform>().anchoredPosition.y)
        {
            for (int j = 0; j < platforms.Count; j++)
            {
                platforms[j].GetComponent<RectTransform>().anchoredPosition = new Vector3(platforms[j].GetComponent<RectTransform>().anchoredPosition.x, platforms[j].GetComponent<RectTransform>().anchoredPosition.y - platforms[j].GetComponent<RectTransform>().sizeDelta.x);
            }
            GenerateBackground();
            GeneratePlatforms(background);
        }
    }
    public void GenerateBackground()
    {
        Image i = Instantiate(background);
        i.transform.SetParent(board.transform);
        i.GetComponent<RectTransform>().anchoredPosition = new Vector3(i.GetComponent<RectTransform>().anchoredPosition.x, 1080 * number, 0);
        number += 1;
        i.transform.localScale = new Vector3(1f, 1f, 1f);
        background = i;
    }
    public void GeneratePlatforms(Image bg)
    {
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GameObject p = Instantiate(platform);
            p.transform.SetParent(board.transform);
            float y = bg.GetComponent<RectTransform>().anchoredPosition.y;
            p.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-300f, 300f), Random.Range(y+10, 1080 * number), 0);
            p.transform.localScale = new Vector3(1f, 1f, 1f);
            p.GetComponent<PlatformCollider>().index = i;
            p.GetComponent<PlatformCollider>().generator = this;
            platforms.Add(p);
        }
    }
    void Awake()
    {
        GeneratePlatforms(background);
        GenerateBackground();
        GeneratePlatforms(background);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
