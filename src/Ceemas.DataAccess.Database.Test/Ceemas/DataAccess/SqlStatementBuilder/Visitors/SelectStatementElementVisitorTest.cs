using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    [TestClass]
    public class SelectStatementElementVisitorTest
    {
        [TestMethod]
        public void SelectTest()
        {
            var tableColumns = new ColumnElement[] { Element.TableColumn("TABLEALIAS", "COLUMNNAME", "COLUMNALIAS") };
            var fromStatementElement = Element.FromStatement(Element.From("SCHEMANAME", "TABLENAME", "TABLEALIAS"), tableColumns);
            var selectElement = Element.SelectStatement(new FromStatementElement[] { fromStatementElement }, null, null, null);

            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SeletctStatementElementVisitor(sqlBuilder);

            visitor.Visit(selectElement);
            var sqlStatement = sqlBuilder.ToString();

            Assert.AreEqual("SELECT TABLEALIAS.COLUMNNAME AS COLUMNALIAS FROM SCHEMANAME.TABLENAME TABLEALIAS", sqlStatement);
        }

        [TestMethod]
        public void SelectCountTest()
        {
            var tableColumns = new ColumnElement[] { Element.Column(Element.Count(null), "ANZAHL") };
            var fromStatementElement = Element.FromStatement(Element.From("SCHEMANAME", "TABLENAME", "TABLEALIAS"), tableColumns);
            var selectElement = Element.SelectStatement(new FromStatementElement[] { fromStatementElement }, null, null, null);

            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SeletctStatementElementVisitor(sqlBuilder);

            visitor.Visit(selectElement);
            var sqlStatement = sqlBuilder.ToString();

            Assert.AreEqual("SELECT COUNT(*) AS ANZAHL FROM SCHEMANAME.TABLENAME TABLEALIAS", sqlStatement);
        }

        [TestMethod]
        public void SelectMultiColumnTest()
        {
            var tableColumns = new ColumnElement[] { Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "COLUMNALIAS1"), Element.TableColumn("TABLEALIAS", "COLUMNNAME2", "COLUMNALIAS2") };
            var fromStatementElement = Element.FromStatement(Element.From("SCHEMANAME", "TABLENAME", "TABLEALIAS"), tableColumns);
            var selectElement = Element.SelectStatement(new FromStatementElement[] { fromStatementElement }, null, null, null);

            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SeletctStatementElementVisitor(sqlBuilder);

            visitor.Visit(selectElement);
            var sqlStatement = sqlBuilder.ToString();

            Assert.AreEqual("SELECT TABLEALIAS.COLUMNNAME1 AS COLUMNALIAS1, TABLEALIAS.COLUMNNAME2 AS COLUMNALIAS2 FROM SCHEMANAME.TABLENAME TABLEALIAS", sqlStatement);
        }

        [TestMethod]
        public void SelectMultiFromTest()
        {
            var tableColumns1 = new ColumnElement[] { Element.TableColumn("TABLEALIAS1", "COLUMNNAME1", "COLUMNALIAS1") };
            var fromStatementElement1 = Element.FromStatement(Element.From("SCHEMANAME1", "TABLENAME1", "TABLEALIAS1"), tableColumns1);

            var tableColumns2 = new ColumnElement[] { Element.TableColumn("TABLEALIAS2", "COLUMNNAME2", "COLUMNALIAS2") };
            var fromStatementElement2 = Element.FromStatement(Element.From("SCHEMANAME2", "TABLENAME2", "TABLEALIAS2"), tableColumns2);

            var selectElement = Element.SelectStatement(new FromStatementElement[] { fromStatementElement1, fromStatementElement2 }, null, null, null);

            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SeletctStatementElementVisitor(sqlBuilder);

            visitor.Visit(selectElement);
            var sqlStatement = sqlBuilder.ToString();

            Assert.AreEqual("SELECT TABLEALIAS1.COLUMNNAME1 AS COLUMNALIAS1, TABLEALIAS2.COLUMNNAME2 AS COLUMNALIAS2 FROM SCHEMANAME1.TABLENAME1 TABLEALIAS1, SCHEMANAME2.TABLENAME2 TABLEALIAS2", sqlStatement);
        }

        [TestMethod]
        public void SelectMultiFromAndColumnTest()
        {
            var tableColumns1 = new ColumnElement[] { Element.TableColumn("TABLEALIAS1", "COLUMNNAME1-1", "COLUMNALIAS1-1"), Element.TableColumn("TABLEALIAS1", "COLUMNNAME1-2", "COLUMNALIAS1-2") };
            var fromStatementElement1 = Element.FromStatement(Element.From("SCHEMANAME1", "TABLENAME1", "TABLEALIAS1"), tableColumns1);

            var tableColumns2 = new ColumnElement[] { Element.TableColumn("TABLEALIAS2", "COLUMNNAME2-1", "COLUMNALIAS2-1"), Element.TableColumn("TABLEALIAS2", "COLUMNNAME2-2", "COLUMNALIAS2-2") };
            var fromStatementElement2 = Element.FromStatement(Element.From("SCHEMANAME2", "TABLENAME2", "TABLEALIAS2"), tableColumns2);

            var selectElement = Element.SelectStatement(new FromStatementElement[] { fromStatementElement1, fromStatementElement2 }, null, null, null);

            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SeletctStatementElementVisitor(sqlBuilder);

            visitor.Visit(selectElement);
            var sqlStatement = sqlBuilder.ToString();

            Assert.AreEqual("SELECT TABLEALIAS1.COLUMNNAME1-1 AS COLUMNALIAS1-1, TABLEALIAS1.COLUMNNAME1-2 AS COLUMNALIAS1-2, TABLEALIAS2.COLUMNNAME2-1 AS COLUMNALIAS2-1, TABLEALIAS2.COLUMNNAME2-2 AS COLUMNALIAS2-2 FROM SCHEMANAME1.TABLENAME1 TABLEALIAS1, SCHEMANAME2.TABLENAME2 TABLEALIAS2", sqlStatement);
        }

        [TestMethod]
        public void SelectJoinTest()
        {
            var tableColumns1 = new ColumnElement[] { Element.TableColumn("TABLEALIAS1", "COLUMNNAME1-1", "COLUMNALIAS1-1"), Element.TableColumn("TABLEALIAS1", "COLUMNNAME1-2", "COLUMNALIAS1-2") };
            var fromStatementElement1 = Element.FromStatement(Element.From("SCHEMANAME1", "TABLENAME1", "TABLEALIAS1"), tableColumns1);

            var tableColumns2 = new ColumnElement[] { Element.TableColumn("TABLEALIAS2", "COLUMNNAME2-1", "COLUMNALIAS2-1"), Element.TableColumn("TABLEALIAS2", "COLUMNNAME2-2", "COLUMNALIAS2-2") };
            var joinOnStatement = Element.JoinOnStatement(Element.Equal(Element.TableColumn("TABLEALIAS2", "COLUMNNAME2-1", ""), Element.TableColumn("TABLEALIAS1", "COLUMNNAME1-1", "")));
            var joinStatementElement1 = Element.JoinStatement(Element.From("SCHEMANAME2", "TABLENAME2", "TABLEALIAS2"), joinOnStatement, tableColumns2);

            var selectElement = Element.SelectStatement(new FromStatementElement[] { fromStatementElement1 }, new JoinStatementElement[] { joinStatementElement1 }, null, null);

            var sqlBuilder = new SqlStringBuilder();
            var visitor = new SeletctStatementElementVisitor(sqlBuilder);

            visitor.Visit(selectElement);
            var sqlStatement = sqlBuilder.ToString();

            Assert.AreEqual("SELECT TABLEALIAS1.COLUMNNAME1-1 AS COLUMNALIAS1-1, TABLEALIAS1.COLUMNNAME1-2 AS COLUMNALIAS1-2, TABLEALIAS2.COLUMNNAME2-1 AS COLUMNALIAS2-1, TABLEALIAS2.COLUMNNAME2-2 AS COLUMNALIAS2-2 FROM SCHEMANAME1.TABLENAME1 TABLEALIAS1 JOIN SCHEMANAME2.TABLENAME2 TABLEALIAS2 ON TABLEALIAS2.COLUMNNAME2-1 = TABLEALIAS1.COLUMNNAME1 - 1", sqlStatement);
        }
    }
}
