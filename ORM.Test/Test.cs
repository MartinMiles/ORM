using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM.Results;

namespace ORM.Test
{
    [TestClass]
    public class DatabaseTest
    {
        private const string GetProcedureName = "GetModels";
        private const string InsertProcedureName = "InsertModel";
        private const string TruncateProcedureName = "Truncate";
        private const string DeleteProcedureName = "DeleteItem";

        [TestInitialize]
        public void Setup()
        {
            Truncate();
        }

        [TestMethod]
        public void SelectModelTests()
        {
            var actualModelId = InsertModel(ModelActual);
            var emptyModelId = InsertModel(ModelEmpty);

            VerifyModelActual(actualModelId);
            VerifyModelEmpty(emptyModelId);
        }

        [TestMethod]
        public void StoredProcedureTests()
        {
            int actualModelId = InsertModel(ModelActual);
            int emptyModelId = InsertModel(ModelEmpty);

            Delete(actualModelId);
            Delete(emptyModelId);
        }

        [TestMethod]
        public void FaultyStoredProcedureTests()
        {
            ProcedureResult result;

            // 1. Try to call procedure without mandatory parameter
            result = Database.StoredProcedure(DeleteProcedureName);
            Assert.AreEqual(result.Status, Status.Error);
            Assert.IsTrue(result.ErrorMessage.Length > 0);

            // 2. Try to pass parameter of wrong type
            result = Database.StoredProcedure(DeleteProcedureName, new SqlParameter("@Id", "Should be integer"));
            Assert.AreEqual(result.Status, Status.Error);
            Assert.IsTrue(result.ErrorMessage.Length > 0);

        }

        #region Private

        #region Methods

        private static void Truncate()
        {
            ProcedureResult result;
            result = Database.StoredProcedure(TruncateProcedureName);
            Assert.IsInstanceOfType(result, typeof (Result));
            Assert.IsNotInstanceOfType(result, typeof (ProcedureIdentityResult));
            Assert.IsNotInstanceOfType(result, typeof (SelectResult<Model>));
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(result.Status, Status.Success);
        }

        private static int InsertModel(Model model)
        {
            ProcedureResult result = Database.StoredProcedure(InsertProcedureName, model.ToSqlParameters(), "@Id");

            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsInstanceOfType(result, typeof(ProcedureIdentityResult));
            Assert.IsNotInstanceOfType(result, typeof(SelectResult<>));
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(result.Status, Status.Success);

            return (result as ProcedureIdentityResult).Identity;
        }

        private static void Delete(int id)
        {
            ProcedureResult result = Database.StoredProcedure(DeleteProcedureName, new SqlParameter("@Id", id));

            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNotInstanceOfType(result, typeof(ProcedureIdentityResult));
            Assert.IsNotInstanceOfType(result, typeof(SelectResult<>));
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(result.Status, Status.Success);
        }

        private static IEnumerable<Model> GetAllModels()
        {
            var result = DataBase<Model>.GetModel<Model>(GetProcedureName);
            var list = result.Values;

            Assert.IsNotNull(result);
            Assert.IsNotNull(list);
            Assert.IsInstanceOfType(result, typeof(SelectResult<Model>));
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(list.Count(), 2, "Should be 2 records by default");

            return list;
        }

        private void VerifyModelActual(int id)
        {
            var notNullable = GetAllModels().First(i => i.Id == id);

            Assert.AreEqual(notNullable.Id, id);
            Assert.AreEqual(notNullable.Int, ModelActual.Int);
            Assert.AreEqual(notNullable.Nvarchar255, ModelActual.Nvarchar255);
            Assert.AreEqual(notNullable.NvarcharMax, ModelActual.NvarcharMax);
            Assert.AreEqual(notNullable.Ntext, ModelActual.Ntext);
            Assert.AreEqual(notNullable.DateTime, ModelActual.DateTime);
            Assert.AreEqual(notNullable.Money, ModelActual.Money);
            Assert.AreEqual(notNullable.Bit, ModelActual.Bit);
        }
        private static void VerifyModelEmpty(int id)
        {
            var nullable = GetAllModels().First(i => i.Id == id);

            Assert.IsNull(nullable.Int);
            Assert.IsNull(nullable.Nvarchar255);
            Assert.IsNull(nullable.NvarcharMax);
            Assert.IsNull(nullable.Ntext);
            Assert.IsNull(nullable.DateTime);
            Assert.IsNull(nullable.Money);
            Assert.IsNull(nullable.Bit);
        }


        #endregion

        #region Properties

        private Model ModelActual
        {
            get
            {
                return new Model
                    {
                        Int = 123,
                        Nvarchar255 = "Nvarchar255",
                        NvarcharMax = "NvarcharMax",
                        Ntext = "Ntext",
                        DateTime = new DateTime(2013, 12, 26, 14, 59, 00, 987),
                        Money = (decimal) 1230.99,
                        Bit = false
                    };
            }
        }

        private Model ModelEmpty
        {
            get
            {
                return new Model
                    {
                        Int = null,
                        Nvarchar255 = null,
                        NvarcharMax = null,
                        Ntext = null,
                        DateTime = null,
                        Money = null,
                        Bit = null
                    };
            }
        }

        #endregion

        #endregion
    }
}
