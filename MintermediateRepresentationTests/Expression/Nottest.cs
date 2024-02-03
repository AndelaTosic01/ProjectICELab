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
public class Nottest
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
                        new Edge(new Variable("out", new BooleanType()))
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

    static IEnumerable<object[]> WrongInputParams
    {
        get
        {
            return new[]
            {
                new object[] //input length != 1
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())),
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
                new object[] // output not Integer or Boolean 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new StringType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType()))
                    }
                },
                new object[] // output note Integer or Boolean 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType()))
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
                        new Edge(new Variable("a", new IntegerType(),1))
                    },
                    0
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new BooleanType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),1))
                    },
                    false
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),0))
                    },
                    1
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new BooleanType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),0))
                    },
                    true
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(WrongInputParams))]
    public void TestConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularInputException>(() => new Not(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(WrongOutputParams))]
    public void TestOutputConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularOutputException>(() => new Not(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(NullParams))]
    public void TestNullConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<NullReferenceException>(() => new Not(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void TestCalcResult(IOutput[] output, IInput[] inputs, object result)
    {
        Operation op = new Not(output, inputs);
        op.CalcResult();
        
        Assert.AreEqual(((Edge)output[0]).Value, result);
    }
}