using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public GameObject doodle;
    public float speed = 9.0f;
    private Vector2 directionVector;
    public GameObject progectile;
    public AudioSource shootingSound;
    // Start is called before the first frame update
    public void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Debug.Log(mousePosition);
        directionVector = (mousePosition - transform.position).normalized;
        shootingSound.Play(0);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionVector * speed * Time.deltaTime);
        if (GetComponent<RectTransform>().anchoredPosition.x < -330 || GetComponent<RectTransform>().anchoredPosition.x > 330)
        {
            Destroy(this);
        }
    }
}
