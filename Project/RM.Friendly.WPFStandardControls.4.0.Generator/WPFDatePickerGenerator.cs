using System;
using System.Collections.Generic;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls.Generator
{
    /// <summary>
    /// コード生成
    /// </summary>
    public class WPFDatePickerGenerator : GeneratorBase
    {
        DatePicker _control;

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            _control = (DatePicker)ControlObject;
            _control.SelectedDateChanged += SelectedDateChanged;
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            _control.SelectedDateChanged -= SelectedDateChanged;
        }

        /// <summary>
        /// 選択日付変更
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント内容</param>
        void SelectedDateChanged(object sender, EventArgs e)
        {
            if (_control.IsFocused && _control.SelectedDate.Value != null)
            {
                DateTime day = _control.SelectedDate.Value;
                AddSentence(new TokenName(), ".EmulateChangeDate(new DateTime(", day.Year, ", ", day.Month, ", ", day.Day, ")", _control.SelectedDate, new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
