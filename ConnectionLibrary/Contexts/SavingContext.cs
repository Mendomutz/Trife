using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using Microsoft.Extensions.Configuration;

namespace ConnectionLibrary.Contexts
{
    public sealed class SavingContext
    {

        #region [Private Properties]

        private readonly bool isConnected;
        private readonly MachineContext machineContext;

        #endregion

        #region [Public Properties]

        public string ErrorMessage { get; set; } = "";

        #endregion

        #region [Constructors]

        [LastModified("10-02-2025")]
        public SavingContext(IConfiguration configuration)
        {
            machineContext = new MachineContext(configuration);

            isConnected = IsConnected();
        }

        #endregion

        #region [Public Functions]

        [LastModified("10-02-2025")]
        public bool IsConnected()
        {
            return machineContext.ConnectionStablished();
        }

        [LastModified("10-02-2025")]
        public bool SyncData()
        {
            if (!isConnected)
            {
                return false;
            }

            var result = false;

            var dataMachines               = machineContext.GetMachines();
            var dataMachineIndicators      = machineContext.GetMachineIndicators();
            var dataMachineIndicatorValues = machineContext.GetMachineIndicatorValues();

            var localMachines               = LocalStorage.GetMachines();
            var localMachineIndicators      = LocalStorage.GetMachineIndicators();
            var localMachineIndicatorValues = LocalStorage.GetMachineIndicatorValues();

            try
            {
                var machinesSynced               = SyncMachines(dataMachines, localMachines);
                var machineIndicatorsSynced      = SyncMachineIndicators(dataMachineIndicators, localMachineIndicators);
                var machineIndicatorValuesSynced = SyncMachineIndicatorValues(dataMachineIndicatorValues, localMachineIndicatorValues);

                result = machinesSynced && machineIndicatorsSynced && machineIndicatorValuesSynced;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public bool SyncMachines(List<Machine> dbMachines, List<Machine> localMachines)
        {
            if (dbMachines.Count != 0) // Machines in db
            {
                foreach (var dbMachine in dbMachines)
                {
                    var localMachine = localMachines.FirstOrDefault(x => x.Id == dbMachine.Id);

                    if (localMachine != null) // Machine exists in both db and local
                    {
                        if (localMachine.TimeStamp > dbMachine.TimeStamp) // Local is newer
                        {
                            machineContext.UpdateMachine(localMachine);
                        }
                        else if (localMachine.TimeStamp < dbMachine.TimeStamp) // DB is newer
                        {
                            LocalStorage.UpdateMachine(dbMachine);
                        }
                    }
                    else // Machine exists only in db
                    {
                        LocalStorage.SaveMachine(dbMachine);
                    }
                }
            }
            else // No machines in db
            {
                if (localMachines.Count != 0)
                {
                    foreach (var localMachine in localMachines)
                    {
                        machineContext.SaveMachine(localMachine);
                    }
                }
                else // No machines in db and local
                {
                    return false;
                }
            }

            return true;
        }

        [LastModified("10-02-2025")]
        public bool SyncMachineIndicators(List<MachineIndicator> dbMachineIndicators, List<MachineIndicator> localMachineIndicators)
        {
            if (dbMachineIndicators.Count != 0) // Machine indicators in db
            {
                foreach (var dbMachineIndicator in dbMachineIndicators)
                {
                    var localMachine = localMachineIndicators.FirstOrDefault(x => x.Id == dbMachineIndicator.Id);

                    if (localMachine != null) // Machine indicators exists in both db and local
                    {
                        if (localMachine.TimeStamp > dbMachineIndicator.TimeStamp) // Local is newer
                        {
                            machineContext.UpdateMachineIndicator(localMachine);
                        }
                        else if (localMachine.TimeStamp < dbMachineIndicator.TimeStamp) // DB is newer
                        {
                            LocalStorage.UpdateMachineIndicator(dbMachineIndicator);
                        }
                    }
                    else // Machine indicators exists only in db
                    {
                        LocalStorage.SaveMachineIndicator(dbMachineIndicator);
                    }
                }
            }
            else // No machine indicators in db
            {
                if (localMachineIndicators.Count != 0)
                {
                    foreach (var localMachineIndicator in localMachineIndicators)
                    {
                        machineContext.SaveMachineIndicator(localMachineIndicator);
                    }
                }
                else // No machine indicators in db and local
                {
                    return false;
                }
            }

            return true;
        }

        [LastModified("10-02-2025")]
        public bool SyncMachineIndicatorValues(List<MachineIndicatorValue> dbMachineIndicatorValues, List<MachineIndicatorValue> localMachineIndicatorValues)
        {
            if (dbMachineIndicatorValues.Count != 0) // Machine indicator values in db
            {
                foreach (var dbMachineIndicatorValue in dbMachineIndicatorValues)
                {
                    var localMachine = localMachineIndicatorValues.FirstOrDefault(x => x.Id == dbMachineIndicatorValue.Id);

                    if (localMachine != null) // Machine indicator values exists in both db and local
                    {
                        if (localMachine.TimeStamp > dbMachineIndicatorValue.TimeStamp) // Local is newer
                        {
                            machineContext.UpdateMachineIndicatorValue(localMachine);
                        }
                        else if (localMachine.TimeStamp < dbMachineIndicatorValue.TimeStamp) // DB is newer
                        {
                            LocalStorage.UpdateMachineIndicatorValue(dbMachineIndicatorValue);
                        }
                    }
                    else // Machine indicator values exists only in db
                    {
                        LocalStorage.SaveMachineIndicatorValue(dbMachineIndicatorValue);
                    }
                }
            }
            else // No machine indicator values in db
            {
                if (localMachineIndicatorValues.Count != 0)
                {
                    foreach (var localMachineIndicatorValue in localMachineIndicatorValues)
                    {
                        machineContext.SaveMachineIndicatorValue(localMachineIndicatorValue);
                    }
                }
                else // No machine indicator values in db and local
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

    }
}
