using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{
    [Range(1,15)]
    public float velocidade = 5.0f;

    private Vector3 direcao;

    GameManager gm;

    private Vector2 leftBottom;
    private Vector2 rightTop;

    private SpriteRenderer ballRenderer;
    private Vector2 ballSize;
    private Vector2 ballHalfsize;
 
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        
        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);
        direcao = new Vector3(dirX, dirY).normalized;

        leftBottom  = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightTop    = Camera.main.ViewportToWorldPoint(Vector3.one);

        ballRenderer  = GetComponent<SpriteRenderer>();
        ballSize      = ballRenderer.bounds.size;
        ballHalfsize  = ballRenderer.bounds.extents;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        
        if (gm.gameState == GameManager.GameState.GAME) gm.tempo += Time.deltaTime;
        if (gm.powerUpTime > 0) gm.powerUpTime -= Time.deltaTime;
        else
        {
            gm.powerUpTime = 0;
            if (gm.powerUp == true) {
                gm.powerUpTime = 10;
                gm.powerUp = false; 
            } 
        } 

        transform.position += direcao * Time.deltaTime * velocidade;

        float ballT = transform.position.y + ballHalfsize.y;
        float ballB = transform.position.y - ballHalfsize.y;
        float ballL = transform.position.x - ballHalfsize.x;
        float ballR = transform.position.x + ballHalfsize.x;

        if (ballL < leftBottom.x || ballR > rightTop.x) direcao = new Vector3(-direcao.x, direcao.y);

        if (ballT > rightTop.y) direcao = new Vector3(direcao.x, -direcao.y);

        if (ballB < leftBottom.y) Reset();

        Debug.Log($"Vidas: {gm.vidas} \t | \t Pontos: {gm.pontos} \t | \t PUTime: {gm.powerUpTime}");
    }

    private void Reset()
    {   
        gm.powerUpTime = 10;
        gm.powerUp = false;

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + new Vector3(0, 0.5f, 0);

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(2.0f, 5.0f);

        direcao = new Vector3(dirX, dirY).normalized;
        gm.vidas--;

        if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }  
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.CompareTag("Player"))
        {
            float dirX = Random.Range(-5.0f, 5.0f);
            float dirY = Random.Range(1.0f, 5.0f);

            direcao = new Vector3(dirX, dirY).normalized;
        }
        else if(col.gameObject.CompareTag("Tijolo"))
        {
            direcao = new Vector3(direcao.x, -direcao.y);
            gm.pontos++;
        }

    }
}
