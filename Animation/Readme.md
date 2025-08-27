# TimeAnimation
> https://github.com/PixelDrennen/DrennenUtilities/tree/main/Animation

See the included examples for details on how to implement **TimeAnimation**.  
You can subscribe to callbacks in the class similar to an event system.  
The two main callbacks are:

- **OnAnimate**  
- **OnComplete**  

> **Note**: This system depends on the `Drennen.Math` namespace.

---

## API

**Namespace:** `Drennen.Animation`

---

### TimeAnimation Options
 - **time** :: *public float*
 - **speed** :: *float* > SetSpeed
 - **direction** :: *float* > SetDirection
 - **completed** :: *bool* > OnComplete
 - **pingpong** :: *public bool*
 - **loop** :: *public bool*
 - **OnAnimateCallback** :: *Callback delegate*
 - **OnCompleteCallback** :: *Callback delegate*
 - **AnimationData** :: *Callback delegate*
 - **animationData** :: *public AnimationData*
 - **animationSetManager** :: *public AnimationSetManager*
 - **animatedObjects** :: *public List<AnimatedObject>*

---

### Methods

- **`SetSpeed(double/float speed)`**  
  Sets how fast the animation progresses.

- **`SetDuration(float seconds)`**  
  Overrides the speed to guarantee the animation completes in the specified number of seconds.

- **`SetDirection(int dir)`**  
  Sets the direction of the animation.  
  - `1` → forward  
  - `-1` → backward  
  - `0` → still / paused  

- **`SetTime(float time)`**  
  Sets the current time value of the animation (0 → 1).

- **`Slice(time, float start (0–1), float end (0–1))`**  
  Returns a float value representing the portion of the animation within the given range as a normalized (0–1) time value.  

  **Slice Example:**  
  ```csharp
  Slice(time, 0f, 0.5f);
  // Returns the time 0-0.5 mapped into a 0–1 range

---

### Easing
 **Namespace:** `Dooms.Math.Easing`
 Various options for processing 0-1 values using easing functions.
 
 **Available Easing Interpolation Mutators:**
  - sine, circ, bounce, expo, back, elastic, cubic, quad, quint, quart
  
 **Available Direction Mutators:**
  - In, Out, Both

 **Available Functions:**
  - Easing.Ease(float time, Interpolation interpolation, Direction direction)
  - prefix + suffix
  - *Prefixes:*
   - EaseIn, EaseOut, EaseInOut
  - *Suffixes:*
   - Sine, Circ, Bounce, Expo, Back, Elastic, Cubic, Quad, Quint, Quart
---

### Animation Data Scriptable Object
**Namespace:** `Drennen.Animation`

  - Can only be created via *Right-click > Create > Animations* in the Unity Project window.
  - Must assign the ScriptableObject (SO) in the Inspector.
  - *Animated Objects List* on the **TimeAnimation** component must:
    - Not be null
	- Match the object count defined in the ScriptableObject inspector window.
	- Contain objects matching names defined in the TA component (improvements WIP).
	
### Notes
 - Keep consistent in-out time values. 
	  - Example: *anim set 1 has `out` value of 0.3* -- *anim set 2 `in` value should then be >= 0.3*

> **Crossfading** is not yet implemented.
