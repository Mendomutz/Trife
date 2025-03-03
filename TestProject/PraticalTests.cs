using ConnectionLibrary.Contexts;
using ConnectionLibrary.Objects;

namespace TestProject
{
    [TestClass]
    public class PraticalTests
    {
        private readonly LibraryContext libraryContext = new();

        #region [Machines]

        [TestMethod]
        public void TesteCadastroMaquina()
        {
            var listIndicators = new List<MachineIndicator>
            {
                new()
                {
                    Name = "Indicador 1",
                    Unit = "Unidade",
                    Type = Tipo.Ambiental,
                },
                new()
                {
                    Name = "Indicador 2",
                    Unit = "Unidade",
                    Type = Tipo.Social,
                }
            };

            var machine = new Machine
            {
                Name              = "Teste de cadastro maquina",
                MachineIndicators = listIndicators,
                IsEnabled         = true
            };

            var context = new MachineContext(libraryContext.Configuration);

            var dbResult    = context.SaveMachine(machine);
            var localResult = LocalStorage.SaveMachine(machine);

            Assert.IsTrue(dbResult && localResult);
        }

        [TestMethod]
        public void TesteListarMaquina()
        {
            var context = new MachineContext(libraryContext.Configuration);

            var dbMachines = context.GetMachines();
            var localMachines = LocalStorage.GetMachines();

            var dbResult    = dbMachines.Count > 0;
            var localResult = localMachines.Count > 0;

            Assert.IsTrue(dbResult && localResult);
        }

        [TestMethod]
        public void TesteEditarMaquina()
        {
            var context = new MachineContext(libraryContext.Configuration);

            var machine = context.GetMachines().FirstOrDefault();

            machine!.Name      = "Teste de edição maquina";
            machine!.IsEnabled = false;

            var dbResult    = context.UpdateMachine(machine);
            var localResult = LocalStorage.UpdateMachine(machine);

            Assert.IsTrue(dbResult && localResult);
        }

        [TestMethod]
        public void TesteDeletarMaquina()
        {
            var context = new MachineContext(libraryContext.Configuration);

            var machine = context.GetMachines().FirstOrDefault();

            var dbResult    = context.DeleteMachine(machine!);
            var localResult = LocalStorage.DeleteMachine(machine!);

            Assert.IsTrue(dbResult && localResult);
        }

        #endregion

    }
}
