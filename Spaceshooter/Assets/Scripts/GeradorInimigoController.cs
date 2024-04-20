using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class GeradorInimigoController : MonoBehaviour
{
    [Header("Inimigo que vão ser gerados")]
    [SerializeField] private GameObject[] inimigos;
    [SerializeField] private GameObject bossAnimition;
    [SerializeField] private Text textoPontos;

    private int pontos = 0;
    private int level = 1;

    private float esperaInimigo = 0f;
    private float timerInimigo = 2f;
    private int nextLevel = 100;

    private int qtdInimigo = 0;

    private bool bossExiste = false;
    // Start is called before the first frame update
    void Start()
    {
        textoPontos.text = "PONTOS: " + pontos.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textoPontos.text = "PONTOS: " + this.pontos.ToString();

        if (level >= 6)
        {
            GeraBoss();
        }
        else
        {
            GeraInimigos();
        }
    }

    public void GanhaPontos(int pontos)
    {
        this.pontos += pontos * level;

        if (this.pontos > nextLevel)
        {
            level++;
            nextLevel *= 2;
        }
    }

    public void DiminuiInimigo()
    {
        qtdInimigo--;
    }
    private bool ChecaPosicaoLivre(Vector3 posicao, Vector2 size)
    {
        Collider2D livre = Physics2D.OverlapBox(posicao, size, 0f);
        if (livre == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GeraInimigos()
    {
        if (esperaInimigo > 0f && qtdInimigo <= 0)
        {
            esperaInimigo -= Time.deltaTime;
        }

        if (esperaInimigo <= 0f && qtdInimigo <= 0)
        {
            int quantidade = level * 4;
            int tentativas = 0;
            while (qtdInimigo < quantidade)
            {
                tentativas++;

                if (tentativas > 200)
                {
                    break;
                }

                GameObject inimigoEscolhido;

                float chance = Random.Range(0f, level);
                if (chance > 2f)
                {
                    inimigoEscolhido = inimigos[1];
                }
                else
                {
                    inimigoEscolhido = inimigos[0];
                }

                Vector3 posicao = new Vector3(Random.Range(-8f, 8f), Random.Range(6f, 12f), 0f);

                if (!ChecaPosicaoLivre(posicao, inimigoEscolhido.transform.localScale)) continue;

                Instantiate(inimigoEscolhido, posicao, transform.rotation);
                qtdInimigo++;
                esperaInimigo = timerInimigo;
            }
        }
    }

    private void GeraBoss()
    {
        if (esperaInimigo > 0f && qtdInimigo <= 0 && !bossExiste)
        {
            esperaInimigo -= Time.deltaTime;
        }

        if (esperaInimigo <= 0f && qtdInimigo <= 0 && !bossExiste)
        {
            GameObject bossCriando = Instantiate(bossAnimition, new Vector3(0f, -8f, 0f), Quaternion.Euler(Vector3.zero));
            bossExiste = true;
            qtdInimigo++;
            esperaInimigo = timerInimigo;
        }
    }
}
