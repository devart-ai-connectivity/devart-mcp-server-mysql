// --------------------------------------------------------------------------
// <copyright file="AdoNetMySqlRunCommand.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Devart.AI.McpServer.AdoNet.CommandLine;
using Devart.AI.McpServer.AdoNet.MySql.Properties;
using Devart.AI.McpServer.Interfaces;

namespace Devart.AI.McpServer.AdoNet.MySql.CommandLine
{
  internal sealed class AdoNetMySqlRunCommand : AdoNetRunCommand
  {
    protected override void SetupConnectionBuilder(IHostApplicationBuilder builder)
    {
      builder.Services.AddSingleton<IConnectionBuilder, AdoNetMySqlConnectionBuilder>();
    }

    protected override void RegisterTools(IMcpServerBuilder serverBuilder, McpConfiguration configuration)
      => serverBuilder.WithTools(AdoNetTools.CreateTools(configuration));

    protected override string ProductFullName => ProductInfo.ProductFullName;

    protected override McpAppSettings CreateAppSettings() => new AdoNetMySqlAppSettings();
  }
}