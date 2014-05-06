using System;
using System.Collections.Generic;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Windows.Controls;

namespace RM.Friendly.WPFStandardControls.Generator
{
    /// <summary>
    /// コード生成
    /// </summary>
    public class WPFCalendarGenerator : GeneratorBase
    {
        Calendar _control;

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            _control = (Calendar)ControlObject;
            _control.SelectedDatesChanged += SelectedDatesChanged;
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            _control.SelectedDatesChanged -= SelectedDatesChanged;
        }

        /// <summary>
        /// 選択日付変更
        /// </summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント内容</param>
        void SelectedDatesChanged(object sender, EventArgs e)
        {
            if (_control.SelectedDate.Value != null)
            {
                DateTime day = _control.SelectedDate.Value;
                AddSentence(new TokenName(), ".EmulateChangeDate(new DateTime(", day.Year, ", ", day.Month, ", ", day.Day, ")", new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
