// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Services.RandomService
{
    public interface IRandomService
    {
        public void RandomGraphicRotation(Graphic image);
        public void RandomGraphicScale(Graphic image, float minScale, float maxScale);
    }
}