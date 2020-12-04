using System;
using System.ComponentModel.Composition;
using System.Threading;
using EnvDTE;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;
using Task = System.Threading.Tasks.Task;

namespace StickyChristmas
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(StickyKeyHandler))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.PrimaryDocument)]
    public class StickyKeyHandler : ICommandHandler<TypeCharCommandArgs>
    {
        private CancellationTokenSource _source;
        private bool _isStuck;
        private DateTime _lastRun = DateTime.MinValue;

        public string DisplayName => nameof(StickyKeyHandler);

        public bool ExecuteCommand(TypeCharCommandArgs args, CommandExecutionContext executionContext)
        {
            if (ShouldRun())
            {
                _isStuck = true;
                _source = new CancellationTokenSource(5000);

                MakeKeyStickyAsync(_source.Token, args)
                    .FileAndForget(nameof(Vsix.Name));
            }
            else
            {
                if (_source?.IsCancellationRequested == false)
                {
                    _source?.Cancel();
                    _source?.Dispose();
                }

                _isStuck = false;
            }

            return false;
        }

        private async Task MakeKeyStickyAsync(CancellationToken token, TypeCharCommandArgs args)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var dte = await AsyncServiceProvider.GlobalProvider.GetServiceAsync<DTE, DTE>();

            try
            {
                dte.UndoContext.Open(Vsix.Name);

                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(20);
                    args.TextView.TextBuffer.Insert(args.TextView.Caret.Position.BufferPosition, args.TypedChar.ToString());
                }
            }
            finally
            {
                dte.UndoContext.Close();
                _lastRun = DateTime.Now;
            }
        }

        private bool ShouldRun()
        {
            return _isStuck == false &&
                   _lastRun < DateTime.Now.AddSeconds(-60) &&
                   GeneralOptions.Instance.Enabled;
        }

        public CommandState GetCommandState(TypeCharCommandArgs args)
        {
            return CommandState.Available;
        }
    }
}