using System;
using UnityEngine;

namespace MiniGame
{
    public class PowerActionController : MonoBehaviour
    {
        [SerializeField] private PowerActionComponent actionComponent;
        [SerializeField] private PowerActionProvider actionProvider;

        private event Action OnUpdate;
        
        private bool isStarted = false;
        
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
        }

        private void LoseAction()
        {
            Debug.LogWarning($"Lose");
        }

        public void Update()
        {
            if (Input.GetMouseButtonUp(0) && !isStarted)
            {
                actionComponent.IsStoped = false;
                
                isStarted = true;
                
                return;
            }
            
            OnUpdate?.Invoke();
        }
    }
}