using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG.Types;

namespace MintermediateRepresentationTests.Types
{

    [TestClass]
    public class BooleanTypeTest
    {
        [TestMethod]
        public void TestClone()
        {
            IDataType type = new BooleanType();
            Assert.AreEqual(type, type.Clone());
        }

        [TestMethod]
        public void TestEquals()
        {
            IDataType type = new BooleanType();
            
            Assert.IsFalse(type.Equals(new IntegerType()));
            Assert.IsFalse(type.Equals(new RealType()));
            Assert.IsFalse(type.Equals(new StringType()));
            Assert.IsTrue(type.Equals(new BooleanType()));
        }
        
        [TestMethod]
        public void TestHashCode()
        {
            IDataType type = new BooleanType();
            Assert.AreEqual(2, type.GetHashCode());
            Assert.AreEqual(new BooleanType().GetHashCode(), type.GetHashCode());
        }
        
        [TestMethod]
        public void TestIsAssignable()
        {
            IDataType type = new BooleanType();
            Assert.IsFalse(type.IsAssignable(1));
            Assert.IsFalse(type.IsAssignable(1L));
            Assert.IsFalse(type.IsAssignable(1f));
            Assert.IsFalse(type.IsAssignable(1.0));
            Assert.IsFalse(type.IsAssignable("1"));
            Assert.IsTrue(type.IsAssignable(true));
        }
        
        
        [TestMethod]
        public void TestIsCompatibleWithIntegerType()
        {
            IDataType type = new BooleanType();
            Assert.IsTrue(type.IsCompatible(new IntegerType()));
            Assert.AreEqual(1, IntegerType.CastValue(true));
            Assert.AreEqual(0, IntegerType.CastValue(false));
            Assert.AreEqual(1L, IntegerType.CastValue(true));
            Assert.AreEqual(0L, IntegerType.CastValue(false));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithRealType()
        {
            IDataType type = new BooleanType();
            Assert.IsTrue(type.IsCompatible(new RealType()));
            Assert.AreEqual(1.0f, RealType.CastValue(true));
            Assert.AreEqual(0.0f, RealType.CastValue(false));
            Assert.AreEqual(1.0, RealType.CastValue(true));
            Assert.AreEqual(0.0, RealType.CastValue(false));
        }
                
        [TestMethod]
        public void TestIsCompatibleWithBooleanType()
        {
            IDataType type = new BooleanType();
            Assert.IsTrue(type.IsCompatible(new BooleanType()));
            Assert.AreEqual(true, BooleanType.CastValue(true));
            Assert.AreEqual(false, BooleanType.CastValue(false));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithStringType()
        {
            IDataType type = new BooleanType();
            Assert.IsTrue(type.IsCompatible(new StringType()));
            Assert.AreEqual("True", StringType.CastValue(true));
            Assert.AreEqual("False", StringType.CastValue(false));
        }
        
        [TestMethod]
        public void TestGetDefaultValue()
        {
            Assert.AreEqual(true, BooleanType.GetDefaultValue());
        }
    }
}