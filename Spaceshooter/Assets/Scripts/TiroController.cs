using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    private Rigidbody2D meuRB;
    [SerializeField] private float velocidade = 10f;
    [SerializeField] private GameObject minhaExplosao;

    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        // meuRB.velocity = Vector2.up * velocidade;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigos"))
        {
            collision.GetComponent<InimigoPai>().RecebeDano();
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().LevaDano();
        }

        Instantiate(minhaExplosao, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}