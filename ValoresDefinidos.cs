using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ValoresDefinidos : MonoBehaviour
{

    //UI
    public string[] resultadoBotao;
    public Color[] corResultadoBotao;
    public int[] pontosResultadoBotao;

    //Botoes falsos
    public GameObject prefabBotaoToqueF;
    public GameObject[] prefabBotaoSeguraF;
    public GameObject[] prefabBotaoArrastaF;
    public GameObject[] prefabBotaoArrasta;

    //Textos falsos
    public string[] PalavrasFalsas;


    //Limites spawn botões
    [SerializeField] private RectTransform painelDeLimites;
   

    private void Awake()
    {

        Vector2 limites1 = new Vector2(painelDeLimites.position.x - (painelDeLimites.rect.width / 2), painelDeLimites.position.y - (painelDeLimites.rect.height / 2));
        Vector2 limites2 = new Vector2(painelDeLimites.position.x + (painelDeLimites.rect.width / 2), painelDeLimites.position.y + (painelDeLimites.rect.height / 2));

    }


    
    public RectTransform GetPainelSpawn()
    {
        
        return painelDeLimites;
    }
}
