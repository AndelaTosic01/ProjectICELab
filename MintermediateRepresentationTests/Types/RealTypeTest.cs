using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG.Types;

namespace MintermediateRepresentationTests.Types
{

    [TestClass]
    public class RealTypeTest
    {
        [TestMethod]
        public void TestClone()
        {
            IDataType type = new RealType();
            Assert.AreEqual(type, type.Clone());
        }

        [TestMethod]
        public void TestEquals()
        {
            IDataType type = new RealType();
            Assert.IsFalse(type.Equals(new IntegerType()));
            Assert.IsTrue(type.Equals(new RealType()));
            Assert.IsFalse(type.Equals(new StringType()));
            Assert.IsFalse(type.Equals(new BooleanType()));
        }
        
        [TestMethod]
        public void TestHashCode()
        {
            IDataType type = new RealType();
            Assert.AreEqual(1, type.GetHashCode());
            Assert.AreEqual(new RealType().GetHashCode(), type.GetHashCode());
        }
        
        [TestMethod]
        public void TestIsAssignable()
        {
            IDataType type = new RealType();
            Assert.IsFalse(type.IsAssignable(1));
            Assert.IsFalse(type.IsAssignable(1L));
            Assert.IsTrue(type.IsAssignable(1f));
            Assert.IsTrue(type.IsAssignable(1.0));
            Assert.IsFalse(type.IsAssignable("1"));
            Assert.IsFalse(type.IsAssignable(true));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithIntegerType()
        {
            IDataType type = new RealType();
            Assert.IsTrue(type.IsCompatible(new IntegerType()));
            Assert.AreEqual(0L, IntegerType.CastValue(0.1f));
            Assert.AreEqual(1L, IntegerType.CastValue(1.1f));
            Assert.AreEqual(-1L, IntegerType.CastValue(-1.1f));
            Assert.AreEqual(0L, IntegerType.CastValue(0.1));
            Assert.AreEqual(1L, IntegerType.CastValue(1.1));
            Assert.AreEqual(-1L, IntegerType.CastValue(-1.1));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithRealType()
        {
            IDataType type = new RealType();
            Assert.IsTrue(type.IsCompatible(new RealType()));
            Assert.AreEqual(0.1, RealType.CastValue(0.1f));
            Assert.AreEqual(1.1, RealType.CastValue(1.1f));
            Assert.AreEqual(-1.1, RealType.CastValue(-1.1f));
            Assert.AreEqual(0.1, RealType.CastValue(0.1));
            Assert.AreEqual(1.1, RealType.CastValue(1.1));
            Assert.AreEqual(-1.1, RealType.CastValue(-1.1));
        }
                
        [TestMethod]
        public void TestIsCompatibleWithBooleanType()
        {
            IDataType type = new RealType();
            Assert.IsTrue(type.IsCompatible(new BooleanType()));
            Assert.AreEqual(false, BooleanType.CastValue(0.0f));
            Assert.AreEqual(true, BooleanType.CastValue(1.1f));
            Assert.AreEqual(true, BooleanType.CastValue(-1.1f));
            Assert.AreEqual(false, BooleanType.CastValue(0.0));
            Assert.AreEqual(true, BooleanType.CastValue(1.1));
            Assert.AreEqual(true, BooleanType.CastValue(-1.1));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithStringType()
        {
            IDataType type = new RealType();
            Assert.IsTrue(type.IsCompatible(new StringType()));
            Assert.AreEqual("0.1", StringType.CastValue(0.1f));
            Assert.AreEqual("1.1", StringType.CastValue(1.1f));
            Assert.AreEqual("-1.1", StringType.CastValue(-1.1f));
            Assert.AreEqual("0.1", StringType.CastValue(0.1));
            Assert.AreEqual("1.1", StringType.CastValue(1.1));
            Assert.AreEqual("-1.1", StringType.CastValue(-1.1));
        }
        
        [TestMethod]
        public void TestGetDefaultValue()
        {
            Assert.AreEqual(0.0, RealType.GetDefaultValue());
        }
    }
}