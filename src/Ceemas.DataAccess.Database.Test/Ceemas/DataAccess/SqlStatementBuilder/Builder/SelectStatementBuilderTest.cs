using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceemas.DataAccess.Database.Test.Ceemas.DataAccess.SqlStatementBuilder.Builder
{
    [TestClass]
    public class SelectStatementBuilderTest
    {
        [TestMethod]
        public void SelectWhereEqualAndEqualTest()
        {
            var statementBuilder = SqlStatement.Select(select =>
            {
                select.From("SCHEMANAME", "TABLENAME", "ALIASNAME", columns =>
                    columns.TableColumn("COLUMN1", "COLUMNALIAS1"));

                select.Where(where =>
                    where.Equal("TABLEALIAS", "COLUMNNAME", 9).And().Equal("TABLEALIAS", "COLUMNNAME", 10));
            });

            var statement = statementBuilder.ToString();
            Assert.AreEqual("SELECT ALIASNAME.COLUMN1 AS COLUMNALIAS1 FROM SCHEMANAME.ALIASNAME ALIASNAME WHERE TABLEALIAS.COLUMNNAME = 10 AND TABLEALIAS.COLUMNNAME = 9", statement);
        }

        [TestMethod]
        public void SelectWhereEqualOrEqualTest()
        {
            var statementBuilder = SqlStatement.Select(select =>
            {
                select.From("SCHEMANAME", "TABLENAME", "ALIASNAME", columns => columns
                    .TableColumn("COLUMN1", "COLUMNALIAS1"));

                select.Where(where => where
                    .Equal("TABLEALIAS", "COLUMNNAME", 9).Or().Equal("TABLEALIAS", "COLUMNNAME", 10));
            });

            var statement = statementBuilder.ToString();
            Assert.AreEqual("SELECT ALIASNAME.COLUMN1 AS COLUMNALIAS1 FROM SCHEMANAME.ALIASNAME ALIASNAME WHERE TABLEALIAS.COLUMNNAME = 10 OR TABLEALIAS.COLUMNNAME = 9", statement);
        }

        [TestMethod]
        public void SelectWhereEqualOrBlockEqualAndEqualTest()
        {
            var statementBuilder = SqlStatement.Select(select =>
            {
                select.From("SCHEMANAME", "TABLENAME", "ALIASNAME", columns => columns
                    .TableColumn("COLUMN1", "COLUMNALIAS1"));

                select.Where(where => where
                    .Equal("TABLEALIAS", "COLUMNNAME", 9)
                    .Or()
                    .Block(block => block
                        .Equal("TABLEALIAS", "COLUMNNAME", 10)
                        .And()
                        .Equal("TABLEALIAS", "COLUMNNAME", 11)
                ));
            });

            var statement = statementBuilder.ToString();
            Assert.AreEqual("SELECT ALIASNAME.COLUMN1 AS COLUMNALIAS1 FROM SCHEMANAME.ALIASNAME ALIASNAME WHERE (TABLEALIAS.COLUMNNAME = 11 AND TABLEALIAS.COLUMNNAME = 10) OR TABLEALIAS.COLUMNNAME = 9", statement);
        }

        [TestMethod]
        public void SelectTableColumnOderByTest()
        {
            var statementBuilder = SqlStatement.Select(select =>
            {
                select.From("SCHEMANAME", "TABLENAME", "ALIASNAME", columns =>
                    columns.TableColumn("COLUMN1", "COLUMNALIAS1"));

                select.Order(order => order
                    .Column("COLUMNALIAS1", OrderByMode.Ascending)
                    .Column("COLUMNALIAS2", OrderByMode.Descending)
                );
            });

            var statement = statementBuilder.ToString();
            Assert.AreEqual("SELECT ALIASNAME.COLUMN1 AS COLUMNALIAS1 FROM SCHEMANAME.ALIASNAME ALIASNAME ORDER BY COLUMNALIAS1, COLUMNALIAS2 DESC", statement);
        }

        [TestMethod]
        public void SelectJoinTest()
        {
            var statementBuilder = SqlStatement.Select(select =>
            {
                select.From("SCHEMANAME", "TABLENAME", "ALIASNAME", columns =>
                    columns.TableColumn("COLUMN1", "COLUMNALIAS1"));

                select.Join("SCHEMANAME1", "JOINNAME", "JOINALIAS", 
                    join => join.Equal("TABLEALIAS", "COLUMN1", "JOINALIAS", "COLUMN_J_1"),
                    columns => columns
                    .TableColumn("COLUMN_J_1", "COLUMNALIAS_J_1")
                    .TableColumn("COLUMN_J_2", "COLUMNALIAS_J_2"));
            });

            var statement = statementBuilder.ToString();
            Assert.AreEqual("SELECT ALIASNAME.COLUMN1 AS COLUMNALIAS1, JOINALIAS.COLUMN_J_1 AS COLUMNALIAS_J_1, JOINALIAS.COLUMN_J_2 AS COLUMNALIAS_J_2 FROM SCHEMANAME.ALIASNAME ALIASNAME JOIN SCHEMANAME1.JOINALIAS JOINALIAS ON TABLEALIAS.COLUMN1 = JOINALIAS.COLUMN_J_1", statement);
        }
    }
}
