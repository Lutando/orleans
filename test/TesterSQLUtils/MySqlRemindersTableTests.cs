using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.ReminderService;
using Orleans.SqlUtils;
using TestExtensions;
using UnitTests.General;
using Xunit;

namespace UnitTests.RemindersTest
{
    /// <summary>
    /// Tests for operation of Orleans Reminders Table using MySQL
    /// </summary>
    public class MySqlRemindersTableTests : ReminderTableTestsBase
    {
        public MySqlRemindersTableTests(ConnectionStringFixture fixture, TestEnvironmentFixture environment) : base(fixture, environment)
        {
            LogManager.AddTraceLevelOverride(nameof(MySqlRemindersTableTests), Severity.Verbose3);
        }

        protected override IReminderTable CreateRemindersTable()
        {
            return new SqlReminderTable(this.ClusterFixture.Services.GetRequiredService<IGrainReferenceConverter>());
        }

        protected override string GetAdoInvariant()
        {
            return AdoNetInvariants.InvariantNameMySql;
        }

        protected override string GetConnectionString()
        {
            return RelationalStorageForTesting.SetupInstance(GetAdoInvariant(), testDatabaseName)
                    .Result.CurrentConnectionString;
        }


        [Fact, TestCategory("Reminders"), TestCategory("MySql")]
        public void RemindersTable_MySql_Init()
        {
        }


        [Fact, TestCategory("Reminders"), TestCategory("MySql")]
        public async Task RemindersTable_MySql_RemindersRange()
        {
            await RemindersRange();
        }

        [Fact, TestCategory("Reminders"), TestCategory("MySql")]
        public async Task RemindersTable_MySql_RemindersParallelUpsert()
        {
            await RemindersParallelUpsert();
        }

        [Fact, TestCategory("Reminders"), TestCategory("MySql")]
        public async Task RemindersTable_MySql_ReminderSimple()
        {
            await ReminderSimple();
        }
    }
}