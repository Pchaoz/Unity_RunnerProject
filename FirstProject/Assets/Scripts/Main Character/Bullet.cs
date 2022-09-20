using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 15f;

    [SerializeField]
    private int m_dmg = 35;

    public int Dmg
    {
        get { return m_dmg; }
    }
    private Rigidbody2D m_rb;

    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rb.velocity = transform.right * m_speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("La bala ha reventado contra: " + col.name);
        Destroy(gameObject);
    }
}
