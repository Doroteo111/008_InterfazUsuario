using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private GameManager gameManager;

    public int points;

    public GameObject explosionParticle;

    // autodescruccion
    public float lifeTime = 2f;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        Destroy(gameObject, lifeTime);
    }

    private void OnDestroy()
    {
        gameManager.targetPositionsInScene.
         Remove(transform.position);
    }
    private void OnMouseDown()
    {
        if (!gameManager.isGameOver){
            if (gameObject.CompareTag("Bad"))
            {
                //gameManager.isGameOver = true;
                //gameManager.GameOver();
                if (gameManager.hasPowerupShield)
                {
                    gameManager.hasPowerupShield = false; //perder escudo

                }
                else 
                {
                    gameManager.MinusLife(); //pierdo una vida
                }
                
              
            }
            else if (gameObject.CompareTag("Good"))
            {
                gameManager.UpdateScore(points);
            }
            else if (gameObject.CompareTag("Shield"))
            {
                gameManager.hasPowerupShield = true;
            }


            Instantiate(explosionParticle, transform.position,explosionParticle.transform.rotation);

            Destroy(gameObject);
        }
        
    }


}
