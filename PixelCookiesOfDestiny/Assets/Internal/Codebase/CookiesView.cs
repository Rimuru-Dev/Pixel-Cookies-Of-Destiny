using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbyssMoth.Internal.Codebase;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AbyssMoth
{
    public class CookiesView : MonoBehaviour
    {
        public Image[] images;
        public float minScale = 0.8f;
        public float maxScale = 1.2f;
        public Action OnClick;
        public int remainingCookies;
        
        public WindowAnimation windowAnimation;
        public TextFileReader textFileReader;
        public Text text;
        
        private void Start()
        {
            PrepareImages();
            
            windowAnimation.OnShowWindow += delegate { text.text = textFileReader.GetFate(); };
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
        }

        private void RandomRotation(Image image)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            image.rectTransform.rotation = randomRotation;
        }

        private void RandomScale(Image image)
        {
            var randomScale = Random.Range(minScale, maxScale);
            image.rectTransform.localScale = new Vector3(randomScale, randomScale, 1f);
        }

        private void SubscribeOnImage(Image image)
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
                    {
                        Debug.Log("WIN");
                    }
                });
            }
        }
    }
}