using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIntermediateRepresentation.RVSDG;
using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Expression;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MintermediateRepresentationTests.Expression;

[TestClass]
public class Multest
{
    static IEnumerable<object[]> NullParams
    {
        get
        {
            return new[]
            {
                new object[] //missing input
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        null
                    }
                },
                new object[] //output null
                {
                    new IOutput[]
                    {
                        null
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())),
                        new Edge(new Variable("b", new IntegerType()))
                    }
                }
            };
        }
    }

    static IEnumerable<object[]> WrongInputParams
    {
        get
        {
            return new[]
            {
                new object[] //input length < 2
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType()))
                    }
                }
            };
        }
    }

    static IEnumerable<object[]> WrongOutputParams
    {
        get
        {
            return new[]
            {
                new object[] // output type not numeric 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new StringType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())),
                        new Edge(new Variable("b", new IntegerType()))
                    }
                },
                new object[] // output not Numeric 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new BooleanType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())),
                        new Edge(new Variable("b", new BooleanType()))
                    }
                },
                new object[] //output.length != 1
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType())),
                        new Edge(new Variable("out1", new IntegerType())),
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())),
                        new Edge(new Variable("b", new BooleanType()))
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
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),4)),
                        new Edge(new Variable("b", new RealType(),3.2))
                    },
                    12L //????? 13 ????? 4*3.2=12.8=13 OR 4*3.2=4*3=12
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),2)),
                        new Edge(new Variable("b", new RealType(),3.2)),
                        new Edge(new Variable("c", new IntegerType(), 2))
                    },
                    12.8
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(WrongInputParams))]
    public void TestConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularInputException>(() => new Mul(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(WrongOutputParams))]
    public void TestOutputConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularOutputException>(() => new Mul(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(NullParams))]
    public void TestNullConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<NullReferenceException>(() => new Mul(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void TestCalcResult(IOutput[] output, IInput[] inputs, object result)
    {
        Operation subOperation = new Mul(output, inputs);
        subOperation.CalcResult();
        
        Assert.AreEqual(((Edge)output[0]).Value, result);
    }
}