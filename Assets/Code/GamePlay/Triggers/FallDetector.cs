﻿using UnityEngine;
using VContainer;

namespace Code.GamePlay.Triggers
{
    public class FallDetector : MonoBehaviour
    {
        private ITriggersDetector triggersDetector;

        private LayerMask layerMask;

        [Inject]
        public void Construct(ITriggersDetector triggersDetector)
        {
            this.triggersDetector = triggersDetector;
        }

        private void Start()
        {
            layerMask = LayerMask.NameToLayer("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == layerMask)
            {
                triggersDetector.DetectTrigger(ETriggers.Fall);
            }
        }
    }
}