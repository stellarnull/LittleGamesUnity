using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float high;
    public Text countText;
    public Text winText;
    public Text lifeText;
    public Text loseText;

    private Rigidbody rb;
    private int count;
    private int lifeCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lifeCount = 3;
        SetCountText();
        SetLifeText();
        winText.text = "";
        loseText.text = "";       
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            Reset();
            lifeCount--;
            SetLifeText();
        }

        if (lifeCount == 0)
        {
            loseText.text = "Game Over!";
            //wait(5000);
            Start();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 jump = new Vector3(0.0f, 1.0f, 0.0f);

        rb.AddForce(movement * speed);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(jump * high);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void Reset()
    {
        transform.position = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }

    void SetLifeText()
    {
        lifeText.text = "Life: " + lifeCount.ToString();
    }
}