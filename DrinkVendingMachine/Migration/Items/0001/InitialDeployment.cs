using FluentMigrator;

namespace DrinkVendingMachine.Migration.Items
{
    [Migration(20180117140000, "InitialDeployment")]
    public class InitialDeployment : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("create-database.sql");
        }
    }
}