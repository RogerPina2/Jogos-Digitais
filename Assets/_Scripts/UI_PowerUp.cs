using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_PowerUp : MonoBehaviour
{
    Text textComp;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.powerUp) textComp.color = Color.red;
        else textComp.color = Color.white;

        if (gm.powerUpTime == 0.0) 
        {
            textComp.text = $"Power-up em: ESPAÇO para usar.";
        }
        else
        {
            textComp.text = $"Power-up em: {gm.powerUpTime.ToString("N2")}";
        } 
    }
}
