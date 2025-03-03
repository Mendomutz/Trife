using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Trife.Classes.Services;

namespace Trife.ViewModels.SubViewModels
{
    public class AnaliseViewModel : ReactiveObject
    {

        #region [Private Properties]

        private readonly SharedService sharedService;

        private Machine? _selectedMachine;
        private MachineIndicator? _selectedIndicator;

        private string? _selectedMachineName;
        private string? _selectedIndicatorName;

        private bool _machineIndicatorDropDownVisible;

        private bool _stackPanelEditPointVisible    = false;
        private bool _stackPanelNotEditPointVisible = true;
        private bool _editPointVisible              = true;

        private DateTime? _selecteDate;
        private IEnumerable<ISeries> _series = [];
        private List<Axis>? _xAxes;

        private MachineIndicatorValue? _point;

        #endregion

        #region [Public Properties]

        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMachine, value);

                MachineIndicatorDropDownVisible = false;

                if (value != null)
                {
                    Series                          = [];
                    SelectedMachineName             = value.Name;
                    SelectedIndicatorName           = null;
                    MachineIndicatorDropDownVisible = true;

                    MachineMachineIndicators.Clear();
                    MachineMachineIndicators.AddRange(value.MachineIndicators);
                }
            }
        }

        public MachineIndicator? SelectedIndicator
        {
            get => _selectedIndicator;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedIndicator, value);

                if (SelectedMachine != null)
                {
                    if (value != null)
                    {
                        SelectedDate = null;

                        var series = GetSeries(SelectedMachine, value);

                        SelectedMachineName   = SelectedMachine.Name;
                        SelectedIndicatorName = value.Name;

                        Series = series;
                    }
                }
            }
        }

        public string? SelectedMachineName
        {
            get => _selectedMachineName;
            set => this.RaiseAndSetIfChanged(ref _selectedMachineName, value);
        }

        public string? SelectedIndicatorName
        {
            get => _selectedIndicatorName;
            set => this.RaiseAndSetIfChanged(ref _selectedIndicatorName, value);
        }

        public bool MachineIndicatorDropDownVisible
        {
            get => _machineIndicatorDropDownVisible;
            set => this.RaiseAndSetIfChanged(ref _machineIndicatorDropDownVisible, value);
        }

        public bool StackPanelEditPointVisible
        {
            get => _stackPanelEditPointVisible;
            set => this.RaiseAndSetIfChanged(ref _stackPanelEditPointVisible, value);
        }

        public bool StackPanelNotEditPointVisible
        {
            get => _stackPanelNotEditPointVisible;
            set => this.RaiseAndSetIfChanged(ref _stackPanelNotEditPointVisible, value);
        }

        public bool EditPointVisible
        {
            get => _editPointVisible;
            set => this.RaiseAndSetIfChanged(ref _editPointVisible, value);
        }

        public DateTime? SelectedDate
        {
            get => _selecteDate;
            set
            {
                this.RaiseAndSetIfChanged(ref _selecteDate, value);

                if (value != null)
                {
                    if (SelectedIndicator != null)
                    {
                        var series = GetSeries(SelectedMachine!, SelectedIndicator!, value.Value, value.Value.AddDays(1));

                        Series = series;
                    }
                }
            }
        }

        public IEnumerable<ISeries> Series
        {
            get => _series;
            set => this.RaiseAndSetIfChanged(ref _series, value);
        }

        public List<Axis>? XAxes
        {
            get => _xAxes;
            set => this.RaiseAndSetIfChanged(ref _xAxes, value);
        }

        public MachineIndicatorValue? Point
        {
            get => _point;
            set => this.RaiseAndSetIfChanged(ref _point, value);
        }

        #region [Collections and Commands]

        //Collections
        public ObservableCollection<Machine> Machines { get; }
        public ObservableCollection<MachineIndicator> MachineIndicators { get; }
        public ObservableCollection<MachineIndicator> MachineMachineIndicators { get; }

        //Commands
        public ReactiveCommand<Unit, Unit> ClearCommand { get; }
        public ReactiveCommand<MachineIndicatorValue, Unit> SaveEditedPointCommand { get; }

        public ReactiveCommand<Unit, Unit> EditPointCommand { get; }
        public ReactiveCommand<Unit, Unit> VoltarEditedPointCommand { get; }

        #endregion

        #endregion

        #region [Constructors]

        [LastModified("11-02-2025")]
        public AnaliseViewModel(SharedService sharedService)
        {
            this.sharedService = sharedService;

            Machines                 = new ObservableCollection<Machine>(sharedService.Machines);
            MachineIndicators        = new ObservableCollection<MachineIndicator>(sharedService.MachineIndicators);
            MachineMachineIndicators = new ObservableCollection<MachineIndicator>(sharedService.MachineIndicators);

            XAxes = new List<Axis>
            {
                new Axis() {
                    LabelsRotation  = 0,
                    MinStep         = 1,
                    ForceStepToMin  = true,
                    SeparatorsPaint = new SolidColorPaint { Color = SKColors.Gray }
                }
            };

            #region [Commands]

            ClearCommand             = ReactiveCommand.Create(Clear);
            SaveEditedPointCommand   = ReactiveCommand.Create<MachineIndicatorValue>(SaveEditedPoint);
            EditPointCommand         = ReactiveCommand.Create(EditPoint);
            VoltarEditedPointCommand = ReactiveCommand.Create(VoltarEditedPoint);

            #endregion

            #region [Observable Updates]

            sharedService.Machines.CollectionChanged += OnMachinesUpdated;

            #endregion

        }

        #endregion

        #region [Private Functions]

        [LastModified("07-11-2024")]
        private void OnMachinesUpdated(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Machines.Clear();

            Machines.AddRange(sharedService.Machines.Where(machine => machine.IsEnabled && !machine.Deleted));
        }

        [LastModified("04-12-2024")]
        private void Clear()
        {
            ResetAll();
        }

        [LastModified("04-12-2024")]
        private void ResetAll()
        {
            SelectedMachine       = null;
            SelectedIndicator     = null;
            SelectedMachineName   = null;
            SelectedIndicatorName = null;
            SelectedDate          = null;
            Point                 = null;

            Series = [];

            MachineMachineIndicators.Clear();
            MachineMachineIndicators.Add(MachineIndicators);
        }

        [LastModified("10-02-2025")]
        private List<ISeries> GetSeries(Machine machine, MachineIndicator machineIndicator, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            var series = new List<ISeries>();

            /// Its possible to configure a global mapping for all series
            /// The location matters, configured here, everytime a <MachineIndicatorValue> object is instantiated it will use this configuration, but only in this scope
            /// For ease of understanding, its configured in each series
            //LiveCharts.Configure(config => config.HasMap<MachineIndicatorValue>((indicator, Index) => new(indicator.Date.ToOADate(), indicator.Value)));

            var indicatorValues = LocalStorage.GetMachineIndicatorValuesByMachineIdMachineIndicatorId(machine.Id, machineIndicator.Id);

            indicatorValues.Sort((a, b) => a.Date.CompareTo(b.Date));
            indicatorValues.RemoveAll(r => r.Date < initialDate || r.Date > finalDate);

            if (indicatorValues.Count != 0)
            {
                var lineSeries = new LineSeries<MachineIndicatorValue>
                {
                    DataPadding    = new LvcPoint(2, 2),
                    Values         = indicatorValues,
                    Fill           = null,
                    LineSmoothness = 0,
                    Mapping        = (indicator, Index) => new(Index + 1, indicator.Value)
                };

                lineSeries.ChartPointPointerHover += EditPointEvent;

                series.Add(lineSeries);
            }

            return series;
        }

        // TODO: Refatorar a função e verificar o funcionamento
        [LastModified("11-12-2024")]
        private void EditPointEvent(IChartView chart, ChartPoint<MachineIndicatorValue, CircleGeometry, LabelGeometry>? point)
        {
            if (point == null)
            {
                return;
            }

            Point = point?.Model ?? null;
        }

        [LastModified("04-12-2024")]
        private void SaveEditedPoint(MachineIndicatorValue machineIndicatorValue)
        {
            var edited = LocalStorage.UpdateMachineIndicatorValue(machineIndicatorValue);

            if (edited)
            {
                Point = null;
            }
        }

        [LastModified("11-12-2024")]
        private void EditPoint()
        {
            StackPanelEditPointVisible    = true;
            StackPanelNotEditPointVisible = false;
            EditPointVisible              = false;
        }

        [LastModified("11-12-2024")]
        private void VoltarEditedPoint()
        {
            StackPanelEditPointVisible    = false;
            StackPanelNotEditPointVisible = true;
            EditPointVisible              = true;
        }

        #endregion

        #region [Public Functions]

        [LastModified("07-11-2024")]
        public void UnloadPage()
        {
            ResetAll();
        }

        #endregion

    }
}
