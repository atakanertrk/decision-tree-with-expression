using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flee.PublicTypes;
using Flee.CalcEngine.PublicTypes;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace DecisionTreeWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Expression : ControllerBase
    {
        [HttpPost]
        public IActionResult EvaluateOrder([FromBody] EvaluateOrderReq request)
        {
            var expDefList = request.ExpressionDefinitions.OrderBy(x => x.ExecuteOrder).ToList();

            CalculationEngine engine = new CalculationEngine();
            ExpressionContext context = new ExpressionContext();
            Helpers.SetContextOptions(context);

            foreach (var globalVar in request.GlobalVariables)
            {
                Helpers.AddVariableToContextByType(context, globalVar);
            }

            foreach (var item in expDefList)
            {
                engine.Add(item.ExpressionName, item.Expression, context);
            }

            var name = expDefList.Last().ExpressionName;
            var result = engine.GetResult(name);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> EvaluateOrderCsharp([FromBody] EvaluateOrderCsharpReq request)
        {
            ScriptState<object> state = null;
            for (int i = 0; i < request.Execute.Count; i++)
            {
                var definition = request.ExpressionDefinitions.First(x => x.ExpressionName == request.Execute[i]);
                try
                {
                    if (i == 0)
                    {
                        state = await CSharpScript.RunAsync(definition.Expression, Helpers.GetImports(request));
                    }
                    else
                    {
                        state = await state.ContinueWithAsync(definition.Expression);
                    }
                }
                catch (EvaluationStoppedException ex)
                {
                    break;
                }
            }
            state = await state.ContinueWithAsync(request.OutputExpression);
            return Ok(state.ReturnValue);
        }

        [HttpPost]
        public async Task<IActionResult> EvaluateOrderCsharpTree([FromBody] EvaluateOrderCsharpTreeReq request)
        {
            ScriptState<object> state = null;
            var countOfExpression = request.ExpressionDefinitions.Count;
            bool hasBreak;
            for (int i = 0; i < countOfExpression; i++)
            {
                var definition = request.ExpressionDefinitions[i];
                try
                {
                    if (i == 0)
                    {
                        state = await CSharpScript.RunAsync(definition.Expression, Helpers.GetImports(request));
                    }
                    else
                    {
                        state = await state.ContinueWithAsync(definition.Expression, Helpers.GetImports(request));
                    }
                }
                catch (EvaluationStoppedException)
                {
                    return Ok(state.ReturnValue); 
                }
            }

            if (!request.IsLastNode)
            {
                if ((bool)state.ReturnValue)
                {
                    state = await EvaluateNode(request.LeftNode, state);
                }
                else
                {
                    state = await EvaluateNode(request.RightNode, state);
                }
            }

            return Ok(state.ReturnValue);
        }
        
        private async Task<ScriptState<object>> EvaluateNode(EvaluateOrderCsharpTreeReq request, ScriptState<object> state)
        {
            var countOfExpression = request.ExpressionDefinitions.Count;
            bool hasBreak;
            for (int i = 0; i < countOfExpression; i++)
            {
                var definition = request.ExpressionDefinitions[i];
                try
                {
                    state = await state.ContinueWithAsync(definition.Expression, Helpers.GetImports(request));
                }
                catch (EvaluationStoppedException)
                {
                    return state;
                }
            }

            if (!request.IsLastNode)
            {
                if ((bool)state.ReturnValue)
                {
                    state = await EvaluateNode(request.LeftNode, state);
                }
                else
                {
                    state = await EvaluateNode(request.RightNode, state);
                }
            }

            return state;
        }
    }
}
