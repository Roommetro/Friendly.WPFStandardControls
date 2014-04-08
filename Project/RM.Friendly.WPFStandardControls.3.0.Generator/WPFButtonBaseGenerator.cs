using System;
using System.Collections.Generic;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls.Generator
{
    /// <summary>
    /// コード生成
    /// </summary>
    public class WPFButtonBaseGenerator : GeneratorBase
    {
        Button _control;

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            _control = (Button)ControlObject;
            _control.Click += ButtonClick;
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            _control.Click -= ButtonClick;
        }

        /// <summary>
        /// ボタン押下
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント内容</param>
        void ButtonClick(object sender, EventArgs e)
        {
            if (_control.IsFocused)
            {
                AddSentence(new TokenName(), ".EmulateClick(", new TokenAsync(CommaType.Non), ");");
            }
        }
    }
}
