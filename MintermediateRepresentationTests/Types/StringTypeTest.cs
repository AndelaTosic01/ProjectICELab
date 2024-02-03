using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG.Types;

namespace MintermediateRepresentationTests.Types
{

    [TestClass]
    public class StringTypeTest
    {
        [TestMethod]
        public void TestClone()
        {
            IDataType type = new StringType();
            Assert.AreEqual(type, type.Clone());
        }

        [TestMethod]
        public void TestEquals()
        {
            IDataType type = new StringType();
            Assert.IsFalse(type.Equals(new IntegerType()));
            Assert.IsFalse(type.Equals(new RealType()));
            Assert.IsTrue(type.Equals(new StringType()));
            Assert.IsFalse(type.Equals(new BooleanType()));
        }
        
        [TestMethod]
        public void TestHashCode()
        {
            IDataType type = new StringType();
            Assert.AreEqual(3, type.GetHashCode());
            Assert.AreEqual(new StringType().GetHashCode(), type.GetHashCode());
        }
        
        [TestMethod]
        public void TestIsAssignable()
        {
            IDataType type = new StringType();
            Assert.IsFalse(type.IsAssignable(1));
            Assert.IsFalse(type.IsAssignable(1L));
            Assert.IsFalse(type.IsAssignable(1f));
            Assert.IsFalse(type.IsAssignable(1.0));
            Assert.IsTrue(type.IsAssignable("1"));
            Assert.IsFalse(type.IsAssignable(true));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithIntegerType()
        {
            IDataType type = new StringType();
            Assert.IsTrue(type.IsCompatible(new IntegerType()));
            Assert.AreEqual(0L, IntegerType.CastValue(""));
            Assert.AreEqual(0L, IntegerType.CastValue("abcds"));
            Assert.AreEqual(0L, IntegerType.CastValue("acd23"));
            Assert.AreEqual(0L, IntegerType.CastValue(""));
            Assert.AreEqual(0L, IntegerType.CastValue("0"));
            Assert.AreEqual(1L, IntegerType.CastValue("1"));
            Assert.AreEqual(-1L, IntegerType.CastValue("-1"));
            Assert.AreEqual(0L, IntegerType.CastValue("0.1"));
            Assert.AreEqual(0L, IntegerType.CastValue("1.1"));
            Assert.AreEqual(0L, IntegerType.CastValue("-1.1"));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithRealType()
        {
            IDataType type = new StringType();
            Assert.IsTrue(type.IsCompatible(new RealType()));
            Assert.AreEqual(0.0, RealType.CastValue(""));
            Assert.AreEqual(0.0, RealType.CastValue("abcds"));
            Assert.AreEqual(0.0, RealType.CastValue("acd23"));
            Assert.AreEqual(0.0, RealType.CastValue("0"));
            Assert.AreEqual(1.0, RealType.CastValue("1"));
            Assert.AreEqual(-1.0, RealType.CastValue("-1"));
            Assert.AreEqual(0.1, RealType.CastValue("0.1"));
            Assert.AreEqual(1.1, RealType.CastValue("1.1"));
            Assert.AreEqual(-1.1, RealType.CastValue("-1.1"));
        }
                
        [TestMethod]
        public void TestIsCompatibleWithBooleanType()
        {
            IDataType type = new StringType();
            Assert.IsFalse(type.IsCompatible(new BooleanType()));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue(""));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue("abcds"));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue("acd23"));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue("True"));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue("False"));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue("true"));
            Assert.ThrowsException<ArgumentException>(()=> BooleanType.CastValue("false"));
        }
        
        [TestMethod]
        public void TestIsCompatibleWithStringType()
        {
            IDataType type = new StringType();
            Assert.IsTrue(type.IsCompatible(new StringType()));
            Assert.AreEqual("", StringType.CastValue(""));
            Assert.AreEqual("abcds", StringType.CastValue("abcds"));
            Assert.AreEqual("acd23", StringType.CastValue("acd23"));
            Assert.AreEqual("0", StringType.CastValue("0"));
            Assert.AreEqual("1", StringType.CastValue("1"));
            Assert.AreEqual("-1", StringType.CastValue("-1"));
            Assert.AreEqual("0.1", StringType.CastValue("0.1"));
            Assert.AreEqual("1.1", StringType.CastValue("1.1"));
            Assert.AreEqual("-1.1", StringType.CastValue("-1.1"));
        }
        
        [TestMethod]
        public void TestGetDefaultValue()
        {
            Assert.AreEqual(String.Empty, StringType.GetDefaultValue());
        }
    }
}