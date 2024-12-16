using Microsoft.AspNetCore.Mvc.Filters;

namespace KatmanliSP.WebAPI.Filters
{
    /// <summary>
    /// Asoect orinted , midddleware, filter vs
    /// </summary>
    public class TraceFlter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}

