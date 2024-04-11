using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocidade = 5f;
    private Rigidbody2D meuRB;
    [SerializeField] private GameObject meuTiro;
    [SerializeField] private Transform posicaoTiro;
    [SerializeField] private GameObject minhaExplosao;
    private int vida = 3;
    private float velocidadeTiro = 10f;
    private float limiteX = 8.25f;
    private float limiteY = 4.25f;

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
            GameObject tiro = Instantiate(meuTiro, posicaoTiro.position, Quaternion.identity);
            tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, velocidadeTiro);
        }
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
}
