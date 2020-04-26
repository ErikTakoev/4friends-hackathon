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
        private Vector3 posDiff;

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
            if (cameraAnim == null)
            {
                Debug.Assert(false);
                return;
            }

            Keyframe[] keys;
            AnimationCurve curve;
            AnimationClip clip;

            clip = new AnimationClip();
            clip.legacy = true;

            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, posDiff.x);
            keys[1] = new Keyframe(1.0f, 0.0f);
            curve = new AnimationCurve(keys);
            clip.SetCurve("Camera", typeof(Transform), "localPosition.x", curve);

            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, posDiff.y);
            keys[1] = new Keyframe(1.0f, 0.0f);
            curve = new AnimationCurve(keys);
            clip.SetCurve("Camera", typeof(Transform), "localPosition.y", curve);

            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, 3.0f);
            keys[1] = new Keyframe(1.0f, 5.0f);
            curve = new AnimationCurve(keys);
            clip.SetCurve("Camera", typeof(Camera), "orthographic size", curve);

            cameraAnim.AddClip(clip, endAnimCamera);
            cameraAnim.Play(endAnimCamera);

            gameObject.SetActive(false);
            Invoke("RestoreRunner", clip.length);
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
            posDiff = playerInput.transform.localPosition - moveCamera.transform.localPosition;

            PlayCameraAnimStart();
        }

        private void PlayCameraAnimStart()
        {
            Animation cameraAnim = moveCamera.GetComponent<Animation>();
            if (cameraAnim == null)
            {
                Debug.Assert(false);
                return;
            }

            Keyframe[] keys;
            AnimationCurve curve;
            AnimationClip clip;

            clip = new AnimationClip();
            clip.legacy = true;

            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, 0.0f);
            keys[1] = new Keyframe(1.0f, posDiff.x);
            curve = new AnimationCurve(keys);
            clip.SetCurve("Camera", typeof(Transform), "localPosition.x", curve);

            keys = new Keyframe[2];
            keys[0] = new Keyframe(0.0f, 0.0f);
            keys[1] = new Keyframe(1.0f, posDiff.y);
            curve = new AnimationCurve(keys);
            clip.SetCurve("Camera", typeof(Transform), "localPosition.y", curve);

            keys = new Keyframe[3];
            keys[0] = new Keyframe(0.0f,  5.0f);
            keys[1] = new Keyframe(0.82f, 2.5f);
            keys[2] = new Keyframe(1.0f,  3.0f);
            curve = new AnimationCurve(keys);
            clip.SetCurve("Camera", typeof(Camera), "orthographic size", curve);

            cameraAnim.AddClip(clip, startAnimCamera);
            cameraAnim.Play(startAnimCamera);

            Invoke("PlayMinigameAnimStart", clip.length);
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