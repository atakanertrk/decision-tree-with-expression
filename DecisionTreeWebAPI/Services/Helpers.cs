using Flee.PublicTypes;
using Microsoft.CodeAnalysis.Scripting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DecisionTreeWebAPI
{
    public class Helpers
    {
        public static void AddVariableToContextByType(ExpressionContext context, Variable variable)
        {
            var type = variable.Value.GetType().FullName;
            switch (variable.ValueType)
            {
                case ValueType.Number:
                    context.Variables.Add(variable.Name, ((JsonElement)(variable.Value)).ToObject<decimal>());
                    break;
                case ValueType.String:
                    context.Variables.Add(variable.Name, ((JsonElement)(variable.Value)).ToObject<string>());
                    break;
                case ValueType.Date:
                    context.Variables.Add(variable.Name, ((JsonElement)(variable.Value)).ToObject<DateTime>());
                    break;
                default:
                    context.Variables.Add(variable.Name, variable.Value);
                    break;
            }
        }

        public static void SetContextOptions(ExpressionContext context)
        {
            context.ParserOptions.FunctionArgumentSeparator = ',';
            context.ParserOptions.RecreateParser();
            context.Options.RealLiteralDataType = RealLiteralDataType.Decimal;
            context.Imports.AddType(typeof(Math));
            context.Imports.AddType(typeof(CustomMethods), "function");
        }

        public static ScriptOptions GetImports(EvaluateOrderCsharpReq request)
        {
            return ScriptOptions.Default
                .WithImports(request.Imports?.Any() == true ? request.Imports : new List<string>() { "System" })
                .WithReferences(typeof(CustomMethods).Assembly);
        }

        public static ScriptOptions GetImports(EvaluateOrderCsharpTreeReq request)
        {
            return ScriptOptions.Default
                .WithImports(request.Imports?.Any() == true ? request.Imports : new List<string>() { "System" })
                .WithReferences(typeof(CustomMethods).Assembly);
        }
    }
}
