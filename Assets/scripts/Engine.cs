using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public static Engine engine;

    public GameObject prefabCuadrado;
    public float cadencia;
    public float yIni;
    public float xIniMin;
    public float xIniMax;

    public int spawnTimeTableSize;

    private float[] spawnTimeTableLeft;
    private float[] spawnTimeTableRight;
    private bool isLeftSpawnTimeTable = true;
    private int spawnTimeIndex = 0;

    private float GetSpawnTimeTable() {
        if (spawnTimeIndex >= spawnTimeTableSize){
            float[] currTable = isLeftSpawnTimeTable ? spawnTimeTableLeft : spawnTimeTableRight;

            currTable[0] = (
                    !isLeftSpawnTimeTable ? spawnTimeTableLeft : spawnTimeTableRight
                )[spawnTimeTableSize-1] + cadencia;

            for (int i = 1; i < spawnTimeTableSize; i++){
                currTable[i] = currTable[i-1] + cadencia;
            }

            isLeftSpawnTimeTable = !isLeftSpawnTimeTable;
            spawnTimeIndex = 0;
        }
        return isLeftSpawnTimeTable ? spawnTimeTableLeft[spawnTimeIndex] : spawnTimeTableRight[spawnTimeIndex];
    }


    void OnEnable() {
        // singleton
        if (engine != null) {
            Destroy(gameObject);
            return;
        }
        engine = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        spawnTimeTableLeft = new float[spawnTimeTableSize];
        spawnTimeTableRight = new float[spawnTimeTableSize];

        spawnTimeTableLeft[0] = cadencia;
        for (int i = 1; i < spawnTimeTableSize; i++){
            spawnTimeTableLeft[i] = spawnTimeTableLeft[i-1] + cadencia;
        }
    }

    void Update() {
        while(GetSpawnTimeTable() < Time.time){
            spawnTimeIndex++;
            GenerarCuadrado();
        }
    }

    void GenerarCuadrado() {
        float xPos = Random.Range(xIniMin, xIniMax);
        Vector3 spawnPos = new Vector3(xPos, yIni, 0);
        Debug.Log(spawnPos);
        Instantiate(prefabCuadrado, spawnPos, Quaternion.identity);
    }
}
