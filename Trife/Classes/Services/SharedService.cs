using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Trife.Classes.Services
{
    [LastModified("18-02-2025")]
    public sealed class SharedService : ReactiveObject
    {

        #region [Private Properties]

        private string? _message;
        private string? _errorMessage;

        #endregion

        #region [Public Properties]

        #region [Lists]

        public ObservableCollection<Machine> Machines { get; set; } = [];
        public ObservableCollection<MachineIndicator> MachineIndicators { get; set; } = [];

        #endregion

        public string? Message
        {
            get => _message!;
            set
            {
                this.RaiseAndSetIfChanged(ref _message, value);

                if (value != null)
                {
                    Task.Delay(5000).ContinueWith(_ =>
                    {
                        Message = null;
                    });
                }
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage!;
            set
            {
                this.RaiseAndSetIfChanged(ref _errorMessage, value);

                if (value != null)
                {
                    Task.Delay(5000).ContinueWith(_ =>
                    {
                        ErrorMessage = null;
                    });
                }
            }
        }

        #endregion

    }
}
