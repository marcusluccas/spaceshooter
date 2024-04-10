using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo01Controller : InimigoPai
{
    private Rigidbody2D meuRB;
    [SerializeField] private Transform posicaoTiro;

    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(0f, velocidade);
        esperaTiro = Random.Range(0.5f, 1f);
        velocidadeTiro = -6f;
    }

    // Update is called once per frame
    void Update()
    {
        Atirando();
    }

    private void Atirando()
    {
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visivel)
        {
            esperaTiro -= Time.deltaTime;
            if (esperaTiro < 0f)
            {
                GameObject tiro = Instantiate(meuTiro, posicaoTiro.position, Quaternion.identity);
                tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, velocidadeTiro);
                esperaTiro = Random.Range(1.5f, 2f);
            }
        }
    }
}
