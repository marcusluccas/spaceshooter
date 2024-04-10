using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inimigo02Controller : InimigoPai
{
    private Rigidbody2D meuRB;
    [SerializeField] private Transform posicaoTiro;
    private float yMax = 2.5f;

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
        Atirando();
        if (transform.position.y < yMax && meuRB.velocity.x == 0)
        {
            if (transform.position.x < 0f)
            {
                Debug.Log("Esquerda!!!");
                meuRB.velocity = new Vector2(velocidade * -1, velocidade);
            }
            else
            {
                Debug.Log("Direita!!!");
                meuRB.velocity = new Vector2(velocidade, velocidade);
            }
        }
    }

    private void Atirando()
    {
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;
        PlayerController player = FindObjectOfType<PlayerController>();

        if (visivel && player)
        {
            esperaTiro -= Time.deltaTime;
            if (esperaTiro < 0f)
            {
                GameObject tiro = Instantiate(meuTiro, posicaoTiro.position, Quaternion.identity);
                Vector3 direcao = player.transform.position - transform.position;
                direcao.Normalize();
                tiro.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeTiro;
                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                tiro.transform.rotation = Quaternion.Euler(0f, 0f, 90f + angulo);
                esperaTiro = Random.Range(2f, 3f);
            }
        }
    }
}
