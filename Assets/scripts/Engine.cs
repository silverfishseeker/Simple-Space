using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour
{
    public static Engine en;

    public float poder;
    public float dificultad;
    public float puntuacion;

    public float cadencia => 100 / (dificultad + 20);
    public float cadenciaDificultad;

    public enum GameState { Menu, InGame, GameOver };
    public GameState currentState;

    public bool isOn => currentState == GameState.InGame;

    public int puntosSeta;

    public GameObject menuUI;
    public GameObject gameOverUI;
    public GameObject prefabCuadrado;
    public GameObject prefabPlaneta;
    public GameObject prefabEarth;
    public GameObject prefabSeta;
    public GameObject vidaExtra;
    public float yIni;
    public float xIniMin;
    public float xIniMax;

    private float nextTime;
    private float nextDificultyTime;

    private Vector3 spawnPos => new Vector3(Random.Range(xIniMin, xIniMax), yIni, 0);

    void OnEnable() {
        // singleton
        if (en != null) {
            Destroy(gameObject);
            return;
        }
        en = this;
    }

    void Start() {
        RestartGame();
    }

    public void Prueba(){
        Instantiate(vidaExtra, spawnPos, Quaternion.identity);
    }

    void Update() {
        switch (currentState) {
            case GameState.Menu:
                break;

            case GameState.InGame:
                while (nextTime < Time.time) {
                    float r = Random.Range(0,500);
                    if(r < 1)
                        Instantiate(vidaExtra, spawnPos, Quaternion.identity);
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

            case GameState.GameOver:
                break;
        }
    }

    public void StartGame() {
        nextDificultyTime = cadenciaDificultad;
        nextTime = 0;
        poder = 0;
        dificultad = 0;
        puntuacion = 0;
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

    public void Score(float s) {
        if(isOn){
            puntuacion += s;
            if (puntuacion % puntosSeta == 0)
                Instantiate(prefabSeta, spawnPos, Quaternion.identity);
        }
    }
}
