using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimition : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CriaBoss()
    {
        Instantiate(boss, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Recomeca()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.VoltaMenu();
        }
    }
}
