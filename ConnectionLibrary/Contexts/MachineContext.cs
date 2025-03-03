using ConnectionLibrary.Contexts.TableFunctions;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConnectionLibrary.Contexts
{
    public class MachineContext
    {

        #region [Private Properties]

        private readonly string? connectionString;
        private readonly SqlConnection connection;

        #endregion

        #region [Constructors]

        [LastModified("05-11-2024")]
        public MachineContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        #endregion

        #region [Public Functions]

        [LastModified("29-11-2024")]
        public bool ConnectionStablished()
        {
            int retryCount = 2;
            int attempts   = 0;

            while (true)
            {
                try
                {
                    connection.Open();

                    connection.Close();

                    return true;
                }
                catch
                {
                    connection.Close();
                    attempts++;

                    if (attempts > retryCount)
                    {
                        return false;
                    }
                }
            }
        }

        #region [Machine Functions]

        [LastModified("10-02-2025")]
        public List<Machine> GetMachines()
        {
            var machines = new List<Machine>();

            try
            {
                connection.Open();

                machines = MachineTable.GetMachines(connection);

                connection.Close();

            }
            catch
            {
                connection.Close();
                throw;
            }
            
            return machines;
        }

        [LastModified("10-02-2025")]
        public bool SaveMachine(Machine machine)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineTable.SaveMachine(connection, transaction, machine);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public bool UpdateMachine(Machine machine)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineTable.UpdateMachine(connection, transaction, machine);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        [LastModified("18-02-2025")]
        public bool DeleteMachine(Machine machine)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineTable.DeleteMachine(connection, transaction, machine);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        #endregion

        #region [MachineIndicator Functions]

        [LastModified("10-02-2025")]
        public List<MachineIndicator> GetMachineIndicators()
        {
            var machineIndicators = new List<MachineIndicator>();

            try
            {
                connection.Open();

                machineIndicators = MachineIndicatorTable.GetMachineIndicators(connection);

                connection.Close();

            }
            catch
            {
                connection.Close();
                throw;
            }

            return machineIndicators;
        }

        [LastModified("10-02-2025")]
        public bool SaveMachineIndicator(MachineIndicator machineIndicator)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineIndicatorTable.SaveMachineIndicator(connection, transaction, machineIndicator);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public bool UpdateMachineIndicator(MachineIndicator machineIndicator)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineIndicatorTable.UpdateMachineIndicator(connection, transaction, machineIndicator);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public bool DeleteMachineIndicator(MachineIndicator machineIndicator)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineIndicatorTable.DeleteMachineIndicator(connection, transaction, machineIndicator);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        #endregion

        #region [MachineIndicatorValue Functions]

        [LastModified("10-02-2025")]
        public List<MachineIndicatorValue> GetMachineIndicatorValues()
        {
            var machineIndicatorValues = new List<MachineIndicatorValue>();

            try
            {
                connection.Open();

                machineIndicatorValues = MachineIndicatorValuesTable.GetMachineIndicatorValues(connection);

                connection.Close();

            }
            catch
            {
                connection.Close();
                throw;
            }

            return machineIndicatorValues;
        }

        [LastModified("10-02-2025")]
        public List<MachineIndicatorValue> GetMachineIndicatorValuesByMachineIdMachineIndicatorId(MachineIndicatorValue machineIndicatorValue)
        {
            try
            {
                connection.Open();

                var machineId          = machineIndicatorValue.MachineId;
                var machineIndicatorId = machineIndicatorValue.MachineIndicatorId;

                var machineIndicatorValues = MachineIndicatorValuesTable.GetMachineIndicatorValuesByMachineIdMachineIndicatorId(connection, machineId, machineIndicatorId);

                connection.Close();

                return machineIndicatorValues;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        [LastModified("10-02-2025")]
        public List<MachineIndicatorValue> GetMachineIndicatorValuesByMachineIndicatorId(MachineIndicatorValue machineIndicatorValue)
        {
            try
            {
                connection.Open();

                var machineId          = machineIndicatorValue.MachineId;
                var machineIndicatorId = machineIndicatorValue.MachineIndicatorId;

                var machineIndicatorValues = MachineIndicatorValuesTable.GetMachineIndicatorValuesByMachineIndicatorId(connection, machineIndicatorId);

                connection.Close();

                return machineIndicatorValues;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        [LastModified("10-02-2025")]
        public bool SaveMachineIndicatorValue(MachineIndicatorValue machineIndicatorValue)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineIndicatorValuesTable.SaveMachineIndicatorValue(connection, transaction, machineIndicatorValue);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public bool UpdateMachineIndicatorValue(MachineIndicatorValue machineIndicatorValue)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineIndicatorValuesTable.UpdateMachineIndicatorValue(connection, transaction, machineIndicatorValue);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public bool DeleteMachineIndicatorValue(MachineIndicatorValue machineIndicatorValue)
        {
            var result = false;

            try
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    result = MachineIndicatorValuesTable.DeleteMachineIndicatorValue(connection, transaction, machineIndicatorValue);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }

            return result;
        }

        #endregion

        #endregion

    }
}
