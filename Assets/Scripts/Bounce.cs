using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector2 lastVelocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        //rb.AddForce(transform.right * -1 * speed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        this.lastVelocity = this.rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 refrectVec = Vector2.Reflect(this.lastVelocity, coll.contacts[0].normal);
        this.rb.velocity = refrectVec;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        this.gameObject.SetActive(false);
        GameObject gameManager = GameObject.Find("GameManager");
        if (other.gameObject.tag=="check1")//チェック１に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(0);
        }

        if (other.gameObject.tag == "check2")//チェック2に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(1);
        }

        if (other.gameObject.tag == "check3")//チェック3に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(2);
        }

        if (other.gameObject.tag == "check4")//チェック4に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(3);
        }

        if (other.gameObject.tag == "check5")//チェック5に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(4);
        }

        if (other.gameObject.tag == "check6")//チェック6に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(5);
        }

        if (other.gameObject.tag == "check7")//チェック7に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(6);
        }

        if (other.gameObject.tag == "check8")//チェック8に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(7);
        }

        if (other.gameObject.tag == "check9")//チェック9に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(8);
        }

        if (other.gameObject.tag == "check10")//チェック10に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(9);
        }

        if (other.gameObject.tag == "check11")//チェック１1に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(10);
        }

        if (other.gameObject.tag == "check12")//チェック１2に侵入
        {
            gameManager.GetComponent<GameManager>().CheckClear(11);
        }
    }
}
