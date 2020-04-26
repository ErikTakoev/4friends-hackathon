using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BackgroundObjectsData
{
    public Transform transform;
    public float speed;
    public float minDensity;
    public float maxDensity;
}

public class SpawnedBackgroundObjectsData
{
    public Transform transform;
    public float initialPos;
    public float speed;
    public float minDensity;
    public float maxDensity;
    public float width;
}

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] staticObjects;
    [SerializeField] private BackgroundObjectsData[] objects;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float destroyDeltaPosX = -20;
    [SerializeField] private float spawnDeltaPosX = 20;
    [SerializeField] private float posRandomizer = 1;
    [SerializeField] private int minObjects = 50;
    [SerializeField] private int maxObjects = 60;

    Dictionary<string, List<SpawnedBackgroundObjectsData>> objectsPool;
    Dictionary<string, List<SpawnedBackgroundObjectsData>> visibleObjects;
    Vector3 prevPos;

    int moveNumber = 0;
    int movesCountSpawn = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        if (playerController == null)
        {
            Debug.Assert(false);
            return;
        }

        PreSpawnObjects();
        CheckSpawnObjects(true);

        playerController.SubscribeForMove(OnMove);
    }

    private void OnMove(Vector3 newPos)
    {
        float dx = newPos.x - prevPos.x;
        foreach (Transform staticTransform in staticObjects)
        {
            staticTransform.localPosition += new Vector3(dx, 0, 0);
        }

        foreach (List<SpawnedBackgroundObjectsData> values in visibleObjects.Values)
        {
            foreach (SpawnedBackgroundObjectsData data in values)
            {
                if (data.transform == null || data.speed == 0.0f)
                    continue;

                float xShift = dx * data.speed;
                data.transform.localPosition += new Vector3(xShift, 0, 0);
            }
        }
        prevPos = newPos;

        ++moveNumber;
        if (moveNumber > movesCountSpawn)
        {
            CheckDestroyObjects();
            CheckSpawnObjects(false);

            moveNumber = 0;
        }
    }

    private void PreSpawnObjects()
    {
        float totalWidth = spawnDeltaPosX - destroyDeltaPosX;

        objectsPool = new Dictionary<string, List<SpawnedBackgroundObjectsData>>();
        visibleObjects = new Dictionary<string, List<SpawnedBackgroundObjectsData>>();

        for (int i = 0; i < objects.Length; ++i)
        {
            string name = objects[i].transform.name;
            float width = objects[i].transform.GetComponent<SpriteRenderer>().size.x;
            int objectsToSpawn = (int)Mathf.Ceil(totalWidth * (objects[i].minDensity + 1.0f) / width);

            objectsPool[name] = new List<SpawnedBackgroundObjectsData>();
            visibleObjects[name] = new List<SpawnedBackgroundObjectsData>();

            for (int j = 0; j < objectsToSpawn; ++j)
            {
                SpawnedBackgroundObjectsData newData = new SpawnedBackgroundObjectsData();

                newData.transform = Instantiate(objects[i].transform, transform);
                newData.initialPos = objects[i].transform.localPosition.x;
                newData.minDensity = objects[i].minDensity;
                newData.maxDensity = objects[i].maxDensity;
                newData.speed = objects[i].speed;
                newData.width = width;

                newData.transform.gameObject.SetActive(false);
                objectsPool[name].Add(newData);
            }

            objects[i].transform.gameObject.SetActive(false);
        }
    }

    private void CheckDestroyObjects()
    {
        foreach (List<SpawnedBackgroundObjectsData> objectsList in visibleObjects.Values)
        {
            for (int i = objectsList.Count - 1; i >= 0; --i)
            {
                SpawnedBackgroundObjectsData data = objectsList[i];
                if (data.transform == null || data.speed == 0.0f)
                    continue;

                if (data.transform.localPosition.x < prevPos.x + destroyDeltaPosX)
                {
                    string name = data.transform.name;

                    name = name.Replace("(Clone)", "");
                    data.transform.gameObject.SetActive(false);
                    objectsPool[name].Add(data);
                    objectsList.RemoveAt(i);
                }
            }
        }
    }

    private void CheckSpawnObjects(bool onAwake)
    {
        int newCount = Random.Range(minObjects, maxObjects);
        int currCount = visibleObjects.Values.Sum(list => list.Count);
        if (currCount >= newCount)
            return;

        List<string> canBeSpawned = new List<string>();
        foreach (var data in visibleObjects)
        {
            if (data.Value.Count == 0)
            {
                canBeSpawned.Add(data.Key);
                continue;
            }
            if (data.Value[data.Value.Count - 1].transform.localPosition.x > prevPos.x + spawnDeltaPosX)
            {
                continue;
            }
            if (objectsPool[data.Key].Count == 0)
            {
                continue;
            }
            canBeSpawned.Add(data.Key);
        }
        if (canBeSpawned.Count == 0)
        {
            return;
        }

        float minPos = prevPos.x + spawnDeltaPosX;
        for (int i = 0; i <= newCount; ++i)
        {
            int freeIndex = Random.Range(0, canBeSpawned.Count - 1);
            string key = canBeSpawned[freeIndex];
            var obj = objectsPool[key][0];
            float density = Random.Range(obj.minDensity, obj.maxDensity);
            float startPos = onAwake ? obj.initialPos : minPos;

            if (visibleObjects[key].Count != 0)
            {
                startPos = visibleObjects[key].Last().transform.localPosition.x + obj.width * (1 + density);
            }
            startPos += Random.Range(0, posRandomizer);

            Vector3 position = obj.transform.localPosition;
            position.x = startPos;
            obj.transform.localPosition = position;
            obj.transform.gameObject.SetActive(true);

            visibleObjects[key].Add(obj);
            objectsPool[key].RemoveAt(0);

            if (position.x > prevPos.x + spawnDeltaPosX)
            {
                canBeSpawned.Remove(key);
            }
            if (canBeSpawned.Count == 0)
                return;
        }
    }
}
