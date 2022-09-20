using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mc_Controller : MonoBehaviour
{
    //Stats MC
    [SerializeField]
	private float m_MoveSpeed = 3f;
    [SerializeField]
    private int m_hp = 3;

    static int m_sum = 0;
    
    [SerializeField]
    private string m_score;

    public TextMeshProUGUI result;
    public TextMeshProUGUI hp_text;

    //Bools per controlar la gravetat del personatje i direcció
    private bool m_onGround = false;
    private bool m_RegularGrav = true;
    private bool m_LimitRight = false;
    private bool m_LimitLeft = false;
    private bool m_FacingRight = true;

    //Rigidbody del personatje
    private Rigidbody2D m_rb;

    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_sum = 0;

        Enemy.onScoreChange += ScoreChange;
    }

    void Update()
    {
        if (m_hp <= 0)
        {
            Destroy(gameObject, 1f);
            SceneManager.LoadScene("GameOver");
        }

        if(Input.GetKey(KeyCode.A) && !m_LimitLeft)
		{
			m_rb.velocity = new Vector3(-m_MoveSpeed,m_rb.velocity.y,0);	
            
            if (m_FacingRight)
            {
                m_FacingRight = false;
                m_rb.transform.Rotate(0f, 180f, 0f);
            }
		}
        if(Input.GetKey(KeyCode.D) && !m_LimitRight)
		{
			m_rb.velocity = new Vector3(m_MoveSpeed,m_rb.velocity.y,0);
            if (!m_FacingRight)
            {
                m_FacingRight = true;
                m_rb.transform.Rotate(0f, -180f, 0f);
            }
        }
        if (m_onGround && Input.GetKey(KeyCode.Space) ) {
            if (m_RegularGrav) {
                m_rb.gravityScale = -1f;
                m_RegularGrav = false;
                m_rb.transform.Rotate(180f, 0f, 0f);
            }
            else {
                m_rb.gravityScale = 1f;
                m_RegularGrav = true;
                m_rb.transform.Rotate(-180f, 0f, 0f);
            }
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("He tocado: " + col.gameObject.name);
        
        if (col.gameObject.tag == "Platforms") {
            m_onGround = true;
        }
        if (col.gameObject.tag == "Enemy")
        {
            m_hp -= 1;
            hp_text.text = "Lifes: " + m_hp.ToString();
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        //Debug.Log("He dejado de tocar: " + col.gameObject.name);
        if  (col.gameObject.tag == "Platforms")  {
            m_onGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "LimitRight")
        {
            m_LimitRight = true;
        }
        if (col.gameObject.tag == "LimitLeft")
        {
            m_LimitLeft = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "LimitRight")
        {
            m_LimitRight = false;
        }
        if (col.gameObject.tag == "LimitLeft")
        {
            m_LimitLeft = false;
        }
    }
    void ScoreChange(int points)
    {
        m_sum += points;
        m_score = "Score: " + m_sum;
        result.text = m_score;
        GameManager.Instance.Score = m_sum;
        Debug.Log("a"); 
    }
    private void OnDestroy()
    {
        Enemy.onScoreChange -= ScoreChange;
    }
}
