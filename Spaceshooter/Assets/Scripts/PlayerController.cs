using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocidade = 5f;
    private Rigidbody2D meuRB;
    [SerializeField] private GameObject meuTiro;
    [SerializeField] private GameObject meuSegundoTiro;
    [SerializeField] private Transform posicaoTiro;
    [SerializeField] private GameObject minhaExplosao;
    private int vida = 3;
    private float velocidadeTiro = 10f;
    private float limiteX = 8.25f;
    private float limiteY = 4.3f;
    private int levelTiro = 1;
    private Vector3 posicaoTiroLeft;
    private Vector3 posicaoTiroRight;

    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();

        Atirando();
    }

    private void Movendo()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        meuRB.velocity = new Vector2(horizontal, vertical).normalized * velocidade;
        float meuX = Mathf.Clamp(transform.position.x, -limiteX, limiteX);
        float meuY = Mathf.Clamp(transform.position.y, -limiteY, limiteY);
        transform.position = new Vector3(meuX, meuY, transform.position.z);
    }

    private void Atirando()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch(levelTiro)
            {
                case 1:
                    CriaTiro(meuTiro, posicaoTiro.position);
                    break;

                case 2: 
                    posicaoTiroLeft = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.15f, transform.position.z);
                    CriaTiro(meuSegundoTiro, posicaoTiroLeft);
                    posicaoTiroRight = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.15f, transform.position.z);
                    CriaTiro(meuSegundoTiro, posicaoTiroRight);                    
                    break;

                case 3:
                    CriaTiro(meuTiro, posicaoTiro.position);
                    posicaoTiroLeft = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.15f, transform.position.z);
                    CriaTiro(meuSegundoTiro, posicaoTiroLeft);
                    posicaoTiroRight = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.15f, transform.position.z);
                    CriaTiro(meuSegundoTiro, posicaoTiroRight);
                    break;
            }
        }
    }

    private void CriaTiro(GameObject tiro, Vector3 posicaoTiro)
    {
        GameObject tiroCriando = Instantiate(tiro, posicaoTiro, Quaternion.identity);
        tiroCriando.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, velocidadeTiro);
    }

    //Criando um metodo de dar dano e recebe a quantidade de dano que vai ser dano
    public void LevaDano(int dano = 1)
    {
        vida -= dano;
        
        if (vida < 0) 
        {
            Instantiate(minhaExplosao, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Power Up"))
        {
            if (levelTiro < 3)
            {
                levelTiro++;
            }
            Destroy(collision.gameObject);
        }
    }
}
