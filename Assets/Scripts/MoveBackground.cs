using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackground : MonoBehaviour
{
    public GameObject doodle;
    private DoodleMovement script;
    public Image background;
    // Start is called before the first frame update
    void MoveBg()
    {
        if (!script.isGrounded && doodle)
        {
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(background.GetComponent<RectTransform>().anchoredPosition.x, doodle.GetComponent<RectTransform>().anchoredPosition.y, 0);
        }
    }
    void Start()
    {
        script = doodle.GetComponent<DoodleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBg();
    }
}
