using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public List<GameObject> platforms;
    public int numOfPlatforms = 10;
    public GameObject board;
    public GameObject doodle;
    public Image background;
    private int number = 0;
    // Start is called before the first frame update
    public void PlatformsMove()
    {
        if (doodle.GetComponent<RectTransform>().anchoredPosition.y > background.GetComponent<RectTransform>().anchoredPosition.y + 100)
        {
            Generate();
        }
    }
    public void DeletePlatform(GameObject p)
    {
        Debug.Log("delete");
        platforms.Remove(p);
        Destroy(p);
    }
    public void GenerateBackground()
    {
        Image i = Instantiate(background);
        i.transform.SetParent(board.transform);
        i.GetComponent<RectTransform>().anchoredPosition = new Vector3(i.GetComponent<RectTransform>().anchoredPosition.x, 1080 * number, 0);
        number += 1;
        i.transform.localScale = new Vector3(1f, 1f, 1f);
        i.name = "Bg" + number;
        background = i;
    }
    public void GeneratePlatform(Image bg)
    {
        GameObject p = Instantiate(platform);
        p.transform.SetParent(board.transform);
        float y = bg.GetComponent<RectTransform>().anchoredPosition.y;
        p.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-300f, 300f), Random.Range(y, y + 1060), 0);
        p.transform.localScale = new Vector3(1f, 1f, 1f);
        p.GetComponent<PlatformCollider>().generator = this;
        platforms.Add(p);

    }
    void Generate()
    {
        GenerateBackground();
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GeneratePlatform(background);
        }
    }
    void Awake()
    {
        GenerateBackground();
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GeneratePlatform(background);
        }
        GenerateBackground();
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GeneratePlatform(background);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            if (doodle && platforms[i].GetComponent<RectTransform>().anchoredPosition.y < doodle.GetComponent<RectTransform>().anchoredPosition.y - 540)
            {
                DeletePlatform(platforms[i]);
            }
        }
    }
}
