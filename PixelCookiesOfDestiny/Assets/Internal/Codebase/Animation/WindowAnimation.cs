// ReSharper disable All
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//
// **************************************************************** //

using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Animation
{
    public sealed class WindowAnimation : MonoBehaviour
    {
        public Action OnShowWindow;
        public RectTransform windowRectTransform;
        public Button closeButton;
        public RectTransform startPosition;
        public RectTransform endPosition;
        public CanvasGroup canvasGroup;
        private Tween currentAnimation;

        private void Start()
        {
            closeButton.onClick.AddListener(HideWindow);

            windowRectTransform.SetParent(startPosition, false);
            windowRectTransform.localPosition = Vector3.zero;
            windowRectTransform.localScale = Vector3.zero;
            canvasGroup.alpha = 0f;
        }

        public void ShowWindow()
        {
            if (currentAnimation != null && currentAnimation.IsActive())
                currentAnimation.Kill();

            OnShowWindow?.Invoke();

            windowRectTransform.SetParent(endPosition, false);
            windowRectTransform.localPosition = Vector3.zero;
            windowRectTransform.localScale = Vector3.zero;

            currentAnimation = DOTween.Sequence()
                .Join(windowRectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, 0.5f));
        }

        private void HideWindow()
        {
            if (currentAnimation != null && currentAnimation.IsActive())
                currentAnimation.Kill();

            currentAnimation = DOTween.Sequence()
                .Join(windowRectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack))
                .Join(canvasGroup.DOFade(0f, 0.5f))
                .OnComplete(() =>
                {
                    windowRectTransform.SetParent(startPosition, false);
                    windowRectTransform.localPosition = Vector3.zero;
                    windowRectTransform.localScale = Vector3.zero;
                    canvasGroup.alpha = 0f;
                });
        }
    }
}