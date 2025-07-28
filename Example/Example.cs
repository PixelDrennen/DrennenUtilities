using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dooms.Animation
{
    public class Example : MonoBehaviour
    {
        public TimeAnimation timeAnimation;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // timeAnimation.SetTime(0);
                if (timeAnimation.GetDirection() != 0)
                    timeAnimation.SetDirection(-timeAnimation.GetDirection());
                else timeAnimation.SetDirection(1);
            }
        }
    }
}