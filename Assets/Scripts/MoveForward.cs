using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public GameObject doodle;
    public float speed = 9.0f;
    private Vector2 directionVector;
    public GameObject projectile;
    public AudioSource shootingSound;
    // Start is called before the first frame update
    public void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        directionVector = (mousePosition - transform.position).normalized;
        shootingSound.Play(0);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionVector * speed * Time.deltaTime);
        if (projectile && doodle && (projectile.GetComponent<RectTransform>().anchoredPosition.x < -330 || projectile.GetComponent<RectTransform>().anchoredPosition.x > 330))
        {
            Destroy(projectile);
        }
        if (projectile && doodle && (projectile.GetComponent<RectTransform>().anchoredPosition.y > doodle.GetComponent<RectTransform>().anchoredPosition.y + 1080 || projectile.GetComponent<RectTransform>().anchoredPosition.y < doodle.GetComponent<RectTransform>().anchoredPosition.y - 1080))
        {
            Destroy(projectile);
        }
    }
}
