using System.Collections;
using System.Collections.Generic;
using AbyssMoth.Internal.Codebase;
using UnityEngine;
using ScreenOrientation = AbyssMoth.Internal.Codebase.ScreenOrientation;

namespace AbyssMoth
{
    public class MyScript : MonoBehaviour
    {
        public GameObject L;
        public GameObject P;
        public ScreenOrientationDetector screenOrientationManager;

        private void Awake()
        {
            screenOrientationManager.onOrientationChange += OnOrientationChange;
        }

        private void OnDestroy()
        {
            screenOrientationManager.onOrientationChange -= OnOrientationChange;
        }

        private void OnOrientationChange(ScreenOrientation newOrientation)
        {
            if (newOrientation == ScreenOrientation.Landscape)
            {
                L.SetActive(true);
                P.SetActive(false);
                // Включите канвас для режима ландшафта
            }
            else if (newOrientation == ScreenOrientation.Portrait)
            {
                P.SetActive(true);
                L.SetActive(false);
                // Включите канвас для режима портрета
            }
        }
    }
}