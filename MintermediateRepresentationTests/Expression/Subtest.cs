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
public class Subtest
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
                new object[] //missing input
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
                new object[] // output type not supported 
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
                new object[]    //output more than one
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType())),
                        new Edge(new Variable("out2", new IntegerType()))
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
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new IntegerType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),5)),
                        new Edge(new Variable("b", new RealType(),1.5))
                    },
                    3L
                },
                new object[]
                {
                    new IOutput[]
                    {
                        new Edge(new Variable("out", new RealType()))
                    },
                    new IInput[]
                    {
                        new Edge(new Variable("a", new IntegerType(),6)),
                        new Edge(new Variable("b", new RealType(),2.5)),
                        new Edge(new Variable("c", new IntegerType(), 1))
                    },
                    2.5
                }
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(NullParams))]
    public void TestNullConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<NullReferenceException>(() => new Sub(output, inputs));
    }
    [TestMethod]
    [DynamicData(nameof(WrongInputParams))]
    public void TestInputConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularInputException>(() => new Sub(output, inputs));
    }
    [TestMethod]
    [DynamicData(nameof(WrongOutputParams))]
    public void TestOutputConstructor(IOutput[] output, IInput[] inputs)
    {
        Assert.ThrowsException<IrregularOutputException>(() => new Sub(output, inputs));
    }

    [TestMethod]
    [DynamicData(nameof(CalcResultParam))]
    public void TestCalcResult(IOutput[] output, IInput[] inputs, object result)
    {
        Operation subOperation = new Sub(output, inputs);
        subOperation.CalcResult();
        
        Assert.AreEqual(((Edge)output[0]).Value, result);
    }
}