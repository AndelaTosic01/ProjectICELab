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
public class Andtest
{
    static IEnumerable<object[]> WrongParamsInput
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

    static IEnumerable<object[]> WrongParamsOutput
    {
        get
        {
            return new[]
            {
                new object[] // output type not supported 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new StringType()))
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
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType())) as IInput,
                        new Edge(new Variable("b", new BooleanType())) as IInput
                    }
                },
                new object[] // output.length!=1 
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new StringType())),
                        new Edge(new Variable("out2", new StringType()))
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

    static IEnumerable<object[]> ParamsNull
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
                        new Edge(new Variable("b", new BooleanType(),true))
                    },
                    1L,
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),1)),
                        new Edge(new Variable("b", new BooleanType(),false))
                    },
                    0L,
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),0)),
                        new Edge(new Variable("b", new BooleanType(),true))
                    },
                    0L
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new BooleanType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),0)),
                        new Edge(new Variable("b", new BooleanType(),false))
                    },
                    false,
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(ParamsNull))]
    public void TestConstructorNull(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<NullReferenceException>(() => new And(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(WrongParamsInput))]
    public void TestConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularInputException>(() => new And(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(WrongParamsOutput))]
    public void TestConstructor2(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularOutputException>(() => new And(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void TestCalcResult(IOutput[] output, IInput[] inputs, object result)
    {
        Operation op = new And(output, inputs);
        op.CalcResult();
        
        Assert.AreEqual(((Edge)output[0]).Value, result);
    }
}