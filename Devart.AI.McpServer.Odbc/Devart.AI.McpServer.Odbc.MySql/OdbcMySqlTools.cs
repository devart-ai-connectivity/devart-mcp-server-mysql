// --------------------------------------------------------------------------
// <copyright file="OdbcMySqlTools.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using ModelContextProtocol.Server;
using Devart.AI.McpServer.Odbc.MySql.Tools;

namespace Devart.AI.McpServer.Odbc.MySql
{
  internal static class OdbcMySqlTools
  {
    public static List<McpServerTool> CreateTools(McpConfiguration configuration)
      => OdbcTools.CreateBuilder(configuration)
        .Add(new OdbcMySqlPrimaryKeysTool(configuration))
        .Add(new OdbcMySqlForeignKeysTool(configuration))
        .Build();
  }
}
