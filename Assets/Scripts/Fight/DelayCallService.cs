using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fight
{
    public class DelayCallService : MonoBehaviour, IDisposable
    {
        private readonly List<TickCall> ticks = new List<TickCall>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
            ServiceLocator.Add(this);
        }

        private void Update()
        {
            for (var index = 0; index < ticks.Count; index++)
            {
                var tick = ticks[index];

                if (tick.CheckAndComplete())
                {
                    tick.Dispose();

                    ticks.RemoveAt(index);
                }
            }
        }

        public void AddTick(float delay, Action callback)
        {
            var tick = new TickCall(delay, callback);
            
            ticks.Add(tick);
        }

        public void Dispose()
        {
            Destroy(this);
        }
    }
}