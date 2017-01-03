﻿using Dotmim.Sync.Core.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Dotmim.Sync.Data;
using System.Data.Common;
using Dotmim.Sync.Core.Common;
using System.Data.SqlClient;

namespace Dotmim.Sync.SqlServer.Builders
{

    /// <summary>
    /// The SqlBuilder class is the Sql implementation of DbBuilder class.
    /// In charge of creating tracking table, stored proc, triggers and adapters.
    /// </summary>
    public class SqlBuilder : DbBuilder
    {

        SqlObjectNames sqlObjectNames;
        public override DbObjectNames ObjectNames
        {
            get
            {
                return sqlObjectNames;
            }
        }

        public SqlBuilder(DmTable tableDescription, DbBuilderOption option = DbBuilderOption.CreateOrUseExistingSchema)
            : base(tableDescription, option)
        {
            sqlObjectNames = new SqlObjectNames(tableDescription);
        }

        internal static (ObjectNameParser tableName, ObjectNameParser trackingName) GetParsers(DmTable tableDescription)
        {
            string tableAndPrefixName = String.IsNullOrWhiteSpace(tableDescription.Prefix) ? tableDescription.TableName : $"{tableDescription.Prefix}.{tableDescription.TableName}";
            var originalTableName = new ObjectNameParser(tableAndPrefixName, "[", "]");
            var trackingTableName = new ObjectNameParser($"{tableAndPrefixName}_tracking", "[", "]");

            return (originalTableName, trackingTableName);
        }
        public static string WrapScriptTextWithComments(string commandText, string commentText, bool includeGo = true, int indentLevel = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder("\n");
            for (int i = 0; i < indentLevel; i++)
            {
                stringBuilder.Append("\t");
                stringBuilder1.Append("\t");
            }
            string str = stringBuilder1.ToString();
            stringBuilder.Append(string.Concat("-- BEGIN ", commentText, str));
            stringBuilder.Append(commandText);
            stringBuilder.Append(string.Concat(str, (includeGo ? string.Concat("GO", str) : string.Empty)));
            stringBuilder.Append(string.Concat("-- END ", commentText, str, "\n"));
            return stringBuilder.ToString();
        }

        public override IDbBuilderProcedureHelper CreateProcBuilder(DbConnection connection, DbTransaction transaction = null)
        {
            return new SqlBuilderProcedure(connection, transaction);
        }

        public override IDbBuilderTriggerHelper CreateTriggerBuilder(DbConnection connection, DbTransaction transaction = null)
        {
            return new SqlBuilderTrigger(connection, transaction);
        }

        public override IDbBuilderTableHelper CreateTableBuilder(DbConnection connection, DbTransaction transaction = null)
        {
            return new SqlBuilderTable(connection, transaction);
        }

        public override IDbBuilderTrackingTableHelper CreateTrackingTableBuilder(DbConnection connection, DbTransaction transaction = null)
        {
            return new SqlBuilderTrackingTable(connection, transaction);
        }

        public override DbSyncAdapter CreateSyncAdapter(DbConnection connection, DbTransaction transaction = null)
        {
            return new SqlSyncAdapter(connection, transaction);
        }
    }
}
