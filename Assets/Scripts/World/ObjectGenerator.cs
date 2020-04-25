using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public float TimeGen;
    public Vector3 Velocity;
    public GameObject ObjectPrefab;
    public Vector3 ObjectScale = Vector3.one;


    float lefttime;

    void Update()
    {

        lefttime += Time.deltaTime;

        if(lefttime > TimeGen)
        {
            lefttime = 0;

            GameObject go = Instantiate(ObjectPrefab, transform);
            go.transform.localScale = ObjectScale;
            go.transform.position = transform.position;

            Rigidbody2D r = go.GetComponent<Rigidbody2D>();
            r.AddForce(Velocity, ForceMode2D.Impulse);
            r.AddTorque(100);
        }
    }
}
