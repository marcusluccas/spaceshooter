using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class GeradorInimigoController : MonoBehaviour
{
    [SerializeField] private GameObject[] inimigos;

    private int pontos = 0;
    private int level = 4;

    private float esperaInimigo = 0f;
    private float timerInimigo = 2f;
    private int nextLevel = 1;

    private int qtdInimigo = 0;

    [SerializeField] private GameObject bossAnimition;
    private GameObject bossAnimitionCriado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (level >= 5)
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
        this.pontos += pontos;

        if (this.pontos > nextLevel * level)
        {
            level++;
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
        if (esperaInimigo > 0f && qtdInimigo <= 0 && bossAnimitionCriado == null)
        {
            esperaInimigo -= Time.deltaTime;
        }

        if (esperaInimigo <= 0f && qtdInimigo <= 0 && bossAnimitionCriado == null)
        {
            bossAnimitionCriado = Instantiate(bossAnimition, Vector3.zero, transform.rotation);
            qtdInimigo++;
            esperaInimigo = timerInimigo;
        }
    }
}
