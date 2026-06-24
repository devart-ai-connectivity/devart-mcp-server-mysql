// --------------------------------------------------------------------------
// <copyright file="OdbcMySqlForeignKeysTool.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Devart.AI.McpServer.Extensions;
using Devart.AI.McpServer.Tools;
using Devart.AI.McpServer.Interfaces;

namespace Devart.AI.McpServer.Odbc.MySql.Tools
{
  internal sealed class OdbcMySqlForeignKeysTool(McpConfiguration serverConfiguration) : ForeignKeysTool(serverConfiguration)
  {
    protected override async Task<DataTable> GetMetadataTable(
      DbConnection connection, 
      string schema, 
      string tableName, 
      IServiceProvider services, 
      CancellationToken cancellationToken)
    {
      const string sql =
"""
  SELECT
    K.CONSTRAINT_NAME FK_NAME,
    K.COLUMN_NAME FKCOLUMN_NAME,
    K.REFERENCED_TABLE_SCHEMA PKTABLE_SCHEM,
    K.REFERENCED_TABLE_NAME PKTABLE_NAME,
    K.REFERENCED_COLUMN_NAME PKCOLUMN_NAME,
    R.UPDATE_RULE UPDATE_RULE,
    R.DELETE_RULE DELETE_RULE
  FROM
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE K JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS R
    USING(CONSTRAINT_SCHEMA, CONSTRAINT_NAME, TABLE_NAME)
  WHERE
    K.TABLE_SCHEMA = ?
    AND K.TABLE_NAME = ?
  ORDER BY K.CONSTRAINT_NAME, K.ORDINAL_POSITION
""";
      var database = services.GetRequiredService<IDatabase>();
      var commandHelper = services.GetRequiredService<ICommandHelper>();

      await using var reader = await database.ExecuteReaderAsync(
        connection,
        sql,
        cmd =>
        {
          commandHelper.AddParameter(cmd, connection.Database);
          commandHelper.AddParameter(cmd, tableName);
        },
        cancellationToken
      ).ConfigureAwait(false);

      return await reader.ToDataTableAsync(OdbcConstants.ForeignKeysCollectionName, cancellationToken).ConfigureAwait(false);
    }
  }
}
