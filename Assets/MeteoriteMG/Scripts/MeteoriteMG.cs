using UnityEngine;
using UnityEngine.UI;

public class MeteoriteMG : MonoBehaviour
{
    [SerializeField] Image leftRed;
    [SerializeField] Image rightRed;
    [SerializeField] Image green;
    [SerializeField] Transform arrowTransform;
    [SerializeField] PlayerInput playerInput;

    [SerializeField] float time = 1;
    [SerializeField] float minArrowAngle = -80;
    [SerializeField] float maxArrowAngle = 80;

    float angleLR;
    float angleRR;
    float angleG;

    float cycleTime;
    bool reverse;
    bool stopped;

    int winCounter = 0;
    int loseConter = 0;

    void Awake()
    {
        if (leftRed == null || rightRed == null || green == null)
        {
            Debug.Assert(false);
            return;
        }

        const float fullAngle = 180;

        angleLR = fullAngle * leftRed.fillAmount;
        angleRR = fullAngle * rightRed.fillAmount;
        angleG = fullAngle * green.fillAmount;

        Debug.Assert(angleLR + angleRR + angleG == fullAngle);

        green.transform.eulerAngles = new Vector3(0, 0, -angleLR);

        cycleTime = time;
        reverse = false;
        arrowTransform.eulerAngles = new Vector3(0, 0, Random.Range(minArrowAngle, maxArrowAngle));
    }

    void Update()
    {
        if (stopped)
            return;

        cycleTime = Mathf.Max(cycleTime - Time.deltaTime, 0);

        float newAngle;
        if (!reverse)
            newAngle = Mathf.Lerp(minArrowAngle, maxArrowAngle, cycleTime / time);
        else
            newAngle = Mathf.Lerp(maxArrowAngle, minArrowAngle, cycleTime / time);

        arrowTransform.eulerAngles = new Vector3(0, 0, newAngle);
        if (cycleTime == 0)
        {
            reverse = !reverse;
            cycleTime = time;
        }

        if (Input.GetMouseButtonDown(0))
        {
            stopped = true;
            if (angleLR < arrowTransform.eulerAngles.z && arrowTransform.eulerAngles.z < angleLR + angleG)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }
    }

    void Lose()
    {
        ++loseConter;
        StopMG();
    }

    void Win()
    {
        ++winCounter;
        StopMG();
    }

    public bool IsMGRunning()
    {
        return gameObject.activeSelf;
    }

    public void StopMG()
    {
        stopped = true;
        playerInput.PauseMovement(false);
        gameObject.SetActive(false);
    }

    public void PlayMG()
    {
        stopped = false;
        playerInput.PauseMovement(true);
        gameObject.SetActive(true);
    }
}
