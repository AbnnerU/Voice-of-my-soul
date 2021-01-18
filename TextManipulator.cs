using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManipulator : MonoBehaviour
{
    private Text texto;
    private int tamanhoDefault;

    private void Awake()
    {
        texto = GetComponent<Text>();
        tamanhoDefault = texto.fontSize;
    } 

    //public void SetTamanhoDefault()
    //{
    //    texto.fontSize = tamanhoDefault;
    //}

    public void AumentarFonte(int valor)
    {
        texto.fontSize = valor;
    }
}
