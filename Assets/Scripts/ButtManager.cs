using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtManager : MonoBehaviour
{
    public UnityEngine.UI.Button buttn;
    void TaskOnClick()
    {
        Debug.Log(buttn.name);
        if (buttn.name == "Restart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (buttn.name == "Exit")
        {
            Application.Quit();
        }
    }
    private void Awake()
    {
        buttn.onClick.AddListener(TaskOnClick);
    }
}
