// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Services.RandomService
{
    public sealed class RandomService : IRandomService
    {
        public void RandomGraphicRotation(Graphic image)
        {
            var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            image.rectTransform.rotation = randomRotation;
        }

        public void RandomGraphicScale(Graphic image, float minScale, float maxScale)
        {
            var randomScale = Random.Range(minScale, maxScale);
            image.rectTransform.localScale = new Vector3(randomScale, randomScale, 1f);
        }
    }
}