using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocidade = 5f;
    private Rigidbody2D meuRB;
    [Header("Coisas dos tiros")]
    [SerializeField] private GameObject meuTiro;
    [SerializeField] private GameObject meuSegundoTiro;
    [SerializeField] private Transform posicaoTiro;
    [Header("Coisas do player")]
    [SerializeField] private GameObject minhaExplosao;
    private int vida = 3;
    private float velocidadeTiro = 10f;
    private float limiteX = 8.25f;
    private float limiteY = 4.3f;
    private int levelTiro = 1;
    private Vector3 posicaoTiroLeft;
    private Vector3 posicaoTiroRight;
    [SerializeField] private GameObject meuEscudo;
    private GameObject escudo;
    private float timerEscudo = 0f;
    private int qtdEscudos = 3;
    [SerializeField] private Text textoVida;
    [SerializeField] private Text textoEscudo;
    [SerializeField] private AudioClip soundTiro;
    private Camera camera;
    [SerializeField] private AudioClip soundMorte;
    [SerializeField] private AudioClip soundStartShield;
    [SerializeField] private AudioClip soundQuitShield;

    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        textoVida.text = vida.ToString();
        textoEscudo.text = qtdEscudos.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();

        Atirando();

        AtivaEscudo();
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
        camera = FindObjectOfType<Camera>();

        if (Input.GetButtonDown("Fire1"))
        {
            switch (levelTiro)
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

            AudioSource.PlayClipAtPoint(soundTiro, camera.transform.position);
        }
    }

    private void CriaTiro(GameObject tiro, Vector3 posicaoTiro)
    {
        GameObject tiroCriando = Instantiate(tiro, posicaoTiro, transform.rotation);
        tiroCriando.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, velocidadeTiro);
    }

    private void AtivaEscudo()
    {
        if (Input.GetButtonDown("Shield") && escudo == null && qtdEscudos > 0)
        {
            escudo = Instantiate(meuEscudo, transform.position, transform.rotation);
            camera = FindObjectOfType<Camera>();
            AudioSource.PlayClipAtPoint(soundStartShield, camera.transform.position);
            qtdEscudos--;
            textoEscudo.text = qtdEscudos.ToString();
        }
        if (escudo != null)
        {
            escudo.transform.position = transform.position;

            timerEscudo += Time.deltaTime;

            if (timerEscudo > 5.2f)
            {
                Destroy(escudo);
                camera = FindObjectOfType<Camera>();
                AudioSource.PlayClipAtPoint(soundQuitShield, camera.transform.position);
                timerEscudo = 0f;
            }
        }
    }

    //Criando um metodo de dar dano e recebe a quantidade de dano que vai ser dano
    public void LevaDano(int dano = 1)
    {
        vida -= dano;

        textoVida.text = vida.ToString();

        if (vida <= 0) 
        {
            Instantiate(minhaExplosao, transform.position, transform.rotation);
            camera = FindObjectOfType<Camera>();
            AudioSource.PlayClipAtPoint(soundMorte, camera.transform.position);
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.VoltaMenu();
            }

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
