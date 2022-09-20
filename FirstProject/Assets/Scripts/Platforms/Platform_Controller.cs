using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Controller : MonoBehaviour
{

    private Rigidbody2D m_rb;
    private float m_MoveSpeed = 1f;
    bool m_NotDestroying = true;

    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = new Vector3(-m_MoveSpeed, m_rb.velocity.y, 0);
        m_NotDestroying = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "LimitLeft")
        {
            //Debug.Log("Choco limite IZQUIERDA");
            m_rb.velocity = new Vector3(m_MoveSpeed, m_rb.velocity.y, 0);
        }
        else if (col.gameObject.tag == "LimitRight")
        {
            //Debug.Log("Choco limite derecha");
            m_rb.velocity = new Vector3(-m_MoveSpeed, m_rb.velocity.y, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //print("He chocado contra " + col.gameObject.name);
        print("Me puedo destruir? " + m_NotDestroying);

        if (col.gameObject.tag == "Player" && m_NotDestroying)
        {
            m_NotDestroying = false;
            //print("Me voy a destruir en 5 segundos (plataforma)");
            Platform_Manager.Instance.StartCoroutine("RespawnPlatform", new Vector2(transform.position.x, transform.position.y));
            Destroy(gameObject, 5f);
            
        }
    }

   
}
