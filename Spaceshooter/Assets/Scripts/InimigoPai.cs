using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPai : MonoBehaviour
{
    protected float velocidade = -2f;
    protected int vida = 1;
    [SerializeField] protected GameObject minhaExplosao;
    [SerializeField] protected GameObject meuTiro;
    protected float esperaTiro;
    protected float velocidadeTiro = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Criando o metodo de levar dano e morrer
    public void RecebeDano(int dano = 1)
    {
        vida -= dano;

        if (vida <= 0)
        {
            Instantiate(minhaExplosao, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // Metodo de destruir o inimigo se ele colidir com o destruidor de tiros
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destruidor de Tiro"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().LevaDano();
            Instantiate(minhaExplosao, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
