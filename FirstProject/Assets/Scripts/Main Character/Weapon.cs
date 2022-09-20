using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Transform m_firePoint;
    private Rigidbody2D m_rb;

    [SerializeField]
    private GameObject m_bulletPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_firePoint = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Bang");
            Shoot();
        }        
    }
    private void Shoot()
    {
        Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
    }
}
