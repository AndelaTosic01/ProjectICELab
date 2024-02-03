using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG.Types;

namespace MintermediateRepresentationTests.Types
{

    [TestClass]
    public class IntegerTypeTest
    {
        [TestMethod]
        public void TestClone()
        {
            IDataType type = new IntegerType();
            Assert.AreEqual(type, type.Clone());
        }

        [TestMethod]
        public void TestEquals()
        {
            IDataType type = new IntegerType();
            Assert.IsTrue(type.Equals(new IntegerType()));
            Assert.IsFalse(type.Equals(new RealType()));
            Assert.IsFalse(type.Equals(new StringType()));
            Assert.IsFalse(type.Equals(new BooleanType()));
        }
        
        [TestMethod]
        public void TestHashCode()
        {
            IDataType type = new IntegerType();
            Assert.AreEqual(0, type.GetHashCode());
            Assert.AreEqual(new IntegerType().GetHashCode(), type.GetHashCode());
        }
        
        [TestMethod]
        public void TestIsAssignable()
        {
            IDataType type = new IntegerType();
            Assert.IsTrue(type.IsAssignable(1));
            Assert.IsTrue(type.IsAssignable(1L));
            Assert.IsFalse(type.IsAssignable(1f));
            Assert.IsFalse(type.IsAssignable(1.0));
            Assert.IsFalse(type.IsAssignable("1"));
            Assert.IsFalse(type.IsAssignable(true));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithIntegerType()
        {
            IDataType type = new IntegerType();
            Assert.IsTrue(type.IsCompatible(new IntegerType()));
            Assert.AreEqual(0L, IntegerType.CastValue(0));
            Assert.AreEqual(1L, IntegerType.CastValue(1));
            Assert.AreEqual(-1L, IntegerType.CastValue(-1));
            Assert.AreEqual(0L, IntegerType.CastValue(0L));
            Assert.AreEqual(1L, IntegerType.CastValue(1L));
            Assert.AreEqual(-1L, IntegerType.CastValue(-1L));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithRealType()
        {
            IDataType type = new IntegerType();
            Assert.IsTrue(type.IsCompatible(new RealType()));
            Assert.AreEqual(0.0, RealType.CastValue(0));
            Assert.AreEqual(1.0, RealType.CastValue(1));
            Assert.AreEqual(-1.0, RealType.CastValue(-1));
            Assert.AreEqual(0.0, RealType.CastValue(0L));
            Assert.AreEqual(1.0, RealType.CastValue(1L));
            Assert.AreEqual(-1.0, RealType.CastValue(-1L));
        }
                
        [TestMethod]
        public void TestIsCompatibleWithBooleanType()
        {
            IDataType type = new IntegerType();
            Assert.IsTrue(type.IsCompatible(new BooleanType()));
            Assert.AreEqual(false, BooleanType.CastValue(0));
            Assert.AreEqual(true, BooleanType.CastValue(1));
            Assert.AreEqual(true, BooleanType.CastValue(-1));
            Assert.AreEqual(false, BooleanType.CastValue(0L));
            Assert.AreEqual(true, BooleanType.CastValue(1L));
            Assert.AreEqual(true, BooleanType.CastValue(-1L));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithStringType()
        {
            IDataType type = new IntegerType();
            Assert.IsTrue(type.IsCompatible(new StringType()));
            Assert.AreEqual("0", StringType.CastValue(0));
            Assert.AreEqual("1", StringType.CastValue(1));
            Assert.AreEqual("-1", StringType.CastValue(-1));
            Assert.AreEqual("0", StringType.CastValue(0L));
            Assert.AreEqual("1", StringType.CastValue(1L));
            Assert.AreEqual("-1", StringType.CastValue(-1L));
        }
        
        [TestMethod]
        public void TestGetDefaultValue()
        {
            Assert.AreEqual(0, IntegerType.GetDefaultValue());
        }
    }
}