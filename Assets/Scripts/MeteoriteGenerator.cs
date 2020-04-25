using UnityEngine;
using MiniGame;

public class MeteoriteGenerator : MonoBehaviour
{
    [SerializeField] PowerActionController powerActionController;
    [SerializeField] float spawnPeriod = 10;

    float spawnTimer;

    void Awake()
    {
        if (powerActionController == null)
        {
            Debug.Assert(false);
            return;
        }

        powerActionController.StopMG();
        spawnTimer = Random.Range(0, spawnPeriod);
    }

    void Update()
    {
        if (powerActionController.IsMGRunning())
            return;

        spawnTimer = Mathf.Min(spawnTimer + Time.deltaTime, spawnPeriod);
        if (spawnTimer == spawnPeriod)
        {
            spawnTimer = 0;
            powerActionController.PlayMG();
        }
    }
}
