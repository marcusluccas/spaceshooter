using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        int qtd = FindObjectsOfType<GameManager>().Length;

        if (qtd > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayMorte()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }

    public void CarregarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void VoltaMenu()
    {
        StartCoroutine(DelayMorte());
    }

    public void Sair()
    {
        Application.Quit();
    }
}
