using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPai : MonoBehaviour
{
    protected float velocidade;
    protected int vida = 1;
    [Header("Objetos dos Inimigos")]
    [SerializeField] protected GameObject minhaExplosao;
    [SerializeField] protected GameObject meuTiro;
    protected float esperaTiro;
    protected float velocidadeTiro = 3f;
    protected int pontos = 10;
    [SerializeField] protected GameObject powerUp;
    protected float chance;
    [SerializeField] private AudioClip soundExplosao;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Criando o metodo de levar dano e morrer
    public void RecebeDano(int dano = 1)
    {
        if (transform.position.y < 5f)
        {
            vida -= dano;

            if (vida <= 0)
            {
                if (powerUp != null)
                {
                    DropaItem();
                }
                Instantiate(minhaExplosao, transform.position, transform.rotation);
                camera = FindObjectOfType<Camera>();
                AudioSource.PlayClipAtPoint(soundExplosao, camera.transform.position);
                GeradorInimigoController gerador = FindObjectOfType<GeradorInimigoController>();
                if (gerador != null)
                {
                    gerador.GanhaPontos(pontos);
                }
                Destroy(gameObject);
            }
        }
    }

    // Metodo de destruir o inimigo se ele colidir com o destruidor de tiros
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destruidor de Tiro"))
        {
            if (GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                Instantiate(minhaExplosao, transform.position, transform.rotation);
                camera = FindObjectOfType<Camera>();
                AudioSource.PlayClipAtPoint(soundExplosao, camera.transform.position);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().LevaDano();
            Instantiate(minhaExplosao, transform.position, transform.rotation);
            camera = FindObjectOfType<Camera>();
            AudioSource.PlayClipAtPoint(soundExplosao, camera.transform.position);
            Destroy(gameObject);
        }
    }

    public void DropaItem()
    {
        float chance = Random.Range(0f, 1f);
        if (chance > this.chance)
        {
            GameObject powerUpCriado = Instantiate(powerUp, transform.position, transform.rotation);
            Vector2 direcao = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            powerUpCriado.GetComponent<Rigidbody2D>().velocity = direcao;
            Destroy(powerUpCriado, 5f);
        }
    }

    private void OnDestroy()
    {
        GeradorInimigoController gerador = FindObjectOfType<GeradorInimigoController>();
        if (gerador)
        {
            gerador.DiminuiInimigo();
        }
    }
}
