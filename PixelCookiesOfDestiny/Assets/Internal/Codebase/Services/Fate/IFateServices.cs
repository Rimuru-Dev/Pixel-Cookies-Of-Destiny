using AbyssMoth.Internal.Codebase.Boot;

namespace AbyssMoth.Internal.Codebase.Services.Fate
{
    public interface IFateServices : IInitialialize
    {
        public string GetFate();
    }
}