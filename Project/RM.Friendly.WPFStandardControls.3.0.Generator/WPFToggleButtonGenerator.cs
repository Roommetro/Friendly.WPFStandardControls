using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows;

namespace Codeer.Friendly.WpfStandardControls.Generator
{
    /// <summary>
    /// コード生成
    /// </summary>
    public class WpfToggleButtonGenerator : GeneratorBase
    {
        ToggleButton _control;

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            _control = (ToggleButton)ControlObject;
            _control.Checked += ChangeCheck;
            _control.Unchecked += ChangeCheck;
            _control.Indeterminate += ChangeCheck;
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            _control.Checked -= ChangeCheck;
            _control.Unchecked -= ChangeCheck;
            _control.Indeterminate -= ChangeCheck;
        }

        /// <summary>
        /// チェック状態変化
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント内容</param>
        void ChangeCheck(object sender, RoutedEventArgs e)
        {
            if (_control.IsFocused)
            {
                string isChecked = "null";
                if (_control.IsChecked.HasValue)
                {
                    isChecked = _control.IsChecked.Value ? "true" : "false";
                }
                AddSentence(new TokenName(), ".EmulateCheck(" + isChecked, new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
