using Codeer.Friendly;
using System;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Forms.DateTimePicker.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Forms.DateTimePickerに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(TypeFullName = "System.Windows.Controls.DatePicker")]
    public class WPFDatePicker : WPFControlBase4<DatePicker>
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="appVar">Application variable object for the control.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="appVar">アプリケーション内変数。</param>
#endif
        public WPFDatePicker(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Get current selected DateTime.
        /// </summary>
#else
        /// <summary>
        /// 現在日時を取得します。
        /// </summary>
#endif
        public DateTime? SelectedDate
        {
            get { return Getter<DateTime?>("SelectedDate"); }
        }

#if ENG
        /// <summary>
        /// Set current selected DateTime.
        /// </summary>
        /// <param name="datetime">DateTime.</param>
#else
        /// <summary>
        /// 現在日時を設定します。
        /// </summary>
        /// <param name="datetime">日時。</param>
#endif
        public void EmulateChangeDate(DateTime datetime)
        {
            InvokeStatic(EmulateChangeDate, datetime);
        }

#if ENG
        /// <summary>
        /// Set current selected DateTime.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="datetime">DateTime.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 現在日時を設定します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="datetime">日時。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeDate(DateTime datetime, Async async)
        {
            InvokeStatic(EmulateChangeDate, async, datetime);
        }
        static void EmulateChangeDate(DatePicker datepicker, DateTime datetime)
        {
            datepicker.Focus();
            datepicker.SelectedDate = datetime;
        }
    }
}
