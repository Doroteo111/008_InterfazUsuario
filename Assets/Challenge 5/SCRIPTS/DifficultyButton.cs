using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty; //diferenciar los botones
    private Button _button; //adecder a su cmponente
    private GameManager gameManager; //relacionarme con el game manager

    private void Awake() //pillar cada compoenete y decir que vale cada boton
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty); //adceder cada que hacemos click y llamamos a la función
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void SetDifficulty() //pilla el gamemanager + startgame + dificultad
    {
        gameManager.StartGame(difficulty);
    }
}
