using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoodleMovement : MonoBehaviour
{
    public GameObject doodle;
    public float speed = 4.0f;
    public float jumpForce = 14.0f;
    public float widthBg = 600f;
    public bool isGrounded = false;
    public Canvas canvas;
    private bool moving = true;
    public float t = 0.0f;
    public AudioSource jumpSound;
    public bool loose = false;
    public Text score;
    private bool spaceCheck = false;
    public GameObject projectile;
    public GameObject board;
    public AudioSource shootingSound;
    public GameObject colliderLegs;
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        float moveY = doodle.GetComponent<Rigidbody2D>().velocity.y;
        if (moving)
        {
            moveY -= 0.0981f;
        }
        doodle.GetComponent<Rigidbody2D>().velocity = new Vector2(moveBy, moveY);
    }
    void Jump()
    {
        if (isGrounded)
        {
            doodle.GetComponent<Rigidbody2D>().velocity = new Vector2(doodle.GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            isGrounded = false;
            moving = false;
            t = 0.0f;
            score.text = "GameScore: " + ((int)doodle.GetComponent<RectTransform>().anchoredPosition.y).ToString();
        }
        if (moving)
        {
            // Record the time spent moving up or down.
            // When this is 1sec then display info
            t += Time.deltaTime;
            if (t > 1.0f)
            {
                t = 0.0f;
            }
        }
    }

    void CheckPosition()
    {
        float x = doodle.GetComponent<RectTransform>().anchoredPosition.x;
        float y = doodle.GetComponent<RectTransform>().anchoredPosition.y;
        if (x > widthBg / 2 + doodle.GetComponent<RectTransform>().sizeDelta.x + 0.5)
        {
            doodle.GetComponent<RectTransform>().anchoredPosition = new Vector3(-widthBg / 2 - doodle.GetComponent<RectTransform>().sizeDelta.x / 2, y, 0);
        }
        if (x < -widthBg / 2 - doodle.GetComponent<RectTransform>().sizeDelta.x / 2)
        {
            doodle.GetComponent<RectTransform>().anchoredPosition = new Vector3(widthBg / 2 + doodle.GetComponent<RectTransform>().sizeDelta.x / 2, y, 0);
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.collider && doodle.GetComponent<Rigidbody2D>().velocity.y < 0.01f)
        {
            isGrounded = true;
            jumpSound.Play(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            doodle.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Move();
        }
        if (Input.GetKey(KeyCode.A))
        {
            doodle.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            Move();
        }
        if (Input.GetKey(KeyCode.Mouse0) && !spaceCheck)
        {
            GameObject gameObj = Instantiate(projectile);
            gameObj.transform.SetParent(board.transform);
            gameObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(doodle.GetComponent<RectTransform>().anchoredPosition.x, doodle.GetComponent<RectTransform>().anchoredPosition.y);
            gameObj.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObj.GetComponent<MoveForward>().projectile = gameObj;
            gameObj.GetComponent<MoveForward>().shootingSound = shootingSound;
            gameObj.name = "Projectile";
            spaceCheck = true;
            gameObj.GetComponent<MoveForward>().Shoot();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            spaceCheck = false;
        }
        Jump();
        CheckPosition();
    }
}
