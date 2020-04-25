using UnityEngine;

public class MeteoriteGenerator : MonoBehaviour
{
    [SerializeField] private MeteorController meteorController;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform persPosToSpawnPoints;

    private float spawnTimer;
    private int lastCheckedPos = -1;

    void Awake()
    {
        if (meteorController == null || playerInput == null)
        {
            Debug.Assert(false);
            return;
        }

        playerInput.SubscribeForMove(OnMove);
    }

    void OnMove(Vector3 pos)
    {
        if (meteorController.gameObject.activeSelf)
            return;

        if (lastCheckedPos + 1 >= persPosToSpawnPoints.childCount)
            return;

        if (persPosToSpawnPoints.GetChild(lastCheckedPos + 1).localPosition.x <= pos.x)
        {
            ++lastCheckedPos;
            meteorController.StartFlying(pos);
        }
    }
}
