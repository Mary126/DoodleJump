using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleCamera : MonoBehaviour
{
    public GameObject doodle;
    private DoodleMovement script;
    public Camera gameCamera;
    // Start is called before the first frame update
    void MoveCamera()
    {
        if (!script.isGrounded)
        {
            gameCamera.GetComponent<RectTransform>().anchoredPosition = new Vector3(gameCamera.GetComponent<RectTransform>().anchoredPosition.x, doodle.GetComponent<RectTransform>().anchoredPosition.y, 0);
        } 
    }
    void Start()
    {
        script = doodle.GetComponent<DoodleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }
}
