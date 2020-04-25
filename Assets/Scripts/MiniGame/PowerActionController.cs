using System;
using UnityEngine;

namespace MiniGame
{
    [RequireComponent(typeof(PowerActionComponent), typeof(PowerActionProvider))]
    public class PowerActionController : MonoBehaviour
    {
        [SerializeField] private PowerActionComponent actionComponent;
        [SerializeField] private PowerActionProvider actionProvider;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private MoveCamera moveCamera;

        public event Action OnEnd;
        private event Action OnUpdate;
        
        private bool isStarted = false;
        private int winCounter = 0;
        private int loseCounter = 0;
        
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
            StopMG();
        }

        private void LoseAction()
        {
            Debug.LogWarning($"Lose");
            ++loseCounter;
            StopMG();
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

        public void StopMG()
        {
            isStarted = false;
            actionComponent.IsStoped = true;
            playerInput.PauseMovement(false);
            moveCamera.Pause(false);
            gameObject.SetActive(false);
            OnEnd?.Invoke();
        }

        public void PlayMG()
        {
            isStarted = true;
            actionComponent.IsStoped = false;
            playerInput.PauseMovement(true);
            moveCamera.Pause(true);
            gameObject.SetActive(true);
        }
    }
}