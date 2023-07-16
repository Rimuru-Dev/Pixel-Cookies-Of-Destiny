// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
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
        public RectTransform windowRectTransform;
        public Button closeButton;
        public RectTransform startPosition; // Позиция начала анимации
        public RectTransform endPosition; // Позиция конца анимации
        public CanvasGroup canvasGroup;

        private Tween currentAnimation;

        public Action OnShowWindow, OnHideWindow;

        private void Start()
        {
            closeButton.onClick.AddListener(HideWindow);

            // Переместить окно в начальную позицию
            windowRectTransform.SetParent(startPosition, false);
            windowRectTransform.localPosition = Vector3.zero;
            windowRectTransform.localScale = Vector3.zero;
            canvasGroup.alpha = 0f;
        }

        [ContextMenu("ShowWindow")]
        public void ShowWindow()
        {
            if (currentAnimation != null && currentAnimation.IsActive())
            {
                currentAnimation.Kill();
            }

            OnShowWindow?.Invoke();

            windowRectTransform.SetParent(endPosition, false);
            windowRectTransform.localPosition = Vector3.zero;
            windowRectTransform.localScale = Vector3.zero;

            currentAnimation = DOTween.Sequence()
                .Join(windowRectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, 0.5f));
        }

        [ContextMenu("HideWindow")]
        private void HideWindow()
        {
            if (currentAnimation != null && currentAnimation.IsActive())
            {
                currentAnimation.Kill();
            }

            OnHideWindow?.Invoke();

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