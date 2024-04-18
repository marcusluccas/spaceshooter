using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class GeradorInimigoController : MonoBehaviour
{
    [Header("Inimigo que v�o ser gerados")]
    [SerializeField] private GameObject[] inimigos;
    [SerializeField] private GameObject bossAnimition;

    private int pontos = 0;
    private int level = 4;

    private float esperaInimigo = 0f;
    private float timerInimigo = 2f;
    private int nextLevel = 1;

    private int qtdInimigo = 0;

    private bool bossExiste = false;
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
        if (esperaInimigo > 0f && qtdInimigo <= 0 && !bossExiste)
        {
            esperaInimigo -= Time.deltaTime;
        }

        if (esperaInimigo <= 0f && qtdInimigo <= 0 && !bossExiste)
        {
            GameObject bossCriando = Instantiate(bossAnimition, new Vector3(0f, -8f, 0f), Quaternion.Euler(Vector3.zero));
            Destroy(bossCriando, 6.7f);
            bossExiste = true;
            qtdInimigo++;
            esperaInimigo = timerInimigo;
        }
    }
}
