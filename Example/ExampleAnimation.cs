using System.Collections;
using System.Collections.Generic;
using Dooms.Animation;
using UnityEngine;
namespace IPVR.Animations
{
    public class ExampleAnimation : TimeAnimation
    {

        // You can utilize the AnimationData scriptableobject if you'd like, or you can just code anims by hand.

        private Vector3 start, end;

        //? for the slice example
        private Vector3 start2, end2;

        public override void OnAwake()
        {
            base.OnAwake();
            // Visually, this already occurs. However, it's good to know what the animation data is by default.
            SetSpeed(1.5); // how fast the animation progresses (float or double)
            SetDirection(0); // forward (1), back (-1), or still (0)
            SetTime(0); // 0-1

            // end is easiest to set by whatever the position already is
            end = transform.position + (Vector3.up * 2f);

            // then just add an offset to move the position
            start = transform.position + (Vector3.right * 3f) + (Vector3.up * 2f);


            start2 = end;
            end2 = transform.position;

        }
        public override void OnStart()
        {
            SetDirection(1); // start the animation
        }

        public override void OnAnimate()
        {
            base.OnAnimate();

            //? lerp using the time variable

            // transform.position = Vector3.Lerp(start, end, time);

            //? you can do this with scale and rotation, or any other property. Simply lerp the value by time.

            //? you can also slice the animation into segments.

            // a segment from 0 to .5 (halfway)
            float segment1 = Slice(time, 0f, .5f);

            // a segment from .5 to 1.
            float segment2 = Slice(time, .5f, 1f);

            //? from here you can use the segments you created like this.
            //? (we check if the time is past the minimum point we set above)

            // if(time > 0f) transform.position = Vector3.Lerp(start, end, segment1);
            // if(time > .5f) transform.position = Vector3.Lerp(start2, end2, segment2);

            //? finally, let's put it all together and add easing to the animation

            if (time > 0f) transform.position = Vector3.Lerp(start, end, Dooms.Math.Easing.EaseOutCirc(segment1));
            if (time > .5f) transform.position = Vector3.Lerp(start2, end2, Dooms.Math.Easing.EaseOutCirc(segment2));
        }

        public override void OnComplete()
        {
            base.OnComplete();

            // this marks the animation as complete. This occurs when time = 1 or -1 with appropriate direction.
        }
    }
}