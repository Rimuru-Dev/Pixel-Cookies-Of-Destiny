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

using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Services.RandomService
{
    public interface IRandomService
    {
        public void RandomGraphicRotation(Graphic image, float minRange = 0f, float maxRange = 360f);
        public void RandomGraphicScale(Graphic image, float minScale, float maxScale);
    }
}