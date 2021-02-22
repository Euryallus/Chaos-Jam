using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float       m_speed = 1;
    [SerializeField]
    private int         m_damage = 10;

    private Rigidbody2D m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector2 direction)
    {
        gameObject.SetActive(true);
        m_rigidbody.velocity = direction * m_speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthManager>().takeDamage(m_damage);
        }

        gameObject.SetActive(false);
        transform.position = new Vector3(0, 0, 0);
    }
}
