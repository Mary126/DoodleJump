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
    private float t = 0.0f;
    private AudioSource audioData;
    public GameObject Background;
    //public Camera doodleCamera;
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
        }
        if (moving)
        {
            // Record the time spent moving up or down.
            // When this is 1sec then display info
            t += Time.deltaTime;
            if (t > 1.0f)
            {
                //moving = false;
                t = 0.0f;
            }
        }
    }

    void CheckPosition()
    {
        if (doodle.GetComponent<RectTransform>().anchoredPosition.x > widthBg / 2 + doodle.GetComponent<RectTransform>().sizeDelta.x / 2 + 0.5)
        {
            doodle.GetComponent<RectTransform>().anchoredPosition = new Vector3(-widthBg / 2 - doodle.GetComponent<RectTransform>().sizeDelta.x / 2, doodle.GetComponent<RectTransform>().anchoredPosition.y, 0);
        }
        if (doodle.GetComponent<RectTransform>().anchoredPosition.x < -widthBg / 2 - doodle.GetComponent<RectTransform>().sizeDelta.x / 2)
        {
            doodle.GetComponent<RectTransform>().anchoredPosition = new Vector3(widthBg / 2 + doodle.GetComponent<RectTransform>().sizeDelta.x / 2, doodle.GetComponent<RectTransform>().anchoredPosition.y, 0);
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.collider == true)
        {
            isGrounded = true;
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) {
            Move();
        }
        Jump();
        CheckPosition();
    }
}
