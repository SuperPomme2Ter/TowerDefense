using UnityEngine;

public class S_WaveSpawner : MonoBehaviour
{
    S_EnnemiesSpawner spawner;
    float timer;
    [SerializeField]
    float spawnRate;

    private void Start()
    {
        spawner=GetComponent<S_EnnemiesSpawner>();
    }
    void Update()
    {
        if (timer >= spawnRate)
        {
            spawner.SpawnEnnemy(0);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
