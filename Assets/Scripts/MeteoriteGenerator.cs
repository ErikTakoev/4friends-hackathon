using UnityEngine;

public class MeteoriteGenerator : MonoBehaviour
{
    [SerializeField] MeteoriteMG mgScript;
    [SerializeField] float spawnPeriod = 10;

    float spawnTimer;

    void Awake()
    {
        if (mgScript == null)
        {
            Debug.Assert(false);
            return;
        }

        mgScript.StopMG();
        spawnTimer = Random.Range(0, spawnPeriod);
    }

    void Update()
    {
        if (mgScript.IsMGRunning())
            return;

        spawnTimer = Mathf.Min(spawnTimer + Time.deltaTime, spawnPeriod);
        if (spawnTimer == spawnPeriod)
        {
            spawnTimer = 0;
            mgScript.PlayMG();
        }
    }
}
