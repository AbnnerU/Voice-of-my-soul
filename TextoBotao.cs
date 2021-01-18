using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextoBotao : MonoBehaviour
{
    [SerializeField] private bool botaoCorreto;
    private TextMeshPro texto;
    private CanvasManeger palavra;   
    private ValoresDefinidos valores;

    void Start()
    {
        valores = FindObjectOfType<ValoresDefinidos>();
        palavra = FindObjectOfType<CanvasManeger>();
        texto = GetComponent<TextMeshPro>();
        if (botaoCorreto)
        {
            texto.text = palavra.GetPalavraBotao();
        }
        else
        {
            DefinirTexto();
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.parent.gameObject.transform.rotation.z);
    }


    private void OnEnable()
    {
        valores = FindObjectOfType<ValoresDefinidos>();
        palavra = FindObjectOfType<CanvasManeger>();
        texto = GetComponent<TextMeshPro>();
        if (botaoCorreto)
        {
            texto.text = palavra.GetPalavraBotao();
        }
        else
        {
            DefinirTexto();
        }
    }


    private void DefinirTexto()
    {
        int tamanho = palavra.GetPalavraBotao().Length;

        switch (tamanho)
        {
            case 1:
                texto.text = PalavrasDataBase.palavras1[Random.Range(0, PalavrasDataBase.palavras1.Length - 1)];
                break;
            case 2:
                texto.text = PalavrasDataBase.palavras2[Random.Range(0, PalavrasDataBase.palavras2.Length - 1)];
                break;
            case 3:
                texto.text = PalavrasDataBase.palavras3[Random.Range(0, PalavrasDataBase.palavras3.Length - 1)];
                break;
            case 4:
                texto.text = PalavrasDataBase.palavras4[Random.Range(0, PalavrasDataBase.palavras4.Length - 1)];
                break;
            case 5:
                texto.text = PalavrasDataBase.palavras5[Random.Range(0, PalavrasDataBase.palavras5.Length - 1)];
                break;
            case 6:
                texto.text = PalavrasDataBase.palavras6[Random.Range(0, PalavrasDataBase.palavras6.Length - 1)];
                break;
            case 7:
                texto.text = PalavrasDataBase.palavras7[Random.Range(0, PalavrasDataBase.palavras7.Length - 1)];
                break;
            case 8:
                texto.text = PalavrasDataBase.palavras8[Random.Range(0, PalavrasDataBase.palavras8.Length - 1)];
                break;
            case 9:
                texto.text = PalavrasDataBase.palavras9[Random.Range(0, PalavrasDataBase.palavras9.Length - 1)];
                break;
            case 10:
                texto.text = PalavrasDataBase.palavras10[Random.Range(0, PalavrasDataBase.palavras10.Length - 1)];
                break;
        }
    }

}
