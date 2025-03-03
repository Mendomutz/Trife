using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using Microsoft.Data.SqlClient;

namespace ConnectionLibrary.Contexts.TableFunctions
{
    public static class MachineIndicatorTable
    {
        [LastModified("10-02-2025")]
        public static List<MachineIndicator> GetMachineIndicators(SqlConnection connection)
        {
            try
            {
                var machineIndicators = new List<MachineIndicator>();

                var query = "SELECT * FROM MachineIndicator WHERE Deleted = 0";

                var command = new SqlCommand(query, connection);
                var reader  = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        machineIndicators.Add(new MachineIndicator
                        {
                            Id        = reader.GetGuid(0),
                            Name      = reader.GetString(1),
                            Unit      = reader.GetString(2),
                            Type      = (Tipo)reader.GetInt32(3),
                            IsEnabled = reader.GetBoolean(4),
                            Deleted   = reader.GetBoolean(5),
                            TimeStamp = reader.GetDateTime(6)
                        });
                    }
                }

                return machineIndicators;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to retrieve machine indicators.", e);
            }
        }

        [LastModified("10-02-2025")]
        public static bool SaveMachineIndicator(SqlConnection connection, SqlTransaction transaction, MachineIndicator machineIndicators)
        {
            object? queryResult;

            var result = false;

            try
            {
                var query = "INSERT INTO MachineIndicator (Id, Name, Unit, Type, IsEnabled, Deleted, TimeStamp) " +
                    "VALUES (@Id, @Name, @Unit, @Type, @IsEnabled, @Deleted, @TimeStamp)";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Id", machineIndicators.Id);
                command.Parameters.AddWithValue("@Name", machineIndicators.Name);
                command.Parameters.AddWithValue("@Unit", machineIndicators.Unit);
                command.Parameters.AddWithValue("@Type", (int)machineIndicators.Type);
                command.Parameters.AddWithValue("@IsEnabled", machineIndicators.IsEnabled);
                command.Parameters.AddWithValue("@Deleted", machineIndicators.Deleted);
                command.Parameters.AddWithValue("@TimeStamp", machineIndicators.TimeStamp);

                queryResult = command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to insert machine indicators and retrieve the ID.");
            }
            
            return result;
        }

        [LastModified("10-02-2025")]
        public static bool UpdateMachineIndicator(SqlConnection connection, SqlTransaction transaction, MachineIndicator machineIndicator)
        {
            var result = false;

            try
            {
                var query = "UPDATE MachineIndicator " +
                    "SET Name = @Name, Unit = @Unit, Type = @Type, IsEnabled = @IsEnabled, TimeStamp = @TimeStamp " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Id", machineIndicator.Id);
                command.Parameters.AddWithValue("@Name", machineIndicator.Name);
                command.Parameters.AddWithValue("@Unit", machineIndicator.Unit);
                command.Parameters.AddWithValue("@Type", (int)machineIndicator.Type);
                command.Parameters.AddWithValue("@IsEnabled", machineIndicator.IsEnabled);
                command.Parameters.AddWithValue("@TimeStamp", machineIndicator.TimeStamp);

                command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to update machine indicators.");
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public static bool DeleteMachineIndicator(SqlConnection connection, SqlTransaction transaction, MachineIndicator machineIndicator)
        {
            var result = false;

            try
            {
                var query = "UPDATE MachineIndicator " +
                    "SET Deleted = @Deleted " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Deleted", machineIndicator.Deleted);
                command.Parameters.AddWithValue("@Id", machineIndicator.Id);

                command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to delete machine indicators.");
            }

            return result;
        }
    }
}
