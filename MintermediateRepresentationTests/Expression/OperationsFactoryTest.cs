using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG;
using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Expression;
using MIntermediateRepresentation.RVSDG.Types;

namespace MintermediateRepresentationTests.Expression;

[TestClass]
public class OperationsFactoryTest {
    static IEnumerable<object[]> WrongParams
    {
        get
        {
            return new[]
            {
                new object[] //wrong name or not existing name
                {
                    "nor",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(), 1)),
                        new Edge(new Variable("b", new BooleanType(), 1))
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
                    "Sum",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),2)),
                        new Edge(new Variable("b", new RealType(),2.5))
                    },
                    4.5
                },
                new object[]    
                {
                    "Sub",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),4)),
                        new Edge(new Variable("b", new RealType(),2.5))
                    },
                    1.5
                },
                new object[]
                {
                    "Mul",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),4)),
                        new Edge(new Variable("b", new RealType(),2.2))
                    },
                    8L
                },
                new object[]
                {
                    "Div",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),4)),
                        new Edge(new Variable("b", new RealType(),2.0))
                    },
                    2.0
                },
                new object[]
                {
                    "Max",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),4)),
                        new Edge(new Variable("b", new RealType(),2.5))
                    },
                    4.0
                },
                new object[]
                {
                    "Min",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),4)),
                        new Edge(new Variable("b", new RealType(),2.5))
                    },
                    2.5
                },
                new object[]
                {
                    "and",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new BooleanType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),1)),
                        new Edge(new Variable("b", new IntegerType(),1))
                    },
                    true
                },
                new object[]
                {
                    "not",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("b", new IntegerType(),1))
                    },
                    0
                },
                new object[]
                {
                    "or",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new BooleanType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),0)),
                        new Edge(new Variable("b", new IntegerType(),1))
                    },
                    true
                },
                new object[]
                {
                    "xor",
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),1)),
                        new Edge(new Variable("b", new BooleanType(),true))
                    },
                    0L
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(WrongParams))]
    public void TestConstructor(String operation, IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<ArgumentException>(() => OperationsFactory.MakeOperation(operation, output, inputs));
        
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void MakeOperationTest(string operation, IOutput[] output, IInput[] inputs, object result)
    {
        Operation op = OperationsFactory.MakeOperation(operation, output, inputs);
        op.CalcResult();
        
        Assert.AreEqual(((Edge)output[0]).Value, result);
    }
}