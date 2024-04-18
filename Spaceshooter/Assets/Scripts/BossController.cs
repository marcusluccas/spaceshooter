using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : InimigoPai
{
    private string estado = "estado1";
    private Rigidbody2D meuRB;
    private bool direita = false;
    private float limiteX = 6f;
    [Header("Coisas dos tiros")]
    [SerializeField] private Transform PosicaoTiro1;
    [SerializeField] private Transform PosicaoTiro2;
    [SerializeField] private Transform PosicaoTiro3;
    [SerializeField] private GameObject tiro1;
    [SerializeField] private GameObject tiro2;
    // Start is called before the first frame update
    void Start()
    {
        velocidade = 2f;
        velocidadeTiro = 4f;
        esperaTiro = Random.Range(2f, 4f);
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case "estado1":
                Estado1();
                break;

            case "estado2":
                Estado2();
                break;
        }
    }

    private void Estado1()
    {
        if (direita)
        {
            meuRB.velocity = new Vector2(velocidade, 0f);
        }
        else
        {
            meuRB.velocity = new Vector2(-velocidade, 0f);
        }

        if (transform.position.x > limiteX)
        {
            direita = false;
        }
        if (transform.position.x < -limiteX)
        {
            direita = true;
        }

        CriaTiro1();
    }

    private void Estado2()
    {
        meuRB.velocity = Vector2.zero;

        CriaTiro2();
    }

    private void CriaTiro1()
    {
        esperaTiro -= Time.deltaTime;

        if (esperaTiro <= 0f)
        {
            GameObject tiro;
            tiro = Instantiate(tiro1, PosicaoTiro1.position, transform.rotation);
            tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -velocidadeTiro);

            tiro = Instantiate(tiro1, PosicaoTiro2.position, transform.rotation);
            tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -velocidadeTiro);

            esperaTiro = Random.Range(0.6f, 0.8f);
        }
    }

    private void CriaTiro2()
    {
        esperaTiro -= Time.deltaTime;

        if (esperaTiro <= 0)
        {
            PlayerController player = FindObjectOfType<PlayerController>();

            if (player)
            {
                GameObject tiro = Instantiate(tiro2, PosicaoTiro3.position, transform.rotation);
                Vector3 direcao = player.transform.position - transform.position;
                direcao.Normalize();
                tiro.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeTiro;
                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                tiro.transform.rotation = Quaternion.Euler(0f, 0f, 90f + angulo);

                esperaTiro = Random.Range(0.2f, 0.5f);
            }
        }
    }
}
