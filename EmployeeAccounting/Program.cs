using EmployeeAccounting.Application.Interfaces;
using EmployeeAccounting.Forms;
using EmployeeAccounting.Infrastructure.Database;
using Microsoft.Extensions.Configuration;

namespace EmployeeAccounting;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var serverConnection = configuration.GetConnectionString("ServerConnection")
                ?? throw new InvalidOperationException("В appsettings.json нет строки ServerConnection.");
            var defaultConnection = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("В appsettings.json нет строки DefaultConnection.");

            IDatabaseInitializer initializer = new DatabaseInitializer(serverConnection, defaultConnection);
            initializer.Initialize();

            System.Windows.Forms.Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Не удалось подключиться к PostgreSQL или инициализировать базу данных.\n\n" +
                ex.Message,
                "Ошибка подключения",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
