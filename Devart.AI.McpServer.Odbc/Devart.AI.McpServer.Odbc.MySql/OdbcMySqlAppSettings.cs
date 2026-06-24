// --------------------------------------------------------------------------
// <copyright file="OdbcMySqlAppSettings.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

namespace Devart.AI.McpServer.Odbc.MySql
{
  internal sealed class OdbcMySqlAppSettings : McpAppSettings
  {
    public override string ServerName => $"Devart {Properties.ProductInfo.ProductFullName}";

    public override string SourceName => "MySQL";

    public override bool OnPremise => true;
  }
}
