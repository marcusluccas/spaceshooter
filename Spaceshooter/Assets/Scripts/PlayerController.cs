using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocidade = 5f;
    private Rigidbody2D meuRB;
    [SerializeField] private TiroPlayerController meuTiro;
    [SerializeField] private Transform posicaoTiro;
    [SerializeField] private GameObject minhaExplosao;
    private int vida = 3;
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        meuRB.velocity = new Vector2(horizontal, vertical).normalized * velocidade;

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(meuTiro, posicaoTiro.position, Quaternion.identity);
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
