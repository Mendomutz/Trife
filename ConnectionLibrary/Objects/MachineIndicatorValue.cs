using ConnectionLibrary.Objects.Attributes;
using ReactiveUI;

namespace ConnectionLibrary.Objects
{
    [LastModified("18-02-2025")]
    public sealed class MachineIndicatorValue : ReactiveObject
    {

        #region [Private Properties]

        private Guid _id;
        private Guid _machineId;
        private Guid _machineIndicatorId;
        private double _value;
        private DateTime _date;
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

        public Guid MachineId
        {
            get => _machineId;
            set => this.RaiseAndSetIfChanged(ref _machineId, value);
        }

        public Guid MachineIndicatorId
        {
            get => _machineIndicatorId;
            set => this.RaiseAndSetIfChanged(ref _machineIndicatorId, value);
        }

        public double Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public DateTime Date
        {
            get => _date;
            set => this.RaiseAndSetIfChanged(ref _date, value);
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

        public MachineIndicatorValue()
        {
            _id        = Guid.NewGuid();
            _timeStamp = DateTime.Now;
        }

        #endregion

    }
}
