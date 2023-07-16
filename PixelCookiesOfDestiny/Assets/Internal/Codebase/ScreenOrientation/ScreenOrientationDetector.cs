// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using UnityEngine;
using UnityEngine.Events;

namespace AbyssMoth.Internal.Codebase
{
    public enum ScreenOrientation
    {
        Portrait,
        Landscape
    }

    public sealed class ScreenOrientationDetector : MonoBehaviour
    {
        public event Action<ScreenOrientation> onOrientationChange;

        private ScreenOrientation currentOrientation;

        private void Start() =>
            currentOrientation = GetCurrentOrientation();

        private void Update()
        {
            var newOrientation = GetCurrentOrientation();

            if (newOrientation != currentOrientation)
            {
                currentOrientation = newOrientation;
                onOrientationChange?.Invoke(currentOrientation);
            }
        }

        private ScreenOrientation GetCurrentOrientation() =>
            Screen.width > Screen.height ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;
    }
    
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