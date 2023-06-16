using Microsoft.CodeAnalysis; // [Generator], GeneratorInitializationContext
                              // ISourceGenerator, GeneratorExecutionContext

namespace Packt.Shared;

[Generator]
public class MessageSourceGenerator : ISourceGenerator
{
  public void Execute(GeneratorExecutionContext execContext)
  {
    IMethodSymbol mainMethod = execContext.Compilation
      .GetEntryPoint(execContext.CancellationToken);

    string sourceCode = $@"// source-generated code
static partial class {mainMethod.ContainingType.Name}
{{
  static partial void Message(string message)
  {{
    System.Console.WriteLine($""Generator says: '{{message}}'"");
  }}
}}
";
    string typeName = mainMethod.ContainingType.Name;
    execContext.AddSource($"{typeName}.Methods.g.cs", sourceCode);
  }

  public void Initialize(GeneratorInitializationContext initContext)
  {
    // this source generator does not need any initialization
  }
}
