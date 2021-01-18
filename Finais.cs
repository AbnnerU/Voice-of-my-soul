using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finais : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;
    [SerializeField] private TextAsset[] finaisRoteiro;
    [SerializeField] private float[] pontosIntimidade;

    void Awake()
    {
        if(PlayerSave.GetPontosIntimidade() <= pontosIntimidade[0])
        {
            dialogos.SetRoteiro(finaisRoteiro[0]);
        }
        else if(PlayerSave.GetPontosIntimidade() > pontosIntimidade[0] && PlayerSave.GetPontosIntimidade() < pontosIntimidade[1])
        {
            dialogos.SetRoteiro(finaisRoteiro[1]);
        }
        else if (PlayerSave.GetPontosIntimidade() > pontosIntimidade[1] && PlayerSave.GetPontosIntimidade() < pontosIntimidade[2])
        {
            dialogos.SetRoteiro(finaisRoteiro[2]);
        }
        else
        {
            dialogos.SetRoteiro(finaisRoteiro[2]);
        }
    }

   
}
