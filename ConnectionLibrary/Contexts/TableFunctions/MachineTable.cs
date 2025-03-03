using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace ConnectionLibrary.Contexts.TableFunctions
{
    public static class MachineTable
    {
        [LastModified("10-02-2025")]
        public static List<Machine> GetMachines(SqlConnection connection)
        {
            try
            {
                var machines = new List<Machine>();

                var query = "SELECT * FROM Machine WHERE Deleted = 0";

                var command = new SqlCommand(query, connection);
                var reader  = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        machines.Add(new Machine
                        {
                            Id                = reader.GetGuid(0),
                            Name              = reader.GetString(1),
                            MachineIndicators = JsonSerializer.Deserialize<List<MachineIndicator>>(reader.GetString(2))!,
                            IsEnabled         = reader.GetBoolean(3),
                            TimeStamp         = reader.GetDateTime(5)
                        });
                    }
                }

                return machines;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to retrieve machines.", e);
            }
        }

        [LastModified("10-02-2025")]
        public static bool SaveMachine(SqlConnection connection, SqlTransaction transaction, Machine machine)
        {
            object? queryResult;

            var result = false;

            try
            {
                var query = "INSERT INTO Machine (Id, Name, MachineIndicators, IsEnabled, Deleted, TimeStamp) " +
                    "VALUES (@Id, @Name, @MachineIndicators, @IsEnabled, @Deleted, @TimeStamp)";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Id", machine.Id);
                command.Parameters.AddWithValue("@Name", machine.Name);
                command.Parameters.AddWithValue("@MachineIndicators", JsonSerializer.Serialize(machine.MachineIndicators));
                command.Parameters.AddWithValue("@IsEnabled", machine.IsEnabled);
                command.Parameters.AddWithValue("@Deleted", false);
                command.Parameters.AddWithValue("@TimeStamp", machine.TimeStamp);

                queryResult = command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to insert machine and retrieve the ID.");
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public static bool UpdateMachine(SqlConnection connection, SqlTransaction transaction, Machine machine)
        {
            var result = false;

            try
            {
                var query = "UPDATE Machine " +
                    "SET Name = @Name, MachineIndicators = @MachineIndicators, IsEnabled = @IsEnabled, TimeStamp = @TimeStamp " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Id", machine.Id);
                command.Parameters.AddWithValue("@Name", machine.Name);
                command.Parameters.AddWithValue("@MachineIndicators", JsonSerializer.Serialize(machine.MachineIndicators));
                command.Parameters.AddWithValue("@IsEnabled", machine.IsEnabled);
                command.Parameters.AddWithValue("@TimeStamp", machine.TimeStamp);

                command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to update machine.");
            }

            return result;
        }

        [LastModified("18-02-2025")]
        public static bool DeleteMachine(SqlConnection connection, SqlTransaction transaction, Machine machine)
        {
            var result = false;

            try
            {
                var query = "UPDATE Machine " +
                    "SET Deleted = @Deleted " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Id", machine.Id);
                command.Parameters.AddWithValue("@Deleted", true);

                command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to delete machine.");
            }

            return result;
        }
    }
}
