using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private string estado = "estado1";
    private float velocidade = 3;
    private Rigidbody2D meuRB;
    private float limiteX = 6f;
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(velocidade, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case "estado1":
                Estado1();
                break;
        }
    }

    private void Estado1()
    {
        if (transform.position.x > limiteX)
        {
            meuRB.velocity = new Vector2(-velocidade, 0f);
        }
        if (transform.position.x < -limiteX)
        {
            meuRB.velocity = new Vector2(velocidade, 0f);
        }
    }
}
