using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int m_hp = 100;

    [SerializeField]
    private float m_speed = 10f;

    private int m_score = 10;

    private float m_ChangePlatform = 4f;

    //Rigidbody del enemic
    private Rigidbody2D m_rb;

    public delegate void OnScoreChange(int m_score);
    public static event OnScoreChange onScoreChange;

    private void Awake()
    {
        Destroy(gameObject, 12f);
        m_rb = GetComponent<Rigidbody2D>();
        if (IsFacingRight())
        {
            m_rb.velocity = new Vector3(m_speed, m_rb.velocity.y, 0);
        }
        else
        {
            m_rb.velocity = new Vector3(-m_speed, m_rb.velocity.y, 0);
        }

    }

    private void Start()
    {
        StartCoroutine(ChangePlatform());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "LimitRight" || col.tag == "LimitLeft")
        {
            m_rb.velocity *= -1;
        }
        if (col.tag == "Bullet")
        {
            TakeDamage(col.GetComponent<Bullet>().Dmg);
        }
    }

    public void TakeDamage(int dmg)
    {
        m_hp -= dmg;
        print(gameObject + " me quedan " + m_hp);
        if (m_hp <= 0)
        {
            onScoreChange(m_score);
            Destroy(gameObject);
        }
    }
    bool IsFacingRight()
    {
        return m_rb.transform.localScale.x > Mathf.Epsilon;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangePlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_ChangePlatform);

            if (gameObject.transform.position.y > 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -3.346796f, gameObject.transform.position.z);
            }else if (gameObject.transform.position.y < 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3.483205f, gameObject.transform.position.z);
            }
        }

    }
}
