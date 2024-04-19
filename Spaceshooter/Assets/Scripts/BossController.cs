using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    private float delay;
    private float esperaTiro2;
    private string[] estados = { "estado1", "estado2", "estado3" };
    private float timerEstado;
    [SerializeField] private Image barraVida;
    private int vidaMax = 100;
    // Start is called before the first frame update
    void Start()
    {
        timerEstado = 10f;
        velocidade = 2f;
        velocidadeTiro = 4f;
        delay = 1;
        esperaTiro = delay;
        esperaTiro2 = delay;
        vida = vidaMax;
        pontos = 100;
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.fillAmount = ((float) vida / (float) vidaMax);
        barraVida.color = new Color32(150, (byte) (barraVida.fillAmount * 255), 50, 255);

        AlteraEstado();

        switch (estado)
        {
            case "estado1":
                Estado1();
                break;

            case "estado2":
                Estado2();
                break;

            case "estado3":
                Estado3();
                break;
        }
    }


    private void CriaTiro1()
    {
        GameObject tiro;
        tiro = Instantiate(tiro1, PosicaoTiro1.position, transform.rotation);
        tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -velocidadeTiro);

        tiro = Instantiate(tiro1, PosicaoTiro2.position, transform.rotation);
        tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -velocidadeTiro);
    }

    private void CriaTiro2()
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

        esperaTiro -= Time.deltaTime;

        if (esperaTiro <= 0f)
        {
            CriaTiro1();
            esperaTiro = delay;
        }
    }

    private void Estado2()
    {
        meuRB.velocity = Vector2.zero;

        esperaTiro2 -= Time.deltaTime;

        if (esperaTiro2 <= 0f)
        {
            CriaTiro2();
            esperaTiro2 = delay / 2;
        }
    }

    private void Estado3()
    {
        meuRB.velocity = Vector2.zero;

        esperaTiro -= Time.deltaTime;

        if (esperaTiro <= 0f)
        {
            CriaTiro1();
            esperaTiro = delay;
        }
        esperaTiro2 -= Time.deltaTime;

        if (esperaTiro2 <= 0f)
        {
            CriaTiro2();
            esperaTiro2 = delay * 1.5f;
        }
    }

    private void AlteraEstado()
    {
        timerEstado -= Time.deltaTime;

        if (timerEstado <= 0f)
        {
            int IndiceEstado = Random.Range(0, estados.Length);

            estado = estados[IndiceEstado];
            
            timerEstado = 10f;
        }
    }
}
