using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackground : MonoBehaviour
{
    public GameObject doodle;
    private DoodleMovement script;
    public GameObject background;
    void MoveBg()
    {
        if (script && !script.isGrounded && !script.loose)
        {
            background.GetComponent<RectTransform>().anchoredPosition = new Vector2(background.GetComponent<RectTransform>().anchoredPosition.x, doodle.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }
    void Start()
    {
        if (doodle)
        {
            script = doodle.GetComponent<DoodleMovement>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBg();
    }
}
