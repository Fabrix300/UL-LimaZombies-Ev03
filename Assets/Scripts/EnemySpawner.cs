using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public float timeBetweenHordes;
    [HideInInspector] public float time;
    [HideInInspector] public int wave = 0;
    public GameObject[] spawnPoints;
    public GameObject[] enemiesPrefabs;
    public Text waveIndicatorText;
    public Text waveTimerText;

    private int enemiesPerHorde;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        //enemiesPerHorde = gameManager.enemiesPerHorde;
        timeBetweenHordes = 60f;
        time = timeBetweenHordes;
        wave += 1;
        waveIndicatorText.text = "Oleada: " + wave;
        waveTimerText.text = "Prox. oleada en: " + time;
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        time = Mathf.Clamp(time, 0f, 60f);
        if (time <= 0f)
        {
            AdvanceWave();
        }
        waveTimerText.text = "Prox. oleada en: " + time.ToString("0.00");
    }

    public void AdvanceWave()
    {
        wave += 1;
        waveIndicatorText.text = "Oleada: " + wave;
        timeBetweenHordes -= 5f;
        timeBetweenHordes = Mathf.Clamp(timeBetweenHordes, 10, 60);
        time = timeBetweenHordes;
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerHorde; i++)
        {
            Instantiate(
                enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], 
                spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position,
                Quaternion.identity
            );


        }
    }
}
