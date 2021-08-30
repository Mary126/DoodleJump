using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    public GameObject doodle;
    public GameObject enemy;
    public AudioSource crash;
    public AudioSource monsterSound;
    public AudioSource mosterHit;

    private bool down = true;

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.collider.gameObject.name);
        if (coll.collider.gameObject.name == "Player")
        {
            doodle.GetComponent<Collider2D>().enabled = false;
            crash.Play(0);
        }
        if (coll.collider.gameObject.name == "Projectile")
        {
            mosterHit.Play(0);
            Destroy(enemy);
            Destroy(coll.collider.gameObject);
        }
    }

    void Awake()
    {
        monsterSound.Play(0);
        monsterSound.loop = true;
        InvokeRepeating("Move", 0.5f, 0.3f);
    }

    void Move()
    {
        if (down)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y - 20);
            down = false;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y + 20);
            down = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (enemy && doodle && enemy.GetComponent<RectTransform>().anchoredPosition.y < doodle.GetComponent<RectTransform>().anchoredPosition.y - 540)
        {
            Destroy(enemy);
        }
    }
}
