using Ceemas.DataAccess.SqlStatementBuilder;
using Ceemas.DataAccess.SqlStatementBuilder.Elements;
using Ceemas.DataAccess.SqlStatementBuilder.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceemas.DataAccess.Database.Test.Ceemas.DataAccess.SqlStatementBuilder.Visitors
{
    [TestClass]
    public class ExpressionElementVisitorTest
    {
        [TestMethod]
        public void TestColumnToValue()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME", "ALIASNAME");
            var rightElement = Element.Value("VALUE");
            var operatorElement = Element.Equal(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME = VALUE", statement);
        }

        [TestMethod]
        public void TestColumnToColumn()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.Equal(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 = TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void NotEqualTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.NotEqual(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 <> TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void GreaterThanTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.GreaterThan(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 > TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void GreaterThanEqualTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.GreaterThanEqual(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 >= TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void LessThanEqualTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.LessThan(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 < TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void LessThanEqualnEqualTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.LessThanEqual(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 <= TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void TestValueToValue()
        {
            var leftElement = Element.Value("VALUE1");
            var rightElement = Element.Value("VALUE2");
            var operatorElement = Element.Equal(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("VALUE1 = VALUE2", statement);
        }

        [TestMethod]
        public void TestValueToValueAndValue()
        {
            var leftElement = Element.Equal(Element.Value("VALUE1"), Element.Value("VALUE2"));
            var rightElement = Element.Equal(Element.Value("VALUE3"), Element.Value("VALUE4"));
            var operatorElement = Element.And(leftElement, rightElement);


            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("VALUE1 = VALUE2 AND VALUE3 = VALUE4", statement);
        }

        [TestMethod]
        public void TestValueToValueOrValue()
        {
            var leftElement = Element.Equal(Element.Value("VALUE1"), Element.Value("VALUE2"));
            var rightElement = Element.Equal(Element.Value("VALUE3"), Element.Value("VALUE4"));
            var operatorElement = Element.Or(leftElement, rightElement);


            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("VALUE1 = VALUE2 OR VALUE3 = VALUE4", statement);
        }

        [TestMethod]
        public void TestExpressionAndExpressionOrExpressionTest()
        {
            var andLeftElement = Element.Equal(Element.Value("VALUE1"), Element.Value("VALUE2"));
            var andRightElement = Element.Equal(Element.Value("VALUE3"), Element.Value("VALUE4"));
            var orRightElement = Element.Equal(Element.Value("VALUE3"), Element.Value("VALUE4"));

            var andOperatorElement = Element.And(andLeftElement, andRightElement);
            var orOperantorElement = Element.Or(andOperatorElement, orRightElement);


            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(orOperantorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("VALUE1 = VALUE2 AND VALUE3 = VALUE4 OR VALUE3 = VALUE4", statement);
        }

        [TestMethod]
        public void TestBlockStartExpressionAndExpressionBlockEndOrExpressionTest()
        {
            var andLeftElement = Element.Equal(Element.Value("VALUE1"), Element.Value("VALUE2"));
            var andRightElement = Element.Equal(Element.Value("VALUE3"), Element.Value("VALUE4"));
            var orRightElement = Element.Equal(Element.Value("VALUE3"), Element.Value("VALUE4"));

            var blockElement = Element.Block(Element.And(andLeftElement, andRightElement));
            var orOperantorElement = Element.Or(blockElement, orRightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(orOperantorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("(VALUE1 = VALUE2 AND VALUE3 = VALUE4) OR VALUE3 = VALUE4", statement);
        }

        [TestMethod]
        public void InStatementTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME", "ALIASNAME");
            var rightsElements = new ValueElement[] { Element.Value("VALUE3"), Element.Value("VALUE4") };

            var orOperantorElement = Element.In(leftElement, rightsElements);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(orOperantorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME IN (VALUE3, VALUE4)", statement);
        }

        [TestMethod]
        public void NotInStatementTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME", "ALIASNAME");
            var rightsElements = new ValueElement[] { Element.Value("VALUE3"), Element.Value("VALUE4") };

            var orOperantorElement = Element.Not(Element.In(leftElement, rightsElements));

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(orOperantorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME NOT IN (VALUE3, VALUE4)", statement);
        }

        [TestMethod]
        public void AddTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.Add(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 + TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void SubtractTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.Subtract(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 - TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.Multiply(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 * TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void DivideTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.Divide(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 / TABLEALIAS.COLUMNNAME1", statement);
        }

        [TestMethod]
        public void ModuloTest()
        {
            var leftElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var rightElement = Element.TableColumn("TABLEALIAS", "COLUMNNAME1", "ALIASNAME2");
            var operatorElement = Element.Modulo(leftElement, rightElement);

            var sqlBuilder = new SqlStringBuilder();
            var expressionElementVisitor = new ExpressionElementVisitor(sqlBuilder);

            expressionElementVisitor.Visit(operatorElement);
            var statement = sqlBuilder.ToString();

            Assert.AreEqual("TABLEALIAS.COLUMNNAME1 % TABLEALIAS.COLUMNNAME1", statement);
        }
    }
}
