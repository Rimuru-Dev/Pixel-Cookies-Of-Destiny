// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
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