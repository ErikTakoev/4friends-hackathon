using UnityEngine;

public class MeteoriteGenerator : MonoBehaviour
{
    [SerializeField] private MeteorController meteorController;
    [SerializeField] private Transform girlTransform;
    [SerializeField] private float spawnPeriod = 10;
    [SerializeField] private float[] persPosToSpawn;

    private float spawnTimer;

    void Awake()
    {
        if (meteorController == null)
        {
            Debug.Assert(false);
            return;
        }

        spawnTimer = Random.Range(0, spawnPeriod);
    }

    void Update()
    {
        if (meteorController.gameObject.activeSelf)
            return;

        spawnTimer = Mathf.Min(spawnTimer + Time.deltaTime, spawnPeriod);
        if (spawnTimer == spawnPeriod)
        {
            spawnTimer = 0;
            meteorController.StartFlying(girlTransform.localPosition);
        }
    }
}
