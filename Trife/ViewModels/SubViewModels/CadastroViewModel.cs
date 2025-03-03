using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using DynamicData;
using ReactiveUI;
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
    public class CadastroViewModel : ReactiveObject
    {

        #region [Private Properties]

        private readonly SharedService sharedService;

        private Machine? _selectedMachine;
        private MachineIndicator? _selectedMachineSelectedMachineIndicators;
        private MachineIndicator? _selectedIndicator;

        private MachineIndicator? _selectedAmbientalIndicator;
        private MachineIndicator? _selectedSocialIndicator;
        private MachineIndicator? _selectedEconomicalIndicator;

        private string _newMachineName    = string.Empty;
        private bool _newMachineIsEnabled = true;

        private string _newMachineIndicatorName    = "";
        private bool _newMachineIndicatorIsEnabled = true;
        private Tipo? _newMachineIndicatorType;
        private string? _newMachineIndicatorUnit;

        private bool _stackMainControllerVisible = true;

        private bool _stackMachineControllerVisible = false;
        private bool _stackMachineCadastroVisible   = false;
        private bool _stackMachineEditVisible       = false;
        private bool _stackMachineDeleteVisible     = false;

        private bool _stackMachineIndicatorControllerVisible = false;
        private bool _stackMachineIndicatorCadastroVisible   = false;
        private bool _stackMachineIndicatorEditVisible       = false;
        private bool _stackMachineIndicatorDeleteVisible     = false;

        private bool _canSelectMachineIndicators = true;

        private bool _machineNameNotAvailable          = false;
        private bool _machineIndicatorNameNotAvailable = false;

        #endregion

        #region [Public Properties]

        public string NewMachineName
        {
            get => _newMachineName;
            set
            {
                this.RaiseAndSetIfChanged(ref _newMachineName, value);

                if (value != "")
                {
                    CheckIfMachineNameIsAvailable();
                }
            }
        }

        public bool NewMachineIsEnabled
        {
            get => _newMachineIsEnabled;
            set => this.RaiseAndSetIfChanged(ref _newMachineIsEnabled, value);
        }

        public string NewMachineIndicatorName
        {
            get => _newMachineIndicatorName;
            set
            {
                this.RaiseAndSetIfChanged(ref _newMachineIndicatorName, value);

                if (value != "")
                {
                    CheckIfMachineIndicatorNameIsAvailable();
                }
            }
        }

        public bool NewMachineIndicatorIsEnabled
        {
            get => _newMachineIndicatorIsEnabled;
            set => this.RaiseAndSetIfChanged(ref _newMachineIndicatorIsEnabled, value);
        }

        public Tipo? NewMachineIndicatorType
        {
            get => _newMachineIndicatorType;
            set => this.RaiseAndSetIfChanged(ref _newMachineIndicatorType, value);
        }

        public string? NewMachineIndicatorUnit
        {
            get => _newMachineIndicatorUnit;
            set => this.RaiseAndSetIfChanged(ref _newMachineIndicatorUnit, value);
        }

        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMachine, value);

                if (value != null)
                {
                    NewMachineName      = value.Name;
                    NewMachineIsEnabled = value.IsEnabled;

                    SelectedMachineMachineIndicators.Clear();
                    SelectedMachineMachineIndicators.AddRange(value.MachineIndicators);
                }
            }
        }

        public MachineIndicator? SelectedMachineSelectedMachineIndicators
        {
            get => _selectedMachineSelectedMachineIndicators;
            set => this.RaiseAndSetIfChanged(ref _selectedMachineSelectedMachineIndicators, value);
        }

        public MachineIndicator? SelectedIndicator
        {
            get => _selectedIndicator;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedIndicator, value);

                if (value != null)
                {
                    NewMachineIndicatorName = value.Name;
                    NewMachineIndicatorUnit = value.Unit;
                    NewMachineIndicatorType = value.Type;
                }
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
                    SelectedIndicator           = value;
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
                    SelectedIndicator           = value;
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
                    SelectedIndicator          = value;
                    SelectedAmbientalIndicator = null;
                    SelectedSocialIndicator    = null;
                }
            }
        }

        #region [Visibility Properties]

        public bool StackMainControllerVisible
        {
            get => _stackMainControllerVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMainControllerVisible, value);
        }

        public bool StackMachineControllerVisible
        {
            get => _stackMachineControllerVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineControllerVisible, value);
        }

        public bool StackMachineCadastroVisible
        {
            get => _stackMachineCadastroVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineCadastroVisible, value);
        }

        public bool StackMachineEditVisible
        {
            get => _stackMachineEditVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineEditVisible, value);
        }

        public bool StackMachineDeleteVisible
        {
            get => _stackMachineDeleteVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineDeleteVisible, value);
        }

        public bool StackMachineIndicatorControllerVisible
        {
            get => _stackMachineIndicatorControllerVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineIndicatorControllerVisible, value);
        }

        public bool StackMachineIndicatorCadastroVisible
        {
            get => _stackMachineIndicatorCadastroVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineIndicatorCadastroVisible, value);
        }

        public bool StackMachineIndicatorEditVisible
        {
            get => _stackMachineIndicatorEditVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineIndicatorEditVisible, value);
        }

        public bool StackMachineIndicatorDeleteVisible
        {
            get => _stackMachineIndicatorDeleteVisible;
            set => this.RaiseAndSetIfChanged(ref _stackMachineIndicatorDeleteVisible, value);
        }

        #endregion

        public bool CanSelectMachineIndicators
        {
            get => _canSelectMachineIndicators;
            set => this.RaiseAndSetIfChanged(ref _canSelectMachineIndicators, value);
        }

        public bool MachineNameNotAvailable
        {
            get => _machineNameNotAvailable;
            set => this.RaiseAndSetIfChanged(ref _machineNameNotAvailable, value);
        }

        public bool MachineIndicatorNameNotAvailable
        {
            get => _machineIndicatorNameNotAvailable;
            set => this.RaiseAndSetIfChanged(ref _machineIndicatorNameNotAvailable, value);
        }

        #region [Collections and Commands]

        #region [Collections]

        public ObservableCollection<Machine> Machines => sharedService.Machines;
        public ObservableCollection<MachineIndicator> MachineIndicators => sharedService.MachineIndicators;
        public ObservableCollection<MachineIndicator> SelectedMachineMachineIndicators { get; } = [];
        public ObservableCollection<Tipo> Types { get; }
        public ObservableCollection<string> NewMachineNameSource { get; }
        public ObservableCollection<string> NewMachineIndicatorNameSource { get; }

        public ObservableCollection<MachineIndicator> AmbientalMachineIndicators { get; }
        public ObservableCollection<MachineIndicator> SocialMachineIndicators { get; }
        public ObservableCollection<MachineIndicator> EconomicalMachineIndicators { get; }

        #endregion

        #region [Commands]

        //Visibility Commands
        public ReactiveCommand<string, Unit> MachineSelectVisibleCommand { get; }
        public ReactiveCommand<string, Unit> MachineReturnVisibleCommand { get; }

        public ReactiveCommand<string, Unit> MachineIndicatorSelectVisibleCommand { get; }
        public ReactiveCommand<string, Unit> MachineIndicatorReturnVisibleCommand { get; }

        //Machine Commands
        public ReactiveCommand<Unit, Unit> CreateMachineCommand { get; }
        public ReactiveCommand<Unit, Unit> EditMachineCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteMachineCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearMachineCommand { get; }
        public ReactiveCommand<Unit, Unit> AddMachineIndicatorCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveMachineIndicatorCommand { get; }

        //Indicator Commands
        public ReactiveCommand<Unit, Unit> CreateMachineIndicatorCommand { get; }
        public ReactiveCommand<Unit, Unit> EditMachineIndicatorCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteMachineIndicatorCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearMachineIndicatorCommand { get; }

        #endregion

        #endregion

        #endregion

        #region [Constructors]

        [LastModified("10-12-2024")]
        public CadastroViewModel(SharedService sharedService)
        {
            this.sharedService  = sharedService;

            AmbientalMachineIndicators  = new ObservableCollection<MachineIndicator>(MachineIndicators.Where(x => x.Type == Tipo.Ambiental));
            SocialMachineIndicators     = new ObservableCollection<MachineIndicator>(MachineIndicators.Where(x => x.Type == Tipo.Social));
            EconomicalMachineIndicators = new ObservableCollection<MachineIndicator>(MachineIndicators.Where(x => x.Type == Tipo.Economico));

            Types = new ObservableCollection<Tipo>(Enum.GetValues(typeof(Tipo)).Cast<Tipo>());

            NewMachineNameSource          = new ObservableCollection<string>(Machines.Select(x => x.Name).ToList());
            NewMachineIndicatorNameSource = new ObservableCollection<string>(MachineIndicators.Select(x => x.Name).ToList());

            #region [Commands]

            MachineSelectVisibleCommand = ReactiveCommand.Create<string>(MachineSelectVisible);
            MachineReturnVisibleCommand = ReactiveCommand.Create<string>(MachineReturnVisible);

            MachineIndicatorSelectVisibleCommand = ReactiveCommand.Create<string>(MachineIndicatorSelectVisible);
            MachineIndicatorReturnVisibleCommand = ReactiveCommand.Create<string>(MachineIndicatorReturnVisible);

            CreateMachineCommand = ReactiveCommand.Create(CreateMachine);
            EditMachineCommand   = ReactiveCommand.Create(EditMachine);
            DeleteMachineCommand = ReactiveCommand.Create(DeleteMachine);
            ClearMachineCommand  = ReactiveCommand.Create(ClearMachine);

            AddMachineIndicatorCommand    = ReactiveCommand.Create(AddMachineIndicator);
            RemoveMachineIndicatorCommand = ReactiveCommand.Create(RemoveMachineIndicator);

            CreateMachineIndicatorCommand = ReactiveCommand.Create(CreateMachineIndicator);
            EditMachineIndicatorCommand   = ReactiveCommand.Create(EditMachineIndicator);
            DeleteMachineIndicatorCommand = ReactiveCommand.Create(DeleteMachineIndicator);
            ClearMachineIndicatorCommand  = ReactiveCommand.Create(ClearMachineIndicator);

            #endregion

            MachineIndicators.CollectionChanged += UpdateSubCollections;
        }

        #endregion

        #region [Private Functions]

        #region [Visibility Functions]

        #region [Machine Visibility]

        [LastModified("05-12-2024")]
        private void MachineSelectVisible(string id)
        {
            ResetMachineVisibility();

            switch (id)
            {
                case "MachineControllerVisible":
                    StackMachineControllerVisible = true;
                    break;

                case "MachineCadastroVisible":
                    StackMachineCadastroVisible = true;
                    break;

                case "MachineEditVisible":
                    StackMachineEditVisible = true;
                    break;

                case "MachineDeleteVisible":
                    StackMachineDeleteVisible = true;
                    break;
            }
        }

        [LastModified("05-12-2024")]
        private void MachineReturnVisible(string id)
        {
            ResetAll();
            ResetMachineVisibility();

            switch (id)
            {
                case "MachineControllerReturn":
                    StackMainControllerVisible = true;
                    break;

                case "MachineCadastroReturn":
                case "MachineEditReturn":
                case "MachineDeleteReturn":
                    StackMachineControllerVisible = true;
                    break;
            }
        }

        [LastModified("05-11-2024")]
        private void ResetMachineVisibility()
        {
            StackMainControllerVisible    = false;
            StackMachineControllerVisible = false;
            StackMachineCadastroVisible   = false;
            StackMachineEditVisible       = false;
            StackMachineDeleteVisible     = false;
        }

        #endregion

        #region [MachineIndicator Visibility]

        [LastModified("05-12-2024")]
        private void MachineIndicatorSelectVisible(string id)
        {
            ResetMachineIndicatorVisibility();

            switch (id)
            {
                case "MachineIndicatorControllerVisible":
                    StackMachineIndicatorControllerVisible = true;
                    break;

                case "MachineIndicatorCadastroVisible":
                    StackMachineIndicatorCadastroVisible = true;
                    break;

                case "MachineIndicatorEditVisible":
                    StackMachineIndicatorEditVisible = true;
                    break;

                case "MachineIndicatorDeleteVisible":
                    StackMachineIndicatorDeleteVisible = true;
                    break;
            }
        }

        [LastModified("05-12-2024")]
        private void MachineIndicatorReturnVisible(string id)
        {
            ResetAll();
            ResetMachineIndicatorVisibility();

            switch (id)
            {
                case "MachineIndicatorControllerReturn":
                    StackMainControllerVisible = true;
                    break;

                case "MachineIndicatorCadastroReturn":
                case "MachineIndicatorEditReturn":
                case "MachineIndicatorDeleteReturn":
                    StackMachineIndicatorControllerVisible = true;
                    break;
            }
        }

        [LastModified("05-11-2024")]
        private void ResetMachineIndicatorVisibility()
        {
            StackMainControllerVisible             = false;
            StackMachineIndicatorControllerVisible = false;
            StackMachineIndicatorCadastroVisible   = false;
            StackMachineIndicatorEditVisible       = false;
            StackMachineIndicatorDeleteVisible     = false;
        }

        #endregion

        #endregion

        #region [Machine Functions]

        [LastModified("28-11-2024")]
        private void CheckIfMachineNameIsAvailable()
        {
            if (NewMachineNameSource.Contains(NewMachineName.Trim(), comparer: StringComparer.OrdinalIgnoreCase))
            {
                MachineNameNotAvailable = true;
            }
            else
            {
                MachineNameNotAvailable = false;
            }
        }

        [LastModified("10-02-2025")]
        private void CreateMachine()
        {
            if (NewMachineName != "")
            {
                if (!MachineNameNotAvailable)
                {
                    var indicators = SelectedMachineMachineIndicators.ToList();

                    var machine = new Machine
                    {
                        Name              = NewMachineName.Trim(),
                        MachineIndicators = indicators,
                        IsEnabled         = NewMachineIsEnabled
                    };

                    var inserted = LocalStorage.SaveMachine(machine);

                    if (inserted)
                    {
                        sharedService.Machines.Add(machine);

                        NewMachineNameSource.Clear();
                        NewMachineNameSource.AddRange(Machines.Select(x => x.Name));

                        ResetCreateMachine();
                        CheckIfMachineNameIsAvailable();

                        sharedService.Message = "Maquina criada com sucesso!";
                    }
                }
                else
                {
                    sharedService.Message = "Maquina já existe!";
                }
            }
            else
            {
                sharedService.Message = "Nome não pode ser vazio!";
            }
        }

        [LastModified("05-11-2024")]
        private void ResetCreateMachine()
        {
            NewMachineName              = string.Empty;
            NewMachineIsEnabled         = true;
            SelectedAmbientalIndicator  = null;
            SelectedSocialIndicator     = null;
            SelectedEconomicalIndicator = null;
            SelectedMachineMachineIndicators.Clear();
        }

        [LastModified("10-02-2025")]
        private void EditMachine()
        {
            if (SelectedMachine != null && NewMachineName != "")
            {
                SelectedMachine.Name              = NewMachineName;
                SelectedMachine.IsEnabled         = NewMachineIsEnabled;
                SelectedMachine.MachineIndicators = SelectedMachineMachineIndicators.ToList();
                SelectedMachine.TimeStamp         = DateTime.Now;

                var updated = LocalStorage.UpdateMachine(SelectedMachine);

                if (updated)
                {
                    ResetEditMachine();

                    sharedService.Message = "Maquina editada com sucesso!";
                }
            }
            else
            {
                sharedService.Message = "Selecione uma maquina!";
            }
        }

        [LastModified("06-11-2024")]
        private void ResetEditMachine()
        {
            SelectedMachine             = null;
            NewMachineName              = string.Empty;
            NewMachineIsEnabled         = true;
            SelectedIndicator           = null;
            SelectedAmbientalIndicator  = null;
            SelectedSocialIndicator     = null;
            SelectedEconomicalIndicator = null;
            SelectedMachineMachineIndicators.Clear();
        }

        [LastModified("02-12-2024")]
        private void DeleteMachine()
        {
            if (SelectedMachine != null)
            {
                var deleted = LocalStorage.DeleteMachine(SelectedMachine);

                if (deleted)
                {
                    sharedService.Machines.Remove(SelectedMachine);

                    ResetDeleteMachine();

                    sharedService.Message = "Maquina deletada com sucesso!";
                }
            }
            else
            {
                sharedService.Message = "Selecione uma maquina para deletar!";
            }
        }

        [LastModified("05-11-2024")]
        private void ResetDeleteMachine()
        {
            SelectedMachine = null;
        }

        [LastModified("05-11-2024")]
        private void AddMachineIndicator()
        {
            if (SelectedIndicator != null)
            {
                if (!SelectedMachineMachineIndicators.Contains(SelectedIndicator))
                {
                    SelectedMachineMachineIndicators.Add(SelectedIndicator);

                    ResetAddMachineIndicators();
                }
                else
                {
                    sharedService.Message = "Indicador já adicionado!";
                }
            }
        }

        [LastModified("06-11-2024")]
        private void ResetAddMachineIndicators()
        {
            SelectedIndicator           = null;
            SelectedAmbientalIndicator  = null;
            SelectedSocialIndicator     = null;
            SelectedEconomicalIndicator = null;
        }

        [LastModified("05-11-2024")]
        private void RemoveMachineIndicator()
        {
            if (SelectedMachineSelectedMachineIndicators != null)
            {
                if (SelectedMachineMachineIndicators.Contains(SelectedMachineSelectedMachineIndicators))
                {
                    SelectedMachineMachineIndicators.Remove(SelectedMachineSelectedMachineIndicators);

                    ResetRemoveMachineIndicators();
                }
            }
        }

        [LastModified("05-11-2024")]
        private void ResetRemoveMachineIndicators()
        {
            SelectedMachineSelectedMachineIndicators = null;
        }

        [LastModified("05-11-2024")]
        private void ClearMachine()
        {
            NewMachineName              = string.Empty;
            NewMachineIsEnabled         = true;
            SelectedMachine             = null;
            SelectedAmbientalIndicator  = null;
            SelectedSocialIndicator     = null;
            SelectedEconomicalIndicator = null;
            SelectedMachineMachineIndicators.Clear();
        }

        #endregion

        #region [Indicators Functions]

        [LastModified("02-01-2025")]
        private void CheckIfMachineIndicatorNameIsAvailable()
        {
            if (NewMachineIndicatorNameSource.Contains(NewMachineIndicatorName.Trim(), comparer: StringComparer.OrdinalIgnoreCase))
            {
                MachineIndicatorNameNotAvailable = true;
            }
            else
            {
                MachineIndicatorNameNotAvailable = false;
            }
        }

        [LastModified("28-11-2024")]
        private void UpdateSubCollections(object? sender, NotifyCollectionChangedEventArgs e)
        {
            AmbientalMachineIndicators.Clear();
            SocialMachineIndicators.Clear();
            EconomicalMachineIndicators.Clear();

            AmbientalMachineIndicators.AddRange(MachineIndicators.Where(x => x.Type == Tipo.Ambiental));
            SocialMachineIndicators.AddRange(MachineIndicators.Where(x => x.Type == Tipo.Social));
            EconomicalMachineIndicators.AddRange(MachineIndicators.Where(x => x.Type == Tipo.Economico));
        }

        [LastModified("06-11-2024")]
        private void RefreshMachineIndicators()
        {
            var temp = new ObservableCollection<MachineIndicator>(MachineIndicators.ToList());
            MachineIndicators.Clear();

            MachineIndicators.AddRange(temp);
        }

        [LastModified("10-02-2025")]
        private void CreateMachineIndicator()
        {
            var errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(NewMachineIndicatorName))
            {
                errorMessages.Add("Nome inválido!");
            }

            if (NewMachineIndicatorType == null)
            {
                errorMessages.Add("Selecione um tipo!");
            }

            if (NewMachineIndicatorUnit == null)
            {
                errorMessages.Add("Unidade inválida!");
            }

            if (MachineIndicatorNameNotAvailable)
            {
                errorMessages.Add("Nome já existe");
            }

            if (errorMessages.Count > 0)
            {
                sharedService.ErrorMessage = string.Join(" ", errorMessages);
                ResetCreateMachineIndicator();

                return;
            }

            var indicator = new MachineIndicator
            {
                Name      = NewMachineIndicatorName,
                IsEnabled = NewMachineIndicatorIsEnabled,
                Unit      = NewMachineIndicatorUnit!,
                Type      = (Tipo)NewMachineIndicatorType!,
                Deleted   = false
            };

            var inserted = LocalStorage.SaveMachineIndicator(indicator);

            if (inserted)
            {
                MachineIndicators.Add(indicator);
                ResetCreateMachineIndicator();
                CheckIfMachineIndicatorNameIsAvailable();

                sharedService.Message = "Indicador criado com sucesso!";
            }
        }

        [LastModified("06-11-2024")]
        private void ResetCreateMachineIndicator()
        {
            NewMachineIndicatorName      = string.Empty;
            NewMachineIndicatorIsEnabled = true;
            NewMachineIndicatorUnit      = null;
            NewMachineIndicatorType      = null;
        }

        [LastModified("10-02-2025")]
        private void EditMachineIndicator()
        {
            if (SelectedIndicator != null && NewMachineIndicatorName != "")
            {
                SelectedIndicator.Name      = NewMachineIndicatorName;
                SelectedIndicator.Unit      = NewMachineIndicatorUnit!;
                SelectedIndicator.Type      = (Tipo)NewMachineIndicatorType!;
                SelectedIndicator.IsEnabled = NewMachineIsEnabled;
                SelectedIndicator.TimeStamp = DateTime.Now;

                var updated = LocalStorage.UpdateMachineIndicator(SelectedIndicator);

                if (updated)
                {
                    ResetEditMachineIndicators();

                    sharedService.Message = "Indicador editado com sucesso!";
                }
            }
            else
            {
                sharedService.ErrorMessage = "Selecione um indicador!";
            }
        }

        [LastModified("06-11-2024")]
        private void ResetEditMachineIndicators()
        {
            SelectedIndicator            = null;
            NewMachineIndicatorName      = string.Empty;
            NewMachineIndicatorUnit      = null;
            NewMachineIndicatorType      = null;
            NewMachineIndicatorIsEnabled = true;
        }

        [LastModified("02-12-2024")]
        private void DeleteMachineIndicator()
        {
            if (SelectedIndicator != null)
            {
                SelectedIndicator.Deleted = true;

                var deleted = LocalStorage.DeleteMachineIndicator(SelectedIndicator);

                if (deleted)
                {
                    MachineIndicators.Remove(SelectedIndicator);

                    ResetDeleteMachineIndicator();

                    sharedService.Message = "Indicador deletado com sucesso!";
                }
            }
            else
            {
                sharedService.ErrorMessage = "Selecione um indicador!";
            }
        }

        [LastModified("05-11-2024")]
        private void ResetDeleteMachineIndicator()
        {
            SelectedIndicator = null;
        }

        [LastModified("05-11-2024")]
        private void ClearMachineIndicator()
        {
            SelectedIndicator       = null;
            NewMachineIndicatorName = string.Empty;
            NewMachineIndicatorUnit = string.Empty;
            NewMachineIsEnabled     = true;
            NewMachineIndicatorType = null;
        }

        #endregion

        #region [View Resets]

        [LastModified("06-11-2024")]
        private void ResetAll()
        {
            ResetMachineVisibility();
            ResetMachineIndicatorVisibility();

            //Reset Machine
            NewMachineName      = string.Empty;
            NewMachineIsEnabled = true;
            SelectedMachine     = null;

            //Reset Machine Indicators
            NewMachineIndicatorName = string.Empty;
            NewMachineIndicatorUnit = string.Empty;
            NewMachineIndicatorType = null;
            NewMachineIsEnabled     = true;
            SelectedIndicator       = null;

            //Reset Machine Machine Indicators
            SelectedAmbientalIndicator  = null;
            SelectedSocialIndicator     = null;
            SelectedEconomicalIndicator = null;
            SelectedMachineMachineIndicators.Clear();

            StackMainControllerVisible = true;
        }

        #endregion

        #endregion

        #region [Public Functions]

        [LastModified("05-11-2024")]
        public void UnloadPage()
        {
            ResetAll();
        }

        #endregion

    }
}
