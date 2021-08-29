using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabilizePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject doodle;
    public Image platform;

    void Move()
    {
        RectTransform platformPosition = platform.GetComponent<RectTransform>();
        if (!doodle.GetComponent<DoodleMovement>().isGrounded)
        {
            platformPosition.anchoredPosition = new Vector3(platformPosition.anchoredPosition.x, doodle.GetComponent<RectTransform>().anchoredPosition.y, 0);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
