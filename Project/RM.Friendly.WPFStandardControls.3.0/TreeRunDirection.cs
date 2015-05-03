namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// VisualTree and LogicalTree utility.
    /// </summary>
#else
    /// <summary>
    /// VisualTreeとLogicalTreeのユーティリティー。
    /// </summary>
#endif
    public enum TreeRunDirection
    {
#if ENG
        /// <summary>
        /// Descendants.
        /// </summary>
#else
        /// <summary>
        /// 子孫方向。
        /// </summary>
#endif
        Descendants,

#if ENG
        /// <summary>
        /// Ancestors.
        /// </summary>
#else
        /// <summary>
        /// 先祖方向。
        /// </summary>
#endif
        Ancestors,
    }
}
