using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Trife.Classes.Services;
using Trife.ViewModels.SubViewModels;
using Trife.Views.SubViews;

namespace Trife.ViewModels;

public class MainViewModel : ReactiveObject
{

    #region [Private Properties]

    private readonly SharedService sharedService   = new();
    private readonly LibraryContext libraryContext = new();

    private readonly SavingContext? savingContext;
    private readonly CadastroViewModel? cadastroViewModel;
    private readonly AvaliacaoViewModel? avaliacaoViewModel;
    private readonly AnaliseViewModel? analiseViewModel;

    private string? _title = "Trife application";
    private string? _message;
    private string? _errorMessage;

    private bool _isCadastroSelected = true;
    private bool _isAvaliacaoSelected;
    private bool _isAnaliseSelected;

    private object? _selectedView;

    #endregion

    #region [Public Properties]

    public string? Title
    {
        get => _title!;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool IsCadastroSelected
    {
        get => _isCadastroSelected;
        set => this.RaiseAndSetIfChanged(ref _isCadastroSelected, value);
    }

    public bool IsAvaliacaoSelected
    {
        get => _isAvaliacaoSelected;
        set => this.RaiseAndSetIfChanged(ref _isAvaliacaoSelected, value);
    }

    public bool IsAnaliseSelected
    {
        get => _isAnaliseSelected;
        set => this.RaiseAndSetIfChanged(ref _isAnaliseSelected, value);
    }

    public object? SelectedView
    {
        get => _selectedView;
        set => this.RaiseAndSetIfChanged(ref _selectedView, value);
    }

    #endregion

    #region [Constructors]

    public MainViewModel()
    {
        try
        {
            savingContext = new SavingContext(libraryContext.Configuration);

            savingContext.SyncData();

            sharedService.ErrorMessage = savingContext.ErrorMessage;

            sharedService.Message = "Carregando configuração...";

            var machineList          = new ObservableCollection<Machine>() { LocalStorage.GetMachines() };
            var machineIndicatorList = new ObservableCollection<MachineIndicator>() { LocalStorage.GetMachineIndicators() };

            machineIndicatorList = LocalStorage.CheckForBasicIndicators(machineIndicatorList.ToList());

            sharedService.Machines          = machineList;
            sharedService.MachineIndicators = machineIndicatorList;

            cadastroViewModel  = new CadastroViewModel(sharedService);
            avaliacaoViewModel = new AvaliacaoViewModel(sharedService);
            analiseViewModel   = new AnaliseViewModel(sharedService);

            this.WhenAnyValue(x => x.IsCadastroSelected).Where(x => x).Subscribe(_ => SelectedView  = new ViewCadastro  { DataContext = cadastroViewModel });
            this.WhenAnyValue(x => x.IsAvaliacaoSelected).Where(x => x).Subscribe(_ => SelectedView = new ViewAnalise   { DataContext = analiseViewModel });
            this.WhenAnyValue(x => x.IsAnaliseSelected).Where(x => x).Subscribe(_ => SelectedView   = new ViewAvaliacao { DataContext = avaliacaoViewModel });

            this.WhenAnyValue(x => x.sharedService.ErrorMessage).Subscribe(errorMessage => ErrorMessage = errorMessage);
            this.WhenAnyValue(x => x.sharedService.Message).Subscribe(message => ErrorMessage = message);

            sharedService.Message = "Configuração carregada com sucesso!";
        }
        catch (Exception ex)
        {
            sharedService.ErrorMessage = ex.Message;
        }
    }

    #endregion

    [LastModified("10-02-2025")]
    public void UnloadPage()
    {
        savingContext?.SyncData();
    }
}
