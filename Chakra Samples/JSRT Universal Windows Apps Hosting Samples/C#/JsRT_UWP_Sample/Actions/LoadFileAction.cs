using Monaco;
using Monaco.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace JsRT_UWP_Sample.Actions
{
    public class LoadFileAction : IActionDescriptor
    {
        public string ContextMenuGroupId => "10_custom";
        public float ContextMenuOrder => 1.5f;
        public string Id => "meta-load-file-action";
        public string KeybindingContext => null;
        public int[] Keybindings => new int[] { Monaco.KeyMod.CtrlCmd | Monaco.KeyCode.KEY_L };
        public string Label => "Load File...";
        public string Precondition => null;

        public async void Run(CodeEditor editor)
        {
            var ofd = new FileOpenPicker();
            ofd.FileTypeFilter.Add(".js");

            var file = await ofd.PickSingleFileAsync();

            if (file != null)
            {
                var text = await FileIO.ReadTextAsync(file);

                editor.Text = text;

                editor.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            }
        }
    }
}
