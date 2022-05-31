﻿using Eto.Parse;
using Eto.Parse.Grammars;
using Stride.Shaders.Parsing;
using Stride.Shaders.Parsing.Grammars.Expression;
using System.Diagnostics;
using System.Linq;
using Stride.Core.Shaders.Grammar;
using Stride.Core.Shaders;
using Stride.Core.Shaders.Parser;
using Stride.Core.Shaders.Grammar.Stride;

var shaderf = File.ReadAllText("./SDSL/shader.sdsl");
var child = File.ReadAllText("./SDSL/InheritExample/Child.sdsl");
var parent = File.ReadAllText("./SDSL/InheritExample/Parent.sdsl");

// var shaderf = File.ReadAllText("../../../SDSL/shader2.sdsl");

var sdsl = new Stride.Shaders.Parsing.ShaderMixinParser();
//sdsl.Grammar.Using(sdsl.Grammar.CastExpression);
var s = new Stopwatch();
var parser = new ExpressionParser();
var match2 = sdsl.Parse(shaderf);
// sdsl.AddMacro("STRIDE_MULTISAMPLE_COUNT", 5);


s.Start();
var match = sdsl.Parse(shaderf);
s.Stop();

// sdsl.PrintParserTree();

Console.WriteLine(shaderf);
Console.WriteLine(new string('*', 64));
Console.WriteLine(match);
Console.WriteLine($"parsing time : {s.Elapsed}");

var grammar = ShaderParser.GetGrammar<StrideGrammar>();

var p = ShaderParser.GetParser<StrideGrammar>();
p.Parse(shaderf,"./SDSL/shader2.sdsl");
s.Start();
var result = p.Parse(shaderf,"./SDSL/shader2.sdsl");
s.Stop();
Console.WriteLine($"irony parsing time : {s.Elapsed}");


