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
    public Image background;
    public GameObject doodle;
    // Start is called before the first frame update
    public void PlatformsMove()
    {
        Debug.Log(platforms.Count);
        for (int j = 0; j < platforms.Count; j++)
        {
            platforms[j].GetComponent<RectTransform>().anchoredPosition = new Vector3(platforms[j].GetComponent<RectTransform>().anchoredPosition.x, platforms[j].GetComponent<RectTransform>().anchoredPosition.y - platforms[j].GetComponent<RectTransform>().sizeDelta.x);
        }
    }
    public void GeneratePlatforms()
    {
        for (int i = 0; i < numOfPlatforms; i++)
        {
            GameObject p = Instantiate(platform);
            p.transform.SetParent(background.transform);
            float y = doodle.GetComponent<RectTransform>().anchoredPosition.y;
            p.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-300f, 300f), Random.Range(0f, y + 600), 0);
            p.transform.localScale = new Vector3(1f, 1f, 1f);
            p.GetComponent<PlatformCollider>().index = i;
            p.GetComponent<PlatformCollider>().generator = this;
            platforms.Add(p);
        }
    }
    void Awake()
    {
        GeneratePlatforms();
        Debug.Log(platforms.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
