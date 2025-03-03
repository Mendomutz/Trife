using ConnectionLibrary.Objects.Attributes;
using ReactiveUI;

namespace ConnectionLibrary.Objects
{
    [LastModified("10-02-2025")]
    public sealed class MachineIndicator : ReactiveObject
    {

        #region [Private Properties]

        private Guid _id;
        private string _name = "";
        private string _unit = "";
        private Tipo _type;
        private bool _isEnabled = true;
        private bool _deleted;
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

        public string Unit
        {
            get => _unit;
            set => this.RaiseAndSetIfChanged(ref _unit, value);
        }

        public Tipo Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
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

        public MachineIndicator()
        {
            _id        = Guid.NewGuid();
            _timeStamp = DateTime.Now;
        }

        #endregion

    }
}
