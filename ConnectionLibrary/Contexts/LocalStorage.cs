using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using DynamicData;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ConnectionLibrary.Contexts
{
    /// <summary>
    /// Classe que gerencia o armazenamento local de máquinas.
    /// Pasta e arquivo de armazenamento são criados automaticamente.
    /// O arquivo é sobrescrito a cada operação.
    /// </summary>
    public sealed class LocalStorage
    {

        #region [Private Properties]

        private static readonly string folderPath = GetFolderPath();
        private static readonly string filePath = GetFilePath();
        private static readonly JsonSerializerOptions options = new() { WriteIndented = true };

        #endregion

        #region [Public Functions]

        #region [Machine Functions]

        /// <summary>
        /// Retorna a lista de máquinas salvas no arquivo JSON.
        /// </summary>
        /// <returns>Retorna a lista de máquinas.</returns>
        [LastModified("10-02-2025")]
        public static List<Machine> GetMachines()
        {
            var machines     = new List<Machine>();
            var localStorage = GetLocalStorage();

            if (localStorage.Machines.Count != 0)
            {
                machines = localStorage.Machines.Where(w => w.Deleted == false).ToList();
            }

            return machines;
        }

        /// <summary>
        /// Salva uma máquina no arquivo JSON.
        /// </summary>
        /// <param name="machine">Máquina a ser salva.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("02-12-2024")]
        public static bool SaveMachine(Machine machine)
        {
            var localStorage = GetLocalStorage();

            localStorage.Machines.Add(machine);
            localStorage.Machines.Sort((a, b) => a.Id.CompareTo(b.Id));

            var json = JsonSerializer.Serialize(localStorage, options);

            File.WriteAllText(filePath, json);

            return true;
        }

        /// <summary>
        /// Atualiza uma máquina no arquivo JSON.
        /// </summary>
        /// <param name="machine">Máquina a ser atualizada.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("02-12-2024")]
        public static bool UpdateMachine(Machine machine)
        {
            var localStorage = GetLocalStorage();

            if (localStorage.Machines.Count != 0)
            {
                var selectedMachine = localStorage.Machines.FirstOrDefault(f => f.Id == machine.Id);

                if (selectedMachine != null)
                {
                    localStorage.Machines.Replace<Machine>(selectedMachine, machine);

                    var json = JsonSerializer.Serialize(localStorage, options);

                    File.WriteAllText(filePath, json);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deleta uma máquina do arquivo JSON.
        /// </summary>
        /// <param name="machine">Máquina a ser deletada.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("03-12-2024")]
        public static bool DeleteMachine(Machine machine)
        {
            var localStorage = GetLocalStorage();

            if (localStorage.Machines.Count != 0)
            {
                var selectedMachine = localStorage.Machines.FirstOrDefault(f => f.Id == machine.Id);

                if (selectedMachine != null)
                {
                    selectedMachine.Deleted = true;
                }
            }

            var json = JsonSerializer.Serialize(localStorage, options);

            File.WriteAllText(filePath, json);

            return true;
        }

        #endregion

        #region [MachineIndicators Functions]

        /// <summary>
        /// Retorna a lista de indicadores de máquinas salvas no arquivo JSON.
        /// </summary>
        /// <returns>Retorna a lista de indicadores de máquinas.</returns>
        [LastModified("10-02-2025")]
        public static List<MachineIndicator> GetMachineIndicators()
        {
            var machineIndicators = new List<MachineIndicator>();
            var localStorage      = GetLocalStorage();

            if (localStorage.MachineIndicator.Count != 0)
            {
                machineIndicators = localStorage.MachineIndicator;
            }

            return machineIndicators;
        }

        /// <summary>
        /// Salva um indicador de máquina no arquivo JSON.
        /// </summary>
        /// <param name="machineIndicator">Indicador de máquina a ser salvo.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("10-02-2025")]
        public static bool SaveMachineIndicator(MachineIndicator machineIndicator)
        {
            var localStorage = GetLocalStorage();

            localStorage.MachineIndicator.Add(machineIndicator);
            localStorage.MachineIndicator.Sort((a, b) => a.Id.CompareTo(b.Id));

            var json = JsonSerializer.Serialize(localStorage, options);

            File.WriteAllText(filePath, json);

            return true;
        }

        /// <summary>
        /// Atualiza um indicador de máquina no arquivo JSON.
        /// </summary>
        /// <param name="machineIndicator">Indicador de máquina a ser atualizado.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("02-12-2024")]
        public static bool UpdateMachineIndicator(MachineIndicator machineIndicator)
        {
            var localStorage = GetLocalStorage();

            if (localStorage.MachineIndicator.Count != 0)
            {
                var selectedMachineIndicator = localStorage.MachineIndicator.FirstOrDefault(f => f.Id == machineIndicator.Id);

                if (selectedMachineIndicator != null)
                {
                    localStorage.MachineIndicator.Replace<MachineIndicator>(selectedMachineIndicator, machineIndicator);

                    var json = JsonSerializer.Serialize(localStorage, options);

                    File.WriteAllText(filePath, json);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deleta um indicador de máquina do arquivo JSON.
        /// </summary>
        /// <param name="machineIndicator">Indicador de máquina a ser deletado.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("03-12-2024")]
        public static bool DeleteMachineIndicator(MachineIndicator machineIndicator)
        {
            var localStorage = GetLocalStorage();

            if (localStorage.MachineIndicator.Count != 0)
            {
                var selectedMachineIndicator = localStorage.MachineIndicator.FirstOrDefault(f => f.Id == machineIndicator.Id);

                if (selectedMachineIndicator != null)
                {
                    selectedMachineIndicator.Deleted = true;
                }
            }

            var json = JsonSerializer.Serialize(localStorage, options);

            File.WriteAllText(filePath, json);

            return true;
        }

        #endregion

        #region [MachineIndicatorValues Functions]

        /// <summary>
        /// Retorna a lista de valores de indicadores de máquinas salvas no arquivo JSON.
        /// </summary>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("10-02-2025")]
        public static List<MachineIndicatorValue> GetMachineIndicatorValues()
        {
            var machineIndicatorValues = new List<MachineIndicatorValue>();
            var localStorage           = GetLocalStorage();

            if (localStorage.MachineIndicatorValues.Count != 0)
            {
                machineIndicatorValues = localStorage.MachineIndicatorValues.Where(w => w.Deleted == false).ToList();
            }

            return machineIndicatorValues;
        }

        /// <summary>
        /// Retorna a lista de valores de indicadores da máquina salva no arquivo JSON.
        /// </summary>
        /// <param name="machineId">Id da máquina.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("10-02-2025")]
        public static List<MachineIndicatorValue> GetMachineIndicatorValuesByMachineId(Guid machineId)
        {
            var machineIndicatorValues = new List<MachineIndicatorValue>();
            var localStorage           = GetLocalStorage();

            if (localStorage.MachineIndicator.Count != 0)
            {
                machineIndicatorValues = localStorage.MachineIndicatorValues.Where(w => w.MachineId == machineId).ToList();
            }

            return machineIndicatorValues;
        }

        /// <summary>
        /// Retorna a lista de valores de indicadores com base no Id da máquina e no Id do indicador de máquina.
        /// </summary>
        /// <param name="machineId">Valor do Id da máquina.</param>
        /// <param name="machineIndicatorId">Valor do Id do indicador de máquina.</param>
        /// <returns>Rertorna a lista de valores de indicadores de máquinas.</returns>
        [LastModified("10-02-2025")]
        public static List<MachineIndicatorValue> GetMachineIndicatorValuesByMachineIdMachineIndicatorId(Guid machineId, Guid machineIndicatorId)
        {
            var machineIndicatorValues = new List<MachineIndicatorValue>();

            var localStorage = GetLocalStorage();

            if (localStorage.MachineIndicatorValues.Count != 0)
            {
                machineIndicatorValues = localStorage.MachineIndicatorValues
                    .Where(w => w.MachineId == machineId && w.MachineIndicatorId == machineIndicatorId && w.IsEnabled == true && w.Deleted == false).ToList();
            }

            return machineIndicatorValues;
        }

        /// <summary>
        /// Salva um valor de indicador de máquina no arquivo JSON.
        /// </summary>
        /// <param name="machineIndicatorValue">Valor de indicador de máquina a ser salvo.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("10-02-2025")]
        public static bool SaveMachineIndicatorValue(MachineIndicatorValue machineIndicatorValue)
        {
            var localStorage = GetLocalStorage();
            //var maxId = localStorage.MachineIndicatorValues.Count != 0 ? localStorage.MachineIndicatorValues.Max(m => m.Id) : 0;
            //machineIndicatorValue.Id = maxId + 1;

            localStorage.MachineIndicatorValues.Add(machineIndicatorValue);
            localStorage.MachineIndicatorValues.Sort((a, b) => a.Id.CompareTo(b.Id));

            var json = JsonSerializer.Serialize(localStorage, options);

            File.WriteAllText(filePath, json);

            return true;
        }

        /// <summary>
        /// Atualiza um valor de indicador de máquina no arquivo JSON.
        /// </summary>
        /// <param name="machineIndicatorValue">Valor de indicador de máquina a ser atualizado.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("10-12-2024")]
        public static bool UpdateMachineIndicatorValue(MachineIndicatorValue machineIndicatorValue)
        {
            var localStorage = GetLocalStorage();

            if (localStorage.MachineIndicatorValues.Count != 0)
            {
                var selectedMachineIndicatorValue = localStorage.MachineIndicatorValues.FirstOrDefault(f => f.Id == machineIndicatorValue.Id);

                if (selectedMachineIndicatorValue != null)
                {
                    localStorage.MachineIndicatorValues.Replace<MachineIndicatorValue>(selectedMachineIndicatorValue, machineIndicatorValue);

                    var json = JsonSerializer.Serialize(localStorage, options);

                    File.WriteAllText(filePath, json);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deleta um valor de indicador de máquina do arquivo JSON.
        /// </summary>
        /// <param name="machineIndicatorValue">Valor de indicador de máquina a ser deletado.</param>
        /// <returns>Retorna true se a operação for bem sucedida, caso contrário, retorna false.</returns>
        [LastModified("03-12-2024")]
        public static bool DeleteMachineIndicatorValue(MachineIndicatorValue machineIndicatorValue)
        {
            var localStorage = GetLocalStorage();

            if (localStorage.MachineIndicatorValues.Count != 0)
            {
                var selectedMachineIndicatorValues = localStorage.MachineIndicatorValues.FirstOrDefault(f => f.Id == machineIndicatorValue.Id);

                if (selectedMachineIndicatorValues != null)
                {
                    selectedMachineIndicatorValues.Deleted = true;
                }
            }

            var json = JsonSerializer.Serialize(localStorage, options);

            File.WriteAllText(filePath, json);

            return true;
        }

        #endregion

        #region [Check Functions]

        /// <summary>
        /// Verifica se os indicadores básicos estão salvos no arquivo JSON.
        /// </summary>
        /// <param name="machineIndicators">Lista de indicadores de máquinas.</param>
        /// <returns>Retonra a lista de indicadores de máquinas.</returns>
        [LastModified("10-02-2025")]
        public static ObservableCollection<MachineIndicator> CheckForBasicIndicators(List<MachineIndicator> machineIndicators)
        {
            var basicMachineIndicators = new List<MachineIndicator>
            {
                new() {
                    Name      = "Eficiência Energética",
                    Unit      = "%",
                    Type      = Tipo.Ambiental,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Consumo de Energia",
                    Unit      = "kWh",
                    Type      = Tipo.Ambiental,
                    IsEnabled = true               
                },
                new()
                {
                    Name      = "Fluído de Corte",
                    Unit      = "L",
                    Type      = Tipo.Ambiental,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Volume da Peça Bruta",
                    Unit      = "m³",
                    Type      = Tipo.Ambiental,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Água para Limpeza",
                    Unit      = "L",
                    Type      = Tipo.Ambiental,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Vida Útil da Ferramenta",
                    Unit      = "h",
                    Type      = Tipo.Ambiental,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Ruído",
                    Unit      = "dB",
                    Type      = Tipo.Social,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Número de Acidentes",
                    Unit      = "N°/h",
                    Type      = Tipo.Social,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Número Médio de Horas de Treinamento",
                    Unit      = "h",
                    Type      = Tipo.Social,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Custo da Mão de Obra",
                    Unit      = "R$/h",
                    Type      = Tipo.Economico,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Custo da Máquina",
                    Unit      = "R$/h",
                    Type      = Tipo.Economico,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Custo da Ferramenta",
                    Unit      = "R$/h",
                    Type      = Tipo.Economico,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Tempo de Fabricação",
                    Unit      = "min",
                    Type      = Tipo.Economico,
                    IsEnabled = true
                },
                new()
                {
                    Name      = "Custo de Fluido de Corte",
                    Unit      = "R$/L",
                    Type      = Tipo.Economico,
                    IsEnabled = true
                },
            };

            if (machineIndicators.Count > 0)
            {
                foreach (MachineIndicator indicator in basicMachineIndicators)
                {
                    if (!machineIndicators.Any(f => f.Name == indicator.Name))
                    {
                        SaveMachineIndicator(indicator);
                        machineIndicators.Add(indicator);
                    }
                }
            }
            else
            {
                foreach (MachineIndicator indicator in basicMachineIndicators)
                {
                    SaveMachineIndicator(indicator);
                }

                machineIndicators = GetMachineIndicators();
            }

            return new ObservableCollection<MachineIndicator>(machineIndicators);
        }

        #endregion

        #endregion

        #region [Private Functions]

        /// <summary>
        /// Retorna o caminho da pasta de armazenamento local.
        /// </summary>
        /// <returns>Retorna o caminho da pasta de armazenamento local.</returns>
        [LastModified("10-12-2024")]
        private static string GetFolderPath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Trife");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// Retorna o caminho do arquivo de armazenamento local.
        /// </summary>
        /// <returns>Retonra o caminho do arquivo de armazenamento local.</returns>
        [LastModified("10-02-2025")]
        private static string GetFilePath()
        {
            var path = Path.Combine(folderPath, "localStorage.json");

            if (!File.Exists(path))
            {
                using (File.Create(path)) { } // Força o encerramento do fluxo de forma imediata com 'using'
            }

            return path;
        }

        /// <summary>
        /// Retorna a lista de máquinas salvas no arquivo JSON.
        /// </summary>
        /// <returns>Retorna a lista de máquinas.</returns>
        [LastModified("10-02-2025")]
        private static LocalStorageObject GetLocalStorage()
        {
            var localStorage = new LocalStorageObject();
            var json         = File.ReadAllText(filePath);

            if (json.StartsWith('{'))
            {
                localStorage = JsonSerializer.Deserialize<LocalStorageObject>(json, options)!;
            }

            return localStorage;
        }

        #endregion

    }

    #region [Local Objects]

    public class LocalStorageObject
    {
        public List<Machine> Machines { get; set; } = [];
        public List<MachineIndicator> MachineIndicator { get; set; } = [];
        public List<MachineIndicatorValue> MachineIndicatorValues { get; set; } = [];
    }

    #endregion

}
