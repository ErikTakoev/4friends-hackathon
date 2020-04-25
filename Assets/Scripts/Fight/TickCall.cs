using System;
using UnityEngine;

namespace Fight
{
    public struct TickCall : IDisposable
    {
        private readonly DateTime targetTime;
        private Action callAction;

        public bool CheckAndComplete()
        {
            if (targetTime > DateTime.Now)
            {
                return false;
            }
            
            callAction.Invoke();
            return true;
        }

        public TickCall(float delay, Action action)
        {
            var time = DateTime.Now;
            targetTime = time.AddSeconds(delay);
            callAction = action;
        }

        public void Dispose()
        {
            callAction = null;
        }
    }
}