using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;
using ConnectionLibrary.Objects.Attributes;

namespace TestProject
{
    //[TestClass]
    public class TestLocalStorage
    {

        #region [Private Functions]

        [LastModified("10-12-2024")]
        private static List<Machine> CreateBasicMachine()
        {
            var machines = new List<Machine>();

            var indicators = CreateBasicMachineIndicator();

            //var machine1 = new Machine
            //{
            //    Id                = 1,
            //    Name              = "Torno mecãnico ABB",
            //    IsEnabled         = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 1, 2, 4, 6, 7, 9, 10, 11, 14 }.Contains(w.Id)).ToList()
            //};

            //var machine2 = new Machine
            //{
            //    Id                = 2,
            //    Name              = "Fresa NEO",
            //    IsEnabled         = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 14 }.Contains(w.Id)).ToList()
            //};

            //var machine3 = new Machine
            //{
            //    Id                = 3,
            //    Name              = "CNC Tormaq",
            //    IsEnabled         = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }.Contains(w.Id)).ToList()
            //};

            //var machine4 = new Machine
            //{
            //    Id                = 4,
            //    Name              = "Torno pet",
            //    IsEnabled         = true,
            //    MachineIndicators = indicators.Where(w => new List<int> { 7, 8, 9, 11, 14 }.Contains(w.Id)).ToList()
            //};

            //machines.Add(machine1);
            //machines.Add(machine2);
            //machines.Add(machine3);
            //machines.Add(machine4);

            return machines;
        }

        [LastModified("10-12-2024")]
        private static List<MachineIndicator> CreateBasicMachineIndicator()
        {
            var basicMachineIndicators = LocalStorage.CheckForBasicIndicators(LocalStorage.GetMachineIndicators()).ToList();

            return basicMachineIndicators;
        }

        [LastModified("10-12-2024")]
        private static List<MachineIndicatorValue> CreateMachineIndicatorValues()
        {
            var rand                   = new Random();
            var machineIndicatorValues = new List<MachineIndicatorValue>();

            var machines = LocalStorage.GetMachines();

            var machine1          = machines[0];
            var machine2          = machines[1];
            var machineIndicator1 = machine1.MachineIndicators[0];

            for (int i = 0; i < 50; i++)
            {
                var value = new MachineIndicatorValue()
                {
                    MachineId          = machine1.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = rand.Next(0, 101),
                    Date               = DateTime.Now - TimeSpan.FromDays(rand.Next(1, 7)),
                    IsEnabled          = true,
                    Deleted            = false
                };

                machineIndicatorValues.Add(value);
            }

            for (int i = 0; i < 50; i++)
            {
                var value = new MachineIndicatorValue()
                {
                    MachineId          = machine2.Id,
                    MachineIndicatorId = machineIndicator1.Id,
                    Value              = rand.Next(50, 101),
                    Date               = DateTime.Now - TimeSpan.FromDays(rand.Next(1, 7)),
                    IsEnabled          = true,
                    Deleted            = false
                };

                machineIndicatorValues.Add(value);
            }

            return machineIndicatorValues;
        }

        #endregion

        [TestMethod]
        [LastModified("10-12-2024")]
        public void TestCheckForBasicIndicators()
        {
            var machineIndicatorList = LocalStorage.GetMachineIndicators();
            var result               = LocalStorage.CheckForBasicIndicators(machineIndicatorList);

            Assert.IsTrue(result.Count > 0);
        }

        #region [Machine tests]

        [TestMethod]
        [LastModified("29-11-2024")]
        public void TestSaveMachine()
        {
            var result = false;

            var machines = CreateBasicMachine();

            foreach (var machine in machines)
            {
                result = LocalStorage.SaveMachine(machine);
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        [LastModified("02-12-2024")]
        public void TestUpdateMachine()
        {
            var result = false;

            var machine = new Machine
            {
                //Id                = 2,
                Name              = "Fresa NEO modificada",
                IsEnabled         = true,
                //MachineIndicators = CreateBasicMachineIndicator().Where(w => new List<int> { 1 }.Contains(w.Id)).ToList(),
                Deleted           = false
            };

            result = LocalStorage.UpdateMachine(machine);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [LastModified("03-12-2024")]
        public void TestDeleteMachine()
        {
            var machine = new Machine
            {
                //Id = 2
            };

            var result = LocalStorage.DeleteMachine(machine);

            Assert.IsTrue(result);
        }

        #endregion

        #region [MachineIndicator tests]

        [TestMethod]
        [LastModified("29-11-2024")]
        public void TestSaveMachineIndicator()
        {
            var result = false;

            var machineIndicators = CreateBasicMachineIndicator();

            foreach (var machineIndicator in machineIndicators)
            {
                result = LocalStorage.SaveMachineIndicator(machineIndicator);
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        [LastModified("02-12-2024")]
        public void TestUpdateMachineIndicator()
        {
            var machineIndicator = new MachineIndicator
            {
                //Id        = 3,
                Name      = "Novo Indicador",
                Unit      = "senoides",
                Type      = Tipo.Social,
                IsEnabled = true,
                Deleted   = false
            };

            var result = LocalStorage.UpdateMachineIndicator(machineIndicator);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [LastModified("03-12-2024")]
        public void TestDeleteMachineIndicator()
        {
            var machineIndicator = new MachineIndicator
            {
                //Id = 3
            };

            var result = LocalStorage.DeleteMachineIndicator(machineIndicator);

            Assert.IsTrue(result);
        }

        #endregion

        #region [MachineIndicatorValue tests]

        [TestMethod]
        [LastModified("29-11-2024")]
        public void TestSaveMachineIndicatorValue()
        {
            var result = false;

            var values = CreateMachineIndicatorValues();

            foreach (var value in values)
            {
                result = LocalStorage.SaveMachineIndicatorValue(value);
            }

            Assert.IsTrue(result);
        }

        [TestMethod]
        [LastModified("02-12-2024")]
        public void TestUpdateMachineIndicatorValue()
        {
            var machineIndicatorValue = new MachineIndicatorValue
            {
                //Id                 = 2,
                //MachineId          = 2,
                //MachineIndicatorId = 1,
                Value              = 50,
                Date               = DateTime.Now - TimeSpan.FromDays(1),
                IsEnabled          = true,
                Deleted            = false
            };

            var result = LocalStorage.UpdateMachineIndicatorValue(machineIndicatorValue);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [LastModified("02-12-2024")]
        public void TestDeleteMachineIndicatorValue()
        {
            var machineIndicatorValue = new MachineIndicatorValue
            {
                //Id = 2
            };

            var result = LocalStorage.DeleteMachineIndicatorValue(machineIndicatorValue);

            Assert.IsTrue(result);
        }

        #endregion

    }
}