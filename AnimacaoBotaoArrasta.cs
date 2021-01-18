using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoBotaoArrasta : MonoBehaviour
{
    [SerializeField] private BotaoArrastar scriptBotao;
    

    public void Interagivel()
    {
        scriptBotao.SetInteragivel();
    }

    public void Desativado()
    {
        scriptBotao.SetDesativado();
    }
    
}
