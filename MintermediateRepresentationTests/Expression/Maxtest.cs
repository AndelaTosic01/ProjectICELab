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
public class Maxtest
{

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
                        new Edge(new Variable("out", new IntegerType())),
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
                new object[] // output not Numeric 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new StringType())),
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())),
                        new Edge(new Variable("b", new BooleanType()))
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
                new object[] //missing output
                {
                    new IOutput[]
                    {
                        null
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType()))
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
                        new Edge(new Variable("a", new IntegerType(),1)),
                        new Edge(new Variable("b", new RealType(),5.3)),
                        new Edge(new Variable("b", new RealType(),3.0)),
                        new Edge(new Variable("b", new IntegerType(),4))
                    },
                    5L
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),1)),
                        new Edge(new Variable("b", new RealType(),3.3)),
                        new Edge(new Variable("b", new RealType(),4.2)),
                        new Edge(new Variable("b", new IntegerType(),4))
                    },
                    4.2
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(WrongInputParams))]
    public void TestInputConstructor(IOutput[] output,IInput[] inputs)
    {
        Assert.ThrowsException<IrregularInputException>(() => new Max(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(WrongOutputParams))]
    public void TestOutputConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularOutputException>(() => new Max(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(NullParams))]
    public void TestNullConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<NullReferenceException>(() => new Max(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void TestCalcResult(IOutput[] output, IInput[] inputs, object result)
    {
        Operation op = new Max(output, inputs);
        op.CalcResult();
        
        Assert.AreEqual(((Edge)output[0]).Value, result);
    }
}