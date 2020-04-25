using System.Collections;
using UnityEngine;
using MiniGame;

public class MeteorController : MonoBehaviour
{
    [SerializeField] private PowerActionController powerActionController;
    [SerializeField] private Collider2D girlCollider;
    [SerializeField] private Collider2D meteorCollider;
    [SerializeField] private Animation flyAnimation;

    Vector3 currShift;

    private void Start()
    {
        gameObject.SetActive(false);
        powerActionController.StopMG(true);
    }

    public void StartFlying(Vector3 shift)
    {
        gameObject.SetActive(true);

        transform.localPosition += shift - currShift;
        currShift = shift;

        flyAnimation.Play();
        StartCoroutine(CheckIntersection());
    }

    private IEnumerator CheckIntersection()
    {
        while (flyAnimation.isPlaying)
        {
            yield return new WaitForFixedUpdate();
            if (girlCollider.IsTouching(meteorCollider))
            {
                flyAnimation.Stop();
                powerActionController.PlayMG();
                powerActionController.OnEnd += OnEnd;
                yield break;
            }
        }

        OnEnd();
    }

    private void OnEnd()
    {
        powerActionController.OnEnd -= OnEnd;
        gameObject.SetActive(false);
    }
}
