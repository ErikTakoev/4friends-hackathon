using System;
using UnityEngine;

namespace MiniGame
{
    [RequireComponent(typeof(PowerActionComponent), typeof(PowerActionProvider))]
    public class PowerActionController : MonoBehaviour
    {
        [SerializeField] private PowerActionComponent actionComponent;
        [SerializeField] public  PowerActionProvider actionProvider;
        [SerializeField] private PlayerController playerInput;
        [SerializeField] private MoveCamera moveCamera;

        public event Action OnEnd;
        private event Action OnUpdate;
        
        private bool isStarted = false;
        private int winCounter = 0;
        private int loseCounter = 0;
        private float xDiff = 0;

        private string startAnimCamera = "ZoomInCamera";
        private string startAnimMinigame = "MinigameStart";
        private string endAnimCamera = "ZoomOutCamera";
        private string endAnimMinigame = "MinigameEnd";
        
        private void Awake()
        {
            actionProvider.Init(actionComponent);
            
            actionProvider.OnLose += LoseAction;
            actionProvider.OnWin += WinAction;

            OnUpdate += actionComponent.OnUpdate;
            OnUpdate += actionProvider.OnUpdate;
        }

        private void WinAction(bool data)
        {
            Debug.LogWarning($"Win perfect hit :{data}");
            ++winCounter;
            StopMG(false);
        }

        private void LoseAction()
        {
            Debug.LogWarning($"Lose");
            ++loseCounter;
            StopMG(false);
        }

        public void Update()
        {
            if (!isStarted)
            {
                return;
            }
            
            OnUpdate?.Invoke();
        }

        public bool IsMGRunning()
        {
            return gameObject.activeSelf;
        }

        public void StopMG(bool withoutAnim)
        {
            isStarted = false;
            actionComponent.IsStoped = true;

            if (!withoutAnim)
                PlayMinigameAnimEnd();
            else
                RestoreRunner();
        }

        private void PlayMinigameAnimEnd()
        {
            Animation anim = GetComponent<Animation>();
            int clipCount = anim.GetClipCount();
            if (anim == null || !anim.Play(endAnimMinigame))
            {
                Debug.Assert(false);
                return;
            }

            Invoke("PlayCameraAnimEnd", anim.GetClip(endAnimMinigame).length);
        }

        private void PlayCameraAnimEnd()
        {
            Animation cameraAnim = moveCamera.GetComponent<Animation>();
            if (cameraAnim == null || !cameraAnim.GetClip(endAnimCamera))
            {
                Debug.Assert(false);
                return;
            }

            Keyframe[] keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, xDiff);
            keys[1] = new Keyframe(1.0f, 0.0f);
            AnimationCurve curve = new AnimationCurve(keys);
            cameraAnim.GetClip(endAnimCamera).SetCurve("Camera", typeof(Transform), "localPosition.x", curve);
            cameraAnim.Play(endAnimCamera);

            gameObject.SetActive(false);
            Invoke("RestoreRunner", cameraAnim.GetClip(endAnimCamera).length);
        }

        private void RestoreRunner()
        {
            playerInput.PauseMovement(false);
            moveCamera.Pause(false);
            gameObject.SetActive(false);
            OnEnd?.Invoke();
        }

        public void PlayMG()
        {
            playerInput.PauseMovement(true);
            moveCamera.Pause(true);
            xDiff = playerInput.transform.localPosition.x - moveCamera.transform.localPosition.x;

            PlayCameraAnimStart();
        }

        private void PlayCameraAnimStart()
        {
            Animation cameraAnim = moveCamera.GetComponent<Animation>();
            if (cameraAnim == null || !cameraAnim.GetClip(startAnimCamera))
            {
                Debug.Assert(false);
                return;
            }

            Keyframe[] keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, 0.0f);
            keys[1] = new Keyframe(1.0f, xDiff);
            AnimationCurve curve = new AnimationCurve(keys);
            cameraAnim.GetClip(startAnimCamera).SetCurve("Camera", typeof(Transform), "localPosition.x", curve);
            cameraAnim.Play(startAnimCamera);

            Invoke("PlayMinigameAnimStart", cameraAnim.GetClip(startAnimCamera).length);
        }

        private void PlayMinigameAnimStart()
        {
            Animation anim = GetComponent<Animation>();
            if (anim == null || !anim.Play(startAnimMinigame))
            {
                Debug.Assert(false);
                return;
            }

            gameObject.SetActive(true);
            Invoke("ActivateMG", anim.GetClip(startAnimMinigame).length);
        }

        private void ActivateMG()
        {
            isStarted = true;
            actionComponent.IsStoped = false;
        }
    }
}