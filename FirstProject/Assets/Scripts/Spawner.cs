using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EnemyPrefab;
    [SerializeField]
    private float m_SpawnRate = 6f;
    private float m_SpawnRateDelta = 3f;

    [SerializeField]
    private Transform[] m_SpawnPoints;

    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
        StartCoroutine(IncreaseDifficulty());

    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            
            GameObject spawned = Instantiate(m_EnemyPrefab);
            int spawner = Random.Range(0, m_SpawnPoints.Length);
            if (m_SpawnPoints[spawner].tag == "ReverseGrav")
            {
                spawned.transform.position = m_SpawnPoints[spawner].position;
                spawned.GetComponent<Rigidbody2D>().gravityScale = -1;
            }
            else
            {
                spawned.transform.position = m_SpawnPoints[spawner].position;
                spawned.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            yield return new WaitForSeconds(m_SpawnRate);
        }

    }

    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_SpawnRateDelta);
            m_SpawnRate -= 0.5f;

            if (m_SpawnRate <= 0.5f)
            {
                m_SpawnRate = 0.5f;
            }

        }

    }

}
