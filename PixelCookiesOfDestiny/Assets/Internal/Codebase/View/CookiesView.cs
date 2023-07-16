using System;
using System.Collections;
using System.Linq;
using AbyssMoth.Internal.Codebase.Animation;
using AbyssMoth.Internal.Codebase.Services.Fate;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AbyssMoth.Internal.Codebase.View
{
    public sealed class CookiesView : MonoBehaviour
    {
        public Image[] images;
        public float minScale = 0.8f;
        public float maxScale = 1.2f;
        public Action OnClick;
        public int remainingCookies;

        public GameObject getMoreCookiespanel;
        public Button getMoreCookiesButton;

        public WindowAnimation windowAnimation;
        public IFateServices fateServices;
        public Text text;

        private void OnDestroy()
        {
            foreach (var image in images)
            {
                if (image.TryGetComponent(out Button button))
                    button.onClick.RemoveAllListeners();
            }

            getMoreCookiesButton.onClick.RemoveListener(GetMoreCookies);
        }

        public void Initialization(IFateServices fateServices)
        {
            this.fateServices = fateServices;
            PrepareImages();
            windowAnimation.OnShowWindow += delegate { text.text = fateServices.GetFate(); };
        }

        private void PrepareImages()
        {
            foreach (var image in images.Where(image => image != null))
            {
                remainingCookies++;

                RandomRotation(image);
                RandomScale(image);
                SubscribeOnImage(image);
            }

            getMoreCookiespanel.SetActive(false);
            getMoreCookiesButton.onClick.AddListener(GetMoreCookies);
        }

        private void RandomRotation(Graphic image)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            image.rectTransform.rotation = randomRotation;
        }

        private void RandomScale(Graphic image)
        {
            var randomScale = Random.Range(minScale, maxScale);
            image.rectTransform.localScale = new Vector3(randomScale, randomScale, 1f);
        }

        private void SubscribeOnImage(Component image)
        {
            if (image.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(delegate
                {
                    OnClick?.Invoke();

                    button.gameObject.SetActive(false);
                    windowAnimation.ShowWindow();
                    remainingCookies--;

                    if (remainingCookies <= 0)
                        StartCoroutine(ShowGetMoreCookiesPanel());
                });
            }
        }

        private IEnumerator ShowGetMoreCookiesPanel()
        {
            yield return new WaitForSeconds(0.2f);
            getMoreCookiespanel.SetActive(true);
        }

        private void GetMoreCookies()
        {
            foreach (var image in images.Where(image => image != null))
            {
                image.gameObject.SetActive(true);

                remainingCookies++;

                RandomRotation(image);
                RandomScale(image);
            }

            getMoreCookiespanel.SetActive(false);
        }
    }
}