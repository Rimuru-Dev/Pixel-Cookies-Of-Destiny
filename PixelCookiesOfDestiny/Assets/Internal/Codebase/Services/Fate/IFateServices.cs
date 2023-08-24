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

using AbyssMoth.Internal.Codebase.Boot;

namespace AbyssMoth.Internal.Codebase.Services.Fate
{
    public interface IFateServices : IInitialialize
    {
        public string GetFate();
    }
}