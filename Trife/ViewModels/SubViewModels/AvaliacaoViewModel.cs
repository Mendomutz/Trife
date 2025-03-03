using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using Trife.Classes.Services;

namespace Trife.ViewModels.SubViewModels
{
    public class AvaliacaoViewModel : ReactiveObject
    {

        #region [Private Properties]

        private readonly SharedService sharedService;

        private Machine? _selectedMachine;
        private MachineIndicator? _selectedAmbientalIndicator;
        private MachineIndicator? _selectedSocialIndicator;
        private MachineIndicator? _selectedEconomicalIndicator;

        private bool _indicatorIsEnabled;
        private double? _indicatorValue;

        #endregion

        #region [Public Properties]

        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMachine, value);
                this.RaisePropertyChanged(nameof(AmbientalMachineIndicators));
                this.RaisePropertyChanged(nameof(SocialMachineIndicators));
                this.RaisePropertyChanged(nameof(EconomicalMachineIndicators));
            }
        }

        public MachineIndicator? SelectedAmbientalIndicator
        {
            get => _selectedAmbientalIndicator;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedAmbientalIndicator, value);

                if (value != null)
                {
                    IndicatorIsEnabled          = value.IsEnabled;
                    SelectedSocialIndicator     = null;
                    SelectedEconomicalIndicator = null;
                }
            }
        }

        public MachineIndicator? SelectedSocialIndicator
        {
            get => _selectedSocialIndicator;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSocialIndicator, value);

                if (value != null)
                {
                    IndicatorIsEnabled          = value.IsEnabled;
                    SelectedAmbientalIndicator  = null;
                    SelectedEconomicalIndicator = null;
                }
            }
        }

        public MachineIndicator? SelectedEconomicalIndicator
        {
            get => _selectedEconomicalIndicator;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedEconomicalIndicator, value);

                if (value != null)
                {
                    IndicatorIsEnabled         = value.IsEnabled;
                    SelectedAmbientalIndicator = null;
                    SelectedSocialIndicator    = null;
                }
            }
        }

        public bool IndicatorIsEnabled
        {
            get => _indicatorIsEnabled;
            set => this.RaiseAndSetIfChanged(ref _indicatorIsEnabled, value);
        }

        public double? IndicatorValue
        {
            get => _indicatorValue;
            set => this.RaiseAndSetIfChanged(ref _indicatorValue, value);
        }

        #region [Collections and Commands]

        #region [Collections]

        public ObservableCollection<Machine> Machines { get; } = [];
        
        public ObservableCollection<MachineIndicator> AmbientalMachineIndicators
        {
            get
            {
                if (SelectedMachine?.MachineIndicators != null)
                {
                    return new ObservableCollection<MachineIndicator>(SelectedMachine.MachineIndicators.Where(x => x.Type == Tipo.Ambiental));
                }
                else
                {
                    return [];
                }
            }
        }

        public ObservableCollection<MachineIndicator> SocialMachineIndicators
        {
            get
            {
                if (SelectedMachine?.MachineIndicators != null)
                {
                    return new ObservableCollection<MachineIndicator>(SelectedMachine.MachineIndicators.Where(x => x.Type == Tipo.Social));
                }
                else
                {
                    return [];
                }
            }
        }

        public ObservableCollection<MachineIndicator> EconomicalMachineIndicators
        {
            get
            {
                if (SelectedMachine?.MachineIndicators != null)
                {
                    return new ObservableCollection<MachineIndicator>(SelectedMachine.MachineIndicators.Where(x => x.Type == Tipo.Economico));
                }
                else
                {
                    return [];
                }
            }
        }

        #endregion

        #region [Commands]

        public ReactiveCommand<Unit, Unit> SaveIndicatorCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearIndicatorCommand { get; }

        #endregion

        #endregion

        #endregion

        #region [Constructors]

        [LastModified("10-12-2024")]
        public AvaliacaoViewModel(SharedService sharedService)
        {
            this.sharedService  = sharedService;

            Machines.AddRange(sharedService.Machines.Where(machine => machine.IsEnabled && !machine.Deleted));

            #region [Observable Updates]

            sharedService.Machines.CollectionChanged += OnMachinesUpdated;

            #endregion

            #region [Commands]

            SaveIndicatorCommand  = ReactiveCommand.Create(SaveIndicatorValue);
            ClearIndicatorCommand = ReactiveCommand.Create(ResetAll);

            #endregion

        }

        #endregion

        #region [Private Functions]

        [LastModified("06-11-2024")]
        private void OnMachinesUpdated(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Machines.Clear();

            Machines.AddRange(sharedService.Machines.Where(machine => machine.IsEnabled && !machine.Deleted));
        }

        [LastModified("10-12-2024")]
        private void SaveIndicatorValue()
        {
            var SelectedIndicator = SelectedAmbientalIndicator ?? SelectedSocialIndicator ?? SelectedEconomicalIndicator;

            if (SelectedMachine != null && SelectedIndicator != null)
            {
                try
                {
                    var machine   = SelectedMachine!;
                    var indicator = SelectedIndicator!;

                    var machineIndicatorValue = new MachineIndicatorValue
                    {
                        MachineId          = machine.Id,
                        MachineIndicatorId = indicator.Id,
                        Value              = IndicatorValue!.Value,
                        Date               = DateTime.Now,
                        IsEnabled          = IndicatorIsEnabled,
                        Deleted            = false,
                        TimeStamp          = DateTime.Now
                    };

                    LocalStorage.SaveMachineIndicatorValue(machineIndicatorValue);

                    sharedService.Message = "Valor do indicador inserido com sucesso!";

                    ClearSaveIndicatorValue();
                }
                catch (Exception e)
                {
                    ResetAll();

                    sharedService.ErrorMessage = e.Message;
                }
            }
            else
            {
                sharedService.ErrorMessage = "Selecione uma máquina e um indicador para salvar.";
            }
        }

        [LastModified("07-11-2024")]
        private void ClearSaveIndicatorValue()
        {
            IndicatorIsEnabled = true;
            IndicatorValue     = null;
        }

        [LastModified("10-12-2024")]
        private void ResetAll()
        {
            IndicatorValue              = null;
            IndicatorIsEnabled          = true;            
            SelectedMachine             = null;
            SelectedAmbientalIndicator  = null;
            SelectedSocialIndicator     = null;
            SelectedEconomicalIndicator = null;
        }

        #endregion

        #region [Public Functions]

        [LastModified("02-01-2025")]
        public void UnloadPage()
        {
            ResetAll();
        }

        #endregion

    }
}
