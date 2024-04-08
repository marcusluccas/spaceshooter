using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo01Controller : MonoBehaviour
{
    private Rigidbody2D meuRB;
    private float velocidade = -2f;
    [SerializeField] private GameObject meuTiro;
    private float esperaTiro = 1f;
    [SerializeField] private Transform posicaoTiro;
    [SerializeField] private GameObject minhaExplocao;

    private int vida = 1;

    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(0f, velocidade);
        esperaTiro = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        esperaTiro -= Time.deltaTime;
        if (esperaTiro < 0 && visivel)
        {
            Instantiate(meuTiro, posicaoTiro.position, Quaternion.identity);
            esperaTiro = Random.Range(1f, 1.5f);
        }
    }

    //Criando um metodo de receber dano que recebe a quantidade de dano
    public void RecebeDano(int dano = 1)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Instantiate(minhaExplocao, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
