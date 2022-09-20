using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Manager : MonoBehaviour
{
    public static Platform_Manager Instance = null;

    [SerializeField]
    private GameObject m_platformPrefab;

    //Basicamente es un singelton, solo hay un controlodor para las plataformas
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator RespawnPlatform(Vector2 spawnCoords)
    {
        Debug.Log("SOY EL RESPAWN PLATFORM");
        yield return new WaitForSeconds(10f);
        Instantiate(m_platformPrefab, spawnCoords, m_platformPrefab.transform.rotation);
    }
}
