using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    /// <summary>
    /// コード生成
    /// </summary>
    public class WPFSelectorGenerator : GeneratorBase
    {
        Selector _control;

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            _control = (Selector)ControlObject;
            _control.SelectionChanged += SelectionChanged;
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            _control.SelectionChanged -= SelectionChanged;
        }

        /// <summary>
        /// 選択インデックス変更
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント内容</param>
        void SelectionChanged(object sender, EventArgs e)
        {
            AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
        }
    }
}
