using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigoController : MonoBehaviour
{
    [SerializeField] private GameObject[] inimigos;

    private int pontos = 0;
    private int level = 1;

    private float esperaInimigo = 0f;
    private float timerInimigo = 2f;
    private int nextLevel = 100;

    private int qtdInimigo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GeraInimigos();
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

    private void GeraInimigos()
    {
        if (esperaInimigo > 0f && qtdInimigo <= 0)
        {
            esperaInimigo -= Time.deltaTime;
        }

        if (esperaInimigo <= 0f && qtdInimigo <= 0)
        {
            int quantidade = level * 4;
            while (qtdInimigo < quantidade)
            {
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

                Instantiate(inimigoEscolhido, new Vector3(Random.Range(-8f, 8f), Random.Range(6f, 12f), 0), Quaternion.identity);
                qtdInimigo++;
                esperaInimigo = timerInimigo;
            }
        }
    }
}
