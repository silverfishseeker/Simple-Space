using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour
{
    public static Engine engine;

    public enum GameState { Menu, InGame, GameOver };
    public GameState currentState;

    public bool isOn => currentState == GameState.InGame;

    public GameObject menuUI;
    public GameObject gameOverUI;

    public GameObject prefabCuadrado;
    public float cadencia;
    public float yIni;
    public float xIniMin;
    public float xIniMax;

    private float nextTime;

    void OnEnable() {
        // singleton
        if (engine != null) {
            Destroy(gameObject);
            return;
        }
        engine = this;
    }

    void Start() {
        RestartGame();
    }

    void Update() {
        switch (currentState) {
            case GameState.Menu:
                break;

            case GameState.InGame:
                while (nextTime < Time.time) {
                    GenerarCuadrado();
                    nextTime += cadencia;
                }
                break;

            case GameState.GameOver:
                break;
        }
    }

    public void StartGame() {
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
        nextTime = 0;
    }

    void GenerarCuadrado() {
        float xPos = Random.Range(xIniMin, xIniMax);
        Vector3 spawnPos = new Vector3(xPos, yIni, 0);
        Instantiate(prefabCuadrado, spawnPos, Quaternion.identity);
    }
}
