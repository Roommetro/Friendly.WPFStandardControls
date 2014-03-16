using Codeer.Friendly;

namespace RM.Friendly.WPFStandardControls3
{
    /// <summary>
    /// 
    /// </summary>
    public class WPFControlsBase : IAppVarOwner
    {
        public AppVar AppVar
        {
            get;
            private set;
        }

        protected WPFControlsBase(AppVar appVar)
        {
            AppVar = appVar;
        }
    }
}
