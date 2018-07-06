using JsRT_UWP_Sample.Actions;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace JsRT_UWP_Sample
{
    public sealed partial class MainPage : Page
    {
        ChakraHost.ChakraHost host = new ChakraHost.ChakraHost();

        public MainPage()
        {
            this.InitializeComponent();

            string msg = host.init();
            if (msg != "NoError")
            {
                JsOutput.Text = msg;
            }

            JsInput.Text = @"// Use Ctrl+Enter to execute code
// Press F1 to bring up commandbar or Ctrl+L to load a file.
";
            JsInput.Loaded += CodeEditor_Loaded;
        }

        private async void CodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            await JsInput.AddCommandAsync(Monaco.KeyMod.CtrlCmd | Monaco.KeyCode.Enter, Execute_Code);

            await JsInput.AddActionAsync(new LoadFileAction());
            
            JsInput.Focus(FocusState.Programmatic);
        }

        private void Execute_Code()
        {
            string result = host.runScript(JsInput.Text);
            JsOutput.Text = JsOutput.Text + "\n> " + JsInput.Text + "\n" + result;
            JsOutput.UpdateLayout();
            JsOutputScroll.ChangeView(null, double.MaxValue, null);
        }
    }
}

