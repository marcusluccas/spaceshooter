using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : InimigoPai
{
    private string estado = "estado1";
    private Rigidbody2D meuRB;
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
        esperaTiro = Random.Range(0.2f, 0.4f);
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(velocidade, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case "estado1":
                Estado1();
                break;
        }
    }

    private void Estado1()
    {
        CriaTiro1();

        if (transform.position.x > limiteX)
        {
            meuRB.velocity = new Vector2(-velocidade, 0f);
        }
        if (transform.position.x < -limiteX)
        {
            meuRB.velocity = new Vector2(velocidade, 0f);
        }
    }

    private void CriaTiro1()
    {
        esperaTiro -= Time.deltaTime;

        if (esperaTiro < 0f)
        {
            GameObject tiro;
            tiro = Instantiate(tiro1, PosicaoTiro1.position, transform.rotation);
            tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -velocidadeTiro);

            tiro = Instantiate(tiro1, PosicaoTiro2.position, transform.rotation);
            tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -velocidadeTiro);

            esperaTiro = Random.Range(0.4f, 0.8f);
        }
    }
}
