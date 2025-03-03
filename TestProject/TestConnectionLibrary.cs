using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;
using Microsoft.Extensions.Configuration;

namespace TestProject
{
    //[TestClass]
    public class TestConnectionLibrary
    {
        private readonly IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        [LastModified("06-11-2024")]
        private static List<Machine> CreateBasicMachine(MachineContext machineContext)
        {
            var machines = new List<Machine>();

            var indicators = machineContext.GetMachineIndicators();

            //var machine1 = new Machine
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Torno mecanico ABB",
            //    IsEnabled = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 1, 2 }.Contains(w.Id)).ToList()
            //};

            //var machine2 = new Machine
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Fresa NEO",
            //    IsEnabled = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 3, 4 }.Contains(w.Id)).ToList()
            //};

            //var machine3 = new Machine
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "CNC Tormaq",
            //    IsEnabled = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 1, 2 }.Contains(w.Id)).ToList()
            //};

            //var machine4 = new Machine
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Torno pet",
            //    IsEnabled = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 3, 4 }.Contains(w.Id)).ToList()
            //};

            //machines.Add(machine1);
            //machines.Add(machine2);
            //machines.Add(machine3);
            //machines.Add(machine4);

            return machines;
        }

        [LastModified("07-11-2024")]
        private static List<MachineIndicatorValue> CreateMachineIndicatorValues(MachineContext machineContext)
        {
            var machineIndicatorValues = new List<MachineIndicatorValue>();
            
            var machine1 = machineContext.GetMachines()[0];
            var machine2 = machineContext.GetMachines()[1];
            var machineIndicator1 = machine1.MachineIndicators[0];

            var machine1IndicatorValues = new List<MachineIndicatorValue>
            {
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 10,
                    Date               = DateTime.Now - TimeSpan.FromDays(7),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 25,
                    Date               = DateTime.Now - TimeSpan.FromDays(7),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 40,
                    Date               = DateTime.Now - TimeSpan.FromDays(5),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 63,
                    Date               = DateTime.Now - TimeSpan.FromDays(5),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 78,
                    Date               = DateTime.Now - TimeSpan.FromDays(4),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 60,
                    Date               = DateTime.Now - TimeSpan.FromDays(2),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 50,
                    Date               = DateTime.Now - TimeSpan.FromDays(1),
                    IsEnabled          = true,
                    Deleted            = false
                }
            };

            var machine2IndicatorValues = new List<MachineIndicatorValue>
            {
                new()
                {
                    MachineId          = machine2.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 14,
                    Date               = DateTime.Now - TimeSpan.FromDays(5),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine2.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 21,
                    Date               = DateTime.Now - TimeSpan.FromDays(4),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine2.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 45,
                    Date               = DateTime.Now - TimeSpan.FromDays(3),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine2.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 68,
                    Date               = DateTime.Now - TimeSpan.FromDays(3),
                    IsEnabled          = true,
                    Deleted            = false
                },
                new()
                {
                    MachineId          = machine2.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = 85,
                    Date               = DateTime.Now - TimeSpan.FromDays(1),
                    IsEnabled          = true,
                    Deleted            = false
                }
            };
                        
            machineIndicatorValues.AddRange(machine1IndicatorValues);
            machineIndicatorValues.AddRange(machine2IndicatorValues);

            return machineIndicatorValues;
        }

        #region [Machine tests]

        [TestMethod]
        [LastModified("06-11-2024")]
        public void TestInsertMachine()
        {
            var machineContext = new MachineContext(configuration);

            var machines = CreateBasicMachine(machineContext);

            foreach (var machine in machines)
            {
                machineContext.SaveMachine(machine);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestUpdateMachine()
        {
            var machineContext = new MachineContext(configuration);

            var machines = CreateBasicMachine(machineContext);

            foreach (var machine in machines)
            {
                machine.Name = "Updated " + machine.Name;
                machine.IsEnabled = !machine.IsEnabled;

                foreach (var indicator in machine.MachineIndicators)
                {
                    indicator.Name = "Updated " + indicator.Name;
                    indicator.IsEnabled = !indicator.IsEnabled;
                }
            }

            foreach (var machine in machines)
            {
                machineContext.UpdateMachine(machine);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestDeleteMachine()
        {
            var machineContext = new MachineContext(configuration);

            var machines = CreateBasicMachine(machineContext);

            foreach (var machine in machines)
            {
                machineContext.DeleteMachine(machine);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestGetMachines()
        {
            var machineContext = new MachineContext(configuration);

            var machines = machineContext.GetMachines();

            Assert.IsTrue(machines.Count > 0);
        }

        #endregion

        #region [MachineIndicatorValues tests]

        [TestMethod]
        [LastModified("06-11-2024")]
        public void TestInsertingMachineIndicatorValue()
        {
            var machineContext = new MachineContext(configuration);

            var machineIndicatorValues = CreateMachineIndicatorValues(machineContext);

            foreach (var machineIndicatorValue in machineIndicatorValues)
            {
                machineContext.SaveMachineIndicatorValue(machineIndicatorValue);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        [LastModified("07-11-2024")]
        public void TestGetMachineIndicatorValuesByMachineIdMachineIndicatorId()
        {
            var machineContext = new MachineContext(configuration);

            var machineIndicatorValue = new MachineIndicatorValue
            {
                //Id                 = 0,
                //MachineId          = 0,
                //MachineIndicatorId = 1,
                Value              = 0,
                Date               = DateTime.Now,
                IsEnabled          = true,
                Deleted            = false
            };

            var machineIndicatorValues = machineContext.GetMachineIndicatorValuesByMachineIdMachineIndicatorId(machineIndicatorValue);

            Assert.IsTrue(machineIndicatorValues.Count > 0);
        }

        #endregion

    }
}