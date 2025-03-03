using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using Microsoft.Data.SqlClient;

namespace ConnectionLibrary.Contexts.TableFunctions
{
    public static class MachineIndicatorValuesTable
    {
        [LastModified("10-02-2025")]
        public static List<MachineIndicatorValue> GetMachineIndicatorValues(SqlConnection connection)
        {
            try
            {
                var machineIndicatorValues = new List<MachineIndicatorValue>();

                var query = "SELECT * FROM MachineIndicatorValues";

                var command = new SqlCommand(query, connection);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        machineIndicatorValues.Add(new MachineIndicatorValue
                        {
                            Id                 = reader.GetGuid(0),
                            MachineId          = reader.GetGuid(1),
                            MachineIndicatorId = reader.GetGuid(2),
                            Value              = reader.GetDouble(3),
                            Date               = reader.GetDateTime(4),
                            IsEnabled          = reader.GetBoolean(5),
                            Deleted            = reader.GetBoolean(6),
                            TimeStamp          = reader.GetDateTime(7)
                        });
                    }
                }

                return machineIndicatorValues;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to retrieve machine indicator values.", e);
            }
        }

        [LastModified("10-02-2025")]
        public static List<MachineIndicatorValue> GetMachineIndicatorValuesByMachineIdMachineIndicatorId(SqlConnection connection, Guid machineId, Guid machineIndicatorId)
        {
            try
            {
                var machineIndicatorValues = new List<MachineIndicatorValue>();

                var query = "SELECT * FROM MachineIndicatorValues " +
                    "WHERE MachineId = @MachineId " +
                    "AND MachineIndicatorId = @MachineIndicatorId " +
                    "AND IsEnabled = 1 " +
                    "AND Deleted = 0";

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@MachineId", machineId);
                command.Parameters.AddWithValue("@MachineIndicatorId", machineIndicatorId);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        machineIndicatorValues.Add(new MachineIndicatorValue
                        {
                            Id                 = reader.GetGuid(0),
                            MachineId          = reader.GetGuid(1),
                            MachineIndicatorId = reader.GetGuid(2),
                            Value              = reader.GetDouble(3),
                            Date               = reader.GetDateTime(4),
                            IsEnabled          = reader.GetBoolean(5),
                            Deleted            = reader.GetBoolean(6)
                        });
                    }
                }

                return machineIndicatorValues;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to retrieve machine indicator values.", e);
            }
        }

        [LastModified("10-02-2025")]
        public static List<MachineIndicatorValue> GetMachineIndicatorValuesByMachineIndicatorId(SqlConnection connection, Guid machineIndicatorId)
        {
            try
            {
                var machineIndicatorValues = new List<MachineIndicatorValue>();

                var query = "SELECT * FROM MachineIndicatorValues " +
                    "WHERE MachineIndicatorId = @MachineIndicatorId " +
                    "AND IsEnabled = 1 " +
                    "AND Deleted = 0";

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@MachineIndicatorId", machineIndicatorId);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        machineIndicatorValues.Add(new MachineIndicatorValue
                        {
                            Id                 = reader.GetGuid(0),
                            MachineId          = reader.GetGuid(1),
                            MachineIndicatorId = reader.GetGuid(2),
                            Value              = reader.GetDouble(3),
                            Date               = reader.GetDateTime(4),
                            IsEnabled          = reader.GetBoolean(5),
                            Deleted            = reader.GetBoolean(6)
                        });
                    }
                }

                return machineIndicatorValues;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to retrieve machine indicator values.", e);
            }
        }

        [LastModified("10-02-2025")]
        public static bool SaveMachineIndicatorValue(SqlConnection connection, SqlTransaction transaction, MachineIndicatorValue machineIndicatorValue)
        {
            object? queryResult;

            var result = false;

            try
            {
                var query = "INSERT INTO MachineIndicatorValues (Id, MachineId, MachineIndicatorId, Value, Date, IsEnabled, Deleted, TimeStamp) " +
                    "VALUES (@Id, @MachineId, @MachineIndicatorId, @Value, @Date, @IsEnabled, @Deleted, @TimeStamp)";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Id", machineIndicatorValue.Id);
                command.Parameters.AddWithValue("@MachineId", machineIndicatorValue.MachineId);
                command.Parameters.AddWithValue("@MachineIndicatorId", machineIndicatorValue.MachineIndicatorId);
                command.Parameters.AddWithValue("@Value", machineIndicatorValue.Value);
                command.Parameters.AddWithValue("@Date", machineIndicatorValue.Date);
                command.Parameters.AddWithValue("@IsEnabled", machineIndicatorValue.IsEnabled);
                command.Parameters.AddWithValue("@Deleted", machineIndicatorValue.Deleted);
                command.Parameters.AddWithValue("@TimeStamp", machineIndicatorValue.TimeStamp);

                queryResult = command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to insert machine indicator value and retrieve the ID.");
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public static bool UpdateMachineIndicatorValue(SqlConnection connection, SqlTransaction transaction, MachineIndicatorValue machineIndicatorValue)
        {
            var result = false;

            try
            {
                var query = "UPDATE MachineIndicatorValues " +
                    "SET MachineId = @MachineId, MachineIndicatorId = @MachineIndicatorId, Value = @Value, Date = @Date, IsEnabled = @IsEnabled, TimeStamp = @TimeStamp " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection, transaction);


                command.Parameters.AddWithValue("@Id", machineIndicatorValue.Id);
                command.Parameters.AddWithValue("@MachineId", machineIndicatorValue.MachineId);
                command.Parameters.AddWithValue("@MachineIndicatorId", machineIndicatorValue.MachineIndicatorId);
                command.Parameters.AddWithValue("@Value", machineIndicatorValue.Value);
                command.Parameters.AddWithValue("@Date", machineIndicatorValue.Date);
                command.Parameters.AddWithValue("@IsEnabled", machineIndicatorValue.IsEnabled);
                command.Parameters.AddWithValue("@TimeStamp", machineIndicatorValue.TimeStamp);

                command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to update machine indicator value.");
            }

            return result;
        }

        [LastModified("10-02-2025")]
        public static bool DeleteMachineIndicatorValue(SqlConnection connection, SqlTransaction transaction, MachineIndicatorValue machineIndicatorValue)
        {
            var result = false;

            try
            {
                var query = "UPDATE MachineIndicatorValues " +
                    "SET Deleted = @Deleted " +
                    "WHERE Id = @Id";

                var command = new SqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@Deleted", machineIndicatorValue.Deleted);
                command.Parameters.AddWithValue("@Id", machineIndicatorValue.Id);

                command.ExecuteScalar();

                result = true;
            }
            catch
            {
                throw new InvalidOperationException("Failed to delete machine indicator value.");
            }

            return result;
        }
    }
}
