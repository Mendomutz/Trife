// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0305:Simplificar a inicialização de coleção", Justification = "Set to the old toList() style", Scope = "member", Target = "~M:Trife.ViewModels.SubViewModels.CadastroViewModel.#ctor(Trife.Classes.Services.SharedService,ConnectionLibrary.Contexts.MachineContext)")]
[assembly: SuppressMessage("Style", "IDE0305:Simplificar a inicialização de coleção", Justification = "Set to the old toList() style", Scope = "member", Target = "~M:Trife.ViewModels.SubViewModels.CadastroViewModel.EditMachine")]
[assembly: SuppressMessage("Style", "IDE0305:Simplificar a inicialização de coleção", Justification = "Set to the old toList() style", Scope = "member", Target = "~M:Trife.ViewModels.SubViewModels.CadastroViewModel.RefreshMachineIndicators")]
[assembly: SuppressMessage("Style", "IDE0305:Simplificar a inicialização de coleção", Justification = "Set to the old toList() style", Scope = "member", Target = "~M:Trife.ViewModels.MainViewModel.#ctor")]
[assembly: SuppressMessage("Reliability", "CA2011:Evitar a recursão infinita", Justification = "Object is set to null, and the proceding verification breaks the loop", Scope = "member", Target = "~P:Trife.Classes.Services.SharedService.Message")]
[assembly: SuppressMessage("Reliability", "CA2011:Evitar a recursão infinita", Justification = "Object is set to null, and the proceding verification breaks the loop", Scope = "member", Target = "~P:Trife.Classes.Services.SharedService.ErrorMessage")]
[assembly: SuppressMessage("Style", "IDE0071:Simplificar interpolação", Justification = "<Pendente>", Scope = "member", Target = "~M:Trife.ViewModels.SubViewModels.AnaliseViewModel.GetSeries(ConnectionLibrary.Objects.Machine,ConnectionLibrary.Objects.MachineIndicator,System.Nullable{System.DateTime},System.Nullable{System.DateTime})~System.ValueTuple{System.Collections.Generic.IEnumerable{LiveChartsCore.ISeries},System.Collections.Generic.IEnumerable{LiveChartsCore.Kernel.Sketches.ICartesianAxis}}")]
[assembly: SuppressMessage("Style", "IDE0090:Usar 'new(...)'", Justification = "<Pendente>", Scope = "member", Target = "~M:Trife.ViewModels.SubViewModels.AnaliseViewModel.#ctor(Trife.Classes.Services.SharedService)")]
[assembly: SuppressMessage("Style", "IDE0028:Simplificar a inicialização de coleção", Justification = "<Pendente>", Scope = "member", Target = "~M:Trife.ViewModels.SubViewModels.AnaliseViewModel.#ctor(Trife.Classes.Services.SharedService)")]
