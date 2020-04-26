using UnityEngine;

[System.Serializable]
public class ParallaxGameObject
{
	public Transform transform;
	public Vector3 coef = Vector3.one;
}

public class ParallaxController : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
    [SerializeField] private ParallaxGameObject[] transformParameters;

	Vector3 prevPos;

    private void Awake()
    {
		playerController.SubscribeForMove(OnMove);
	}

	private void OnMove(Vector3 newPos)
	{
		Vector3 shift = newPos - prevPos;
		foreach (ParallaxGameObject data in transformParameters)
		{
			if (data.transform == null || data.coef == Vector3.zero)
				continue;

			Vector3 objShift = shift;
			objShift.Scale(data.coef);
			data.transform.localPosition += objShift;
		}
		prevPos = newPos;
	}
}
