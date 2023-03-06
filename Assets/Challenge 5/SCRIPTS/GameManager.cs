using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //contorlara la instancion de los objetos, logica global y la dificultad
    public GameObject[] targetPrefabs;
    private float minX = -3.75f; //puntos especificos
    private float minY = -3.75f;
    private float distanceBetweenSquares = 2.5f;

    public bool isGameOver; //aparecer objeto
   
    public List<Vector3> targetPositionsInScene; //posiciones ocupadas en la regilla
   

    public TextMeshProUGUI scoreText; //enseñar los puntos que obtienes
    public GameObject gameOverPanel; //asignar los paneles
    public GameObject startGamePanel;

    public TextMeshProUGUI heartsText;

    private Vector3 randomPos;
    private int score;
    
    private float spawnRate = 2f;
    private int hearts = 3;

    public bool hasPowerupShield;
    
   private void Start() //eliminamos esto, ya que el start esta en otro script, botones
    {
        gameOverPanel.SetActive(false);
        startGamePanel.SetActive(true);
        
        /*isGameOver = false;
        StartCoroutine("SpawnRandomTarget");
    
        score = 0;
        scoreText.text = $"Score:  {score}";

        gameOverPanel.SetActive(false); //que no aparezca
        */
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // aparezca al morir
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //recargar la escena actual, coge el nombre y lo recarga
    }
    private Vector3 RandomSpawnPosition() //cuantos saltos doy
    {
        float spawnPosX = minX + Random.Range(0, 4) *distanceBetweenSquares; //enq columna 
        float spawnPosY = minY + Random.Range(0, 4) * distanceBetweenSquares;
        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    private IEnumerator SpawnRandomTarget() //Corrutina
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate); //Esperamos tiempo de aparicion
            int randomIndex = Random.Range(0, targetPrefabs.Length); //que elemento haremos aparecer
            randomPos = RandomSpawnPosition(); //en que cuadrado
            while (targetPositionsInScene.Contains(randomPos)) //mientras comntenga una posicion, la posicionesta ocupada
            {
                randomPos = RandomSpawnPosition(); //busca otra posicion
            }
            Instantiate(targetPrefabs[randomIndex], randomPos,targetPrefabs[randomIndex].transform.rotation); //que, como
            targetPositionsInScene.Add(randomPos); //añadir posicion ocupada a la lista
        }
    }
    public void UpdateScore(int newPoints)
    {
        score += newPoints;
        scoreText.text = $"Score:  {score}";
    }

    
    public void StartGame(int difficulty) //resetaar las dificultades e iniciar la partida
    {
        isGameOver = false;

        score = 0;
        UpdateScore(0);//llamamos a la función

        hearts = 3;
        heartsText.text = $"Hearts:  {hearts}";

        spawnRate /= difficulty;
        StartCoroutine(SpawnRandomTarget());
        startGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    public void MinusLife()
    {
        hearts--;
        heartsText.text = $"Hearts:  {hearts}";
        if (hearts <= 0)
        {
           
            GameOver();
        }
    }

}
