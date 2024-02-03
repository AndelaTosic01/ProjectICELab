using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Expression;
using MIntermediateRepresentation.RVSDG.Types;

namespace MintermediateRepresentationTests.Expression;

[TestClass]
public class Sumtest
{

    static IEnumerable<object[]> WrongParams
    {
        get
        {
            return new[]
            {
                new object[] //missing input
                {
                    new Variable("out", new IntegerType()), 
                    null
                },
                new object[] //input length < 2
                {
                    new Variable("out", new IntegerType()), 
                    new DataDeclaration[]
                    {
                        new Variable("a", new IntegerType())
                    }
                },
                new object[] // output type not supported 
                {
                    new Variable("out", new BooleanType()), 
                    new DataDeclaration[]
                    {
                        new Variable("a", new IntegerType()),
                        new Variable("b", new IntegerType())
                    }
                }
            };
        }
    }

    static IEnumerable<object[]> CalcResultParam
    {
        get
        {
            return new[]
            {
                new object[]
                {
                    new Variable("out", new IntegerType()), 
                    new DataDeclaration[]
                    {
                        new Variable("a", new IntegerType(),2),
                        new Variable("b", new RealType(),2.5),
                        new Variable("c", new StringType(), "2")
                    },
                    6L
                },
                new object[]
                {
                    new Variable("out", new RealType()), 
                    new DataDeclaration[]
                    {
                        new Variable("a", new IntegerType(),2),
                        new Variable("b", new RealType(),2.5),
                        new Variable("c", new StringType(), "2")
                    },
                    6.5
                },
                new object[]
                {
                    new Variable("out", new StringType()), 
                    new DataDeclaration[]
                    {
                        new Variable("a", new IntegerType(),2),
                        new Variable("b", new RealType(),2.5),
                        new Variable("c", new StringType(), "2")
                    },
                    "22.52"
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(WrongParams))]
    public void TestConstructor(DataDeclaration output, DataDeclaration[] inputs)
    {
        Assert.ThrowsException<ArgumentException>(() => new Sum(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void TestCalcResult(DataDeclaration output, DataDeclaration[] inputs, object result)
    {
        Operation op = new Sum(output, inputs);
        op.CalcResult();
        
        Assert.AreEqual(output.Value, result);
    }
}