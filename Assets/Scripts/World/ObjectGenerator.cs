using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public float TimeGen;
    public Vector3 MinVelocity;
    public Vector3 MaxVelocity;
    public GameObject ObjectPrefab;
    public float ObjectMinScale = 0.5f;
    public float ObjectMaxScale = 1f;
    public float DestroyTime = 5;


    float lefttime;

    void Update()
    {

        lefttime += Time.deltaTime;

        if(lefttime > TimeGen)
        {
            lefttime = 0;

            GameObject go = Instantiate(ObjectPrefab, transform);
            go.transform.localScale = Random.Range(ObjectMinScale, ObjectMaxScale) * Vector3.one;
            go.transform.position = transform.position;

            Rigidbody2D r = go.GetComponent<Rigidbody2D>();
            r.AddForce(new Vector3(Random.Range(MinVelocity.x, MaxVelocity.x), Random.Range(MinVelocity.y, MinVelocity.y)), ForceMode2D.Impulse);
            r.AddTorque(100);

            if (DestroyTime != 0f)
                Destroy(go, DestroyTime);
        }
    }
}
