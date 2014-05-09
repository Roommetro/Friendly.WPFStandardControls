using Codeer.Friendly;
using Codeer.Friendly.Windows;
using RM.Friendly.WPFStandardControls.Inside;
using System;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on controls of type System.Windows.Controls.RichTextBox.
    /// </summary>
#else
    /// <summary>
    /// TypeがSystem.Windows.Controls.RichTextBoxに対応した操作を提供します。
    /// </summary>
#endif
    public class WPFRichTextBox : WPFControlBase<RichTextBox>
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
        public WPFRichTextBox(AppVar appVar)
            : base(appVar) { }

#if ENG
        /// <summary>
        /// Returns the control's text.
        /// </summary>
#else
        /// <summary>
        /// テキストを取得します。
        /// </summary>
#endif
        public string Text
        {
            get { return InvokeStatic(GetText, Ret<string>()); }
        }

#if ENG
        /// <summary>
        /// Clear the control's text.
        /// </summary>
#else
        /// <summary>
        /// テキストをクリアします。
        /// </summary>
#endif
        public void EmulateClearText()
        {
            InvokeStatic(EmulateClearText);
        }

#if ENG
        /// <summary>
        /// Clear the control's text.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// テキストをクリアします。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateClearText(Async async)
        {
            InvokeStatic(EmulateClearText, async);
        }

        private static void EmulateClearText(RichTextBox textBox)
        {
            textBox.Document.Blocks.Clear();
        }

#if ENG
        /// <summary>
        /// Append text.
        /// </summary>
        /// <param name="text">Text to use.</param>
#else
        /// <summary>
        /// テキストを追加します。
        /// </summary>
        /// <param name="text">テキスト。</param>
#endif
        public void EmulateAppendText(string text)
        {
            InvokeStatic(EmulateAppendText, text);
        }

#if ENG
        /// <summary>
        /// Append text.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="text">Text to use.</param>
        /// <param name="async">Asynchronous execution.</param>
#else
        /// <summary>
        /// テキストを追加します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="text">テキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateAppendText(string text, Async async)
        {
            InvokeStatic(EmulateAppendText, async, text);
        }

        static void EmulateAppendText(RichTextBox textBox, string text)
        {
            textBox.AppendText(text);
        }

        static string GetText(RichTextBox rich)
        {
            return RichTextBoxUtility.GetText(rich);
        }
    }
}
