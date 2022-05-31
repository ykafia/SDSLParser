using Stride.Shaders.Parsing;
using Stride.Shaders.Parsing.AST.Shader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Shaders;

public class ShaderSourceString
{
    public string Code { get; set; }
    public string ClassName { get => AST != null ? AST.Name : string.Empty; }
    public ShaderProgram? AST { get; set; }
    string[] EntryPointNames = {
        "PSMain",
        "VSMain",
        "GSMain",
        "HSMain",
        "DSMain",
        "CSMain"
    };

    ShaderMixinParser Parser { get; set; }

    public ShaderSourceString(string code, ShaderMixinParser parser)
    {
        Code = code;
        Parser = parser;
    }

    public void Parse()
    {
        AST = (ShaderProgram)Parser.Parse(Code);
    }

    public IEnumerable<ShaderValueDeclaration> GetStreamValues()
    {
        if (AST is not null)
            return
                from e in AST.Body
                where e is ShaderValueDeclaration v 
                && v.IsStream
                select e as ShaderValueDeclaration;
        else
            throw new Exception("AST is null");
    }
    public IEnumerable<ShaderMethod> GetEntryPoints()
    {
        if (AST is not null)
            return
                from e in AST.Body
                where e is ShaderMethod method
                && EntryPointNames.Contains(method.Name)
                select e as ShaderMethod;
        else
            throw new Exception("AST is null");
    }
    public IEnumerable<ShaderMethod> GetMethods()
    {
        if (AST is not null)
            return
                from e in AST.Body
                where e is ShaderMethod method
                && !EntryPointNames.Contains(method.Name)
                select e as ShaderMethod;
        else
            throw new Exception("AST is null");
    }


}
