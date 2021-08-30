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
    public GameObject stars;

    private bool down = true;

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.collider.gameObject.name);
        if (coll.collider.gameObject.name == "Player")
        {
            //Destroy Player colliders and create a stars image on the head of the doodle
            coll.collider.gameObject.GetComponent<Collider2D>().enabled = false;
            coll.collider.gameObject.GetComponent<DoodleMovement>().colliderLegs.GetComponent<Collider2D>().enabled = false;
            GameObject gameObj = Instantiate(stars);
            gameObj.transform.SetParent(doodle.transform);
            gameObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20f, 86.0f);
            gameObj.transform.localScale = new Vector3(1f, 1f, 1f);
            crash.Play(0);
        }
        if (coll.collider.gameObject.name == "Projectile")
        {
            //if Projectile hits the Enemy
            mosterHit.Play(0);
            Destroy(enemy);
            Destroy(coll.collider.gameObject);
        }
    }

    void Awake()
    {
        monsterSound.Play(0);
        monsterSound.loop = true;
        InvokeRepeating("Move", 0.5f, 0.3f); //repeate Move() every 0.5 seconds
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
