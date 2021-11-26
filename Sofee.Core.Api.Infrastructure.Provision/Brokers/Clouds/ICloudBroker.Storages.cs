// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using Sofee.Core.Api.Infrastructure.Provision.Models.Storages;

namespace Sofee.Core.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<ISqlServer> CreateSqlServerAsync(
            string sqlServerName,
            IResourceGroup resourceGroup);

        ValueTask<ISqlDatabase> CreateSqlDatabaseAsync(
            string sqlDatabasename,
            ISqlServer sqlServer);

        SqlDatabaseAccess GetAdminAccess();
    }
}
