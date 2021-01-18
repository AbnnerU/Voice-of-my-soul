using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventosImagem : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;
    [SerializeField] private Image[] uiImagens;
    [SerializeField] private int[] linhaDoEvento;
    [SerializeField] private int[] indexObjeto;
    [SerializeField] private Sprite[] novoSprite;
    private bool impedir;
    private int falaAnterior;
    void Start()
    {
        falaAnterior = 0;
        impedir = true;           
            
    }

  
    void Update()
    {
        if (impedir == false)
        {
            for (int i = 0; i < linhaDoEvento.Length; i++)
            {
                if (dialogos.GetNumeroFala() == linhaDoEvento[i])
                {
                    impedir = true;
                    NovoEvento(i);
                }
            }
            impedir = true;
        }

        if (falaAnterior != dialogos.GetNumeroFala())
        {
            impedir = false;
            falaAnterior = dialogos.GetNumeroFala();
        }

        
    }

    private void NovoEvento(int id)
    {
        uiImagens[indexObjeto[id]].sprite = novoSprite[id];
        if (uiImagens[indexObjeto[id]].name != "Fundo") //migué
        {
            uiImagens[indexObjeto[id]].SetNativeSize();
        }

    }
}
