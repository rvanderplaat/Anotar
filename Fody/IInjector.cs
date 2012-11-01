using Mono.Cecil;

public interface IInjector
{
    TypeReference LoggerType { get; }
    MethodReference DebugMethod { get; set; }
    MethodReference DebugExceptionMethod { get; set; }
    MethodReference InfoMethod { get; set; }
    MethodReference InfoExceptionMethod { get; set; }
    MethodReference WarnMethod { get; set; }
    MethodReference WarnExceptionMethod { get; set; }
    MethodReference ErrorMethod { get; set; }
    MethodReference ErrorExceptionMethod { get; set; }
    void AddField(TypeDefinition type, MethodDefinition constructor, FieldDefinition fieldDefinition);
    string ReferenceName { get; }
    void Init(AssemblyDefinition reference, ModuleDefinition moduleDefinition);
}