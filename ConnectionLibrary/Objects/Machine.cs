using ConnectionLibrary.Objects.Attributes;
using ReactiveUI;

namespace ConnectionLibrary.Objects
{
    [LastModified("18-02-2025")]
    public sealed class Machine : ReactiveObject
    {

        #region [Private Properties]

        private Guid _id;
        private string _name = "";
        private List<MachineIndicator> _machineIndicators = [];
        private bool _isEnabled = true;
        private bool _deleted = false;
        private DateTime _timeStamp;

        #endregion

        #region [Public Properties]

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public List<MachineIndicator> MachineIndicators
        {
            get => _machineIndicators;
            set => this.RaiseAndSetIfChanged(ref _machineIndicators, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => this.RaiseAndSetIfChanged(ref _isEnabled, value);
        }

        public bool Deleted
        {
            get => _deleted;
            set => this.RaiseAndSetIfChanged(ref _deleted, value);
        }

        public DateTime TimeStamp
        {
            get => _timeStamp;
            set => this.RaiseAndSetIfChanged(ref _timeStamp, value);
        }

        #endregion

        #region [Constructors]

        public Machine()
        {
            _id        = Guid.NewGuid();
            _timeStamp = DateTime.Now;
        }

        #endregion

    }
}
