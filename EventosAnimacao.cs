using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventosAnimacao : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;
    [SerializeField] private Animator[] uiImagens;
    [SerializeField] private int[] linhaDoEvento;
    [SerializeField] private int[] indexObjeto;
    [SerializeField] private string[] animacao;
    private bool impedir;
    private int falaAnterior;
    // Start is called before the first frame update
    void Start()
    {
        falaAnterior = 0;
        impedir = true;
    }

    // Update is called once per frame
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
        uiImagens[indexObjeto[id]].Play(animacao[id],0,0);
    }
}
