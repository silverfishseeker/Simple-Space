using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Engine : MonoBehaviour
{
    public static Engine en;

    public float poderVal;
    public float poder{
        get => poderVal;
        set{
            poderVal = value;
            ActualizarPoderDisplayer();
        }
    }

    public float dificultad;
    public int puntuacion;

    public float cadencia => 100 / (dificultad + 20);
    public float cadenciaDificultad;

    public enum GameState { Menu, InGame, GameOver };
    public GameState currentState;

    public bool isOn => currentState == GameState.InGame;

    public int puntosSeta;
    public int puntosMejora;

    public float alturaMaxima;

    public GameObject menuUI;
    public GameObject gameOverUI;
    public GameObject prefabCuadrado;
    public GameObject prefabPlaneta;
    public GameObject prefabEarth;
    public GameObject prefabSeta;
    public GameObject prefabVidaExtra;
    public GameObject prefabFlower;
    public GameObject prefabFlorSpread;
    public GameObject prefabFlorNegra;
    public TextMeshProUGUI scoreDisplayer;
    public TextMeshProUGUI poderDisplayer;
    public float yIni;
    public float xIniMin;
    public float xIniMax;

    private float nextTime;
    private float nextDificultyTime;

    private Vector3 spawnPos => new Vector3(Random.Range(xIniMin, xIniMax), yIni, 0);

    private void ActualizarPoderDisplayer() => poderDisplayer.text=poder.ToString();

    void OnEnable() {
        // singleton
        if (en != null) {
            Destroy(gameObject);
            return;
        }
        en = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        RestartGame();
    }

    public void Prueba(){
        Instantiate(
            Random.Range(0,3) switch {
                0 => prefabFlower,
                1 => prefabFlorSpread,
                2 => prefabFlorNegra,
                _ => null
            },
            spawnPos, Quaternion.identity);
    }

    void Update() {
        switch (currentState) {
            case GameState.InGame:
                while (nextTime < Time.time) {
                    float r = Random.Range(0,500);
                    if(r < 1)
                        Instantiate(prefabVidaExtra, spawnPos, Quaternion.identity);
                    else if(r < 8)
                        Instantiate(prefabEarth, spawnPos, Quaternion.identity);
                    else if(r < 100)
                        Instantiate(prefabPlaneta, spawnPos, Quaternion.identity);
                    else
                        Instantiate(prefabCuadrado, spawnPos, Quaternion.identity);
                    nextTime += cadencia;
                }

                while (nextDificultyTime < Time.time){
                    dificultad++;
                    nextDificultyTime+=cadenciaDificultad;
                }

                break;

            case GameState.Menu:
                break;

            case GameState.GameOver:
                break;
        }
    }

    public void StartGame() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ActualizarPoderDisplayer();
        nextDificultyTime = cadenciaDificultad;
        nextTime = Time.time-1;
        currentState = GameState.InGame;
        menuUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    // Método para mostrar la pantalla de Game Over
    public void GameOver() {
        currentState = GameState.GameOver;
        menuUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    // Método para reiniciar el juego desde el Game Over
    public void RestartGame() {
        currentState = GameState.Menu;
        menuUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void Score(int s) {
        if(isOn){
            for (int i = 0; i < s; i++) {
                puntuacion ++;
                if (puntuacion % puntosSeta == 0)
                    Instantiate(prefabSeta, spawnPos, Quaternion.identity);
                if (puntuacion % puntosMejora == 0)
                    Instantiate(
                        Random.Range(0,3) switch {
                            0 => prefabFlower,
                            1 => prefabFlorSpread,
                            2 => prefabFlorNegra,
                            _ => null
                        },
                        spawnPos, Quaternion.identity);
            }
            scoreDisplayer.text = puntuacion.ToString();
        }
    }
}
