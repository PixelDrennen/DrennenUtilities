using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Dooms
{
    [ExecuteAlways]
    public class PPICalculator : MonoBehaviour
    {
        private Image img;
        private float ppi = 30;
        private float targetPPU = 60;
        private void Awake()
        {
            img = (GetComponent<Image>() == null) ? gameObject.AddComponent<Image>() : GetComponent<Image>();
        }
        private void Update()
        {
            if (img != null)
                if (img.type == Image.Type.Sliced)
                {
                    RectTransform rt = transform as RectTransform;
                    // Debug.Log(rt.rect.width);
                    img.pixelsPerUnitMultiplier = (rt.rect.width / ppi) + targetPPU;
                }
        }
    }
}