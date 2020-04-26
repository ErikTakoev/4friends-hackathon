using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxGameObject
{
	public Transform transform;
	public Vector3 coef;
}

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private ParallaxGameObject[] transformParameters;
	[SerializeField] private float accelCoef = 0.05f;

	Dictionary<Transform, Vector3> initialPositions;
	Vector3 screenCenter;
	float additionalCoef = 1000;

    private void Awake()
    {
		screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		initialPositions = new Dictionary<Transform, Vector3>();

		foreach (ParallaxGameObject data in transformParameters)
		{
			if (data.transform == null)
				continue;

			initialPositions.Add(data.transform, data.transform.localPosition);
		}
	}

    private void Update()
    {
		if (transformParameters.Length == 0 || accelCoef == 0.0f)
		{
			return;
		}

		Vector3 lastPos = Input.mousePosition;
		Vector3 shift = (lastPos - screenCenter) * additionalCoef;

		foreach (ParallaxGameObject data in transformParameters)
		{
			if (data.transform == null || data.coef == Vector3.zero)
				continue;

			Vector3 initialPos = initialPositions[data.transform];
			float newX = initialPos.x + Mathf.Sign(shift.x) * Mathf.Sqrt(Mathf.Abs(shift.x)) * data.coef.x * accelCoef / additionalCoef;
			float newY = initialPos.y + Mathf.Sign(shift.y) * Mathf.Sqrt(Mathf.Abs(shift.y)) * data.coef.y * accelCoef / additionalCoef;

			data.transform.localPosition = new Vector3(newX, newY, 0);
		}
	}
}
