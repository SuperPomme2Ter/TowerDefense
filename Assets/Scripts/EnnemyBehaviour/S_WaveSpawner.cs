using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_WaveSpawner : MonoBehaviour
{
    [Serializable] struct WaveComposition
    {
        public SO_WaveComposition Composition;
        public float timeGap;
    }
    [Serializable]
    struct Waves
    {
        public List<WaveComposition> wavePart;
    }
    S_EnnemiesSpawner spawner;
    [SerializeField] List<Waves> waveList = new List<Waves>();
    private List<WaveComposition> waveComposition = new();
    Queue<WaveComposition> compositions = new();
    Queue<Waves> waves=new();
    private Waves actualWave;
    private WaveComposition actualComposition;
    float timer;
    float spawnRate=1;
    int enemiesSpawned=0;
    bool waiting=false;
    private float waitingTimer = 0;
    private float gapBetweenComposition = 0;

    private void Start()
    {
        foreach (Waves wave in waveList)
        {
            waves.Enqueue(wave);
            
        }
        GetWave();
        GetComposition();
        spawner=GetComponent<S_EnnemiesSpawner>();
        enabled=false;
        
    }

    void Update()
    {

        if (timer >= spawnRate)
        {
            spawner.SpawnEnnemy(actualComposition.Composition.allEnemiesType[Random.Range(0, actualComposition.Composition.allEnemiesType.Count)]);
            enemiesSpawned++;
            timer = 0;
            if (enemiesSpawned >= actualComposition.Composition.enemyQuantity)
            {
                if (!GetComposition())
                {
                    enabled = false;
                }


            }
        }
        timer += Time.deltaTime;
    }

    void GetWave()
    {
        if (waves.TryDequeue(out actualWave))
        {
            SetWaveComposition(actualWave);
            return;
        }
        enabled=false;
    }
    void SetWaveComposition(Waves wave)
    {
        foreach (WaveComposition composition in wave.wavePart)
        {
            compositions.Enqueue(composition);
        }
    }

    bool GetComposition()
    {
        if(compositions.TryDequeue(out actualComposition))
        {
            spawnRate=actualComposition.Composition.spawnRate;
            enemiesSpawned=0;
            return true;
        }
        GetWave();
        actualComposition = compositions.Dequeue();
        return false;

    }
    
}
