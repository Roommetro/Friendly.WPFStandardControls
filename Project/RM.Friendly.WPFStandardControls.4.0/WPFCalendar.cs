using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.Friendly;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.Calendar.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.Calendarに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFCalendar : WPFControlBase4<Calendar>
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
        public WPFCalendar(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Gets selected date.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択日時です。 
        /// </summary>
#endif
        public DateTime? SelectedDate
        {
            get { return Getter<DateTime?>("SelectedDate"); }
        }

#if ENG
        /// <summary>
        /// Sets selected date.
        /// </summary>
        /// <param name="date">date.</param>
#else
        /// <summary>
        /// 現在の選択日付を設定します。
        /// </summary>
        /// <param name="date">日付。</param>
#endif
        public void EmulateChangeDate(DateTime? date)
        {
            InvokeStatic(EmulateChangeDate, date);
        }

#if ENG
        /// <summary>
        /// Sets selected date.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="date">date.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// 現在の選択日付を設定します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="date">日付。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeDate(DateTime? date, Async async)
        {
            InvokeStatic(EmulateChangeDate, async, date);
        }

        private static void EmulateChangeDate(Calendar calendar, DateTime? date)
        {
            calendar.SelectedDate = date;
        }
    }
}
