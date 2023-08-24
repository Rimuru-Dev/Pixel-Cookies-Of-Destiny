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
using System.Collections;
using System.Linq;
using AbyssMoth.Internal.Codebase.Animation;
using AbyssMoth.Internal.Codebase.Services.Fate;
using AbyssMoth.Internal.Codebase.Services.RandomService;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Cookies
{
    public sealed class CookiesController : MonoBehaviour
    {
        private const float CooldownShowingPanel = 0.7f;

        public Action OnWin;

        [SerializeField] private Image[] images;
        [SerializeField] private float minScale = 0.8f;
        [SerializeField] private float maxScale = 1.2f;
        [SerializeField] private Text text;
        [SerializeField] private WindowAnimation windowAnimation;

        private IFateServices fateServices;
        private IRandomService randomService;
        private int remainingCookies;

        private void OnDestroy()
        {
            foreach (var image in images)
            {
                if (image.TryGetComponent(out Button button))
                    button.onClick.RemoveAllListeners();
            }
        }

        public void Initialization(IFateServices fateServices)
        {
            this.fateServices = fateServices;

            randomService = new RandomService();

            PrepareImages();

            windowAnimation.OnShowWindow += SetNewFateText;
        }

        private void PrepareImages()
        {
            foreach (var image in images.Where(image => image != null))
            {
                remainingCookies++;

                randomService.RandomGraphicRotation(image);
                randomService.RandomGraphicScale(image, minScale, maxScale);
                SubscribeOnImage(image);
            }
        }

        private void SubscribeOnImage(Component image)
        {
            if (image.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(delegate
                {
                    button.gameObject.SetActive(false);
                    windowAnimation.ShowWindow();
                    remainingCookies--;

                    if (remainingCookies <= 0)
                        StartCoroutine(Win());
                });
            }
        }

        private IEnumerator Win()
        {
            yield return new WaitForSeconds(CooldownShowingPanel);

            OnWin?.Invoke();
        }

        public void GetMoreCookies()
        {
            foreach (var image in images.Where(image => image != null))
            {
                image.gameObject.SetActive(true);

                remainingCookies++;

                randomService.RandomGraphicRotation(image);
                randomService.RandomGraphicScale(image, minScale, maxScale);
            }
        }

        private void SetNewFateText() =>
            text.text = fateServices.GetFate();
    }
}