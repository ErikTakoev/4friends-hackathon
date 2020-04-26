using System.Collections;
using UnityEngine;
using MiniGame;

public class MeteorController : MonoBehaviour
{
    [SerializeField] private PowerActionController powerActionController;
    [SerializeField] private Collider2D girlCollider;
    [SerializeField] private Collider2D meteorCollider;
    [SerializeField] private Animation flyAnimation;
    [SerializeField] private Transform cameraTransform;

    const string farFlyAnimName = "FarMeteorAnimation";
    const string nearFlyAnimName = "NearMeteorAnimation";

    Transform origParent;
    Vector3 currShift;
    Vector3 currPosition;

    private void Start()
    {
        origParent = transform.parent;
        currPosition = transform.localPosition;

        gameObject.SetActive(false);
        powerActionController.StopMG(true);
    }

    public void StartFlying(Vector3 shift)
    {
        gameObject.SetActive(true);

        currPosition += shift - currShift;
        currShift = shift;

        transform.localPosition = currPosition;
        transform.parent = cameraTransform;

        flyAnimation.Play(farFlyAnimName);
        Invoke("OnFarFlyEnd", flyAnimation.GetClip(farFlyAnimName).length);
    }

    private void OnFarFlyEnd()
    {
        flyAnimation.Play(nearFlyAnimName);
        transform.parent = origParent;

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
