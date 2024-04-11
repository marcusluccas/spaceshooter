using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigoController : MonoBehaviour
{
    [SerializeField] private GameObject[] inimigos;

    private int pontos = 0;
    private int level = 1;

    private float esperaTiro = 0f;
    private float timerTiro = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GeraInimigos();
    }

    private void GeraInimigos()
    {
        esperaTiro -= Time.deltaTime;
        if (esperaTiro <= 0f)
        {
            Instantiate(inimigos[0], new Vector3(Random.Range(-8f, 8f), Random.Range(6f, 12f), 0), Quaternion.identity);
            esperaTiro = timerTiro;
        }
    }
}
