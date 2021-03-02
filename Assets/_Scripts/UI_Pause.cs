using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Pause : MonoBehaviour
{
    GameManager gm;

    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    public void Retomar()
    {
        gm.ChangeState(GameManager.GameState.GAME);
    }

    public void Inicio()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
