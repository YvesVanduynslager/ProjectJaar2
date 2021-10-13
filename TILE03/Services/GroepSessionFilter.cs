using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TILE03.Models.Domain;
using TILE03.Models.Domain.BewerkingStrategy;
using TILE03.Models.Domain.State;
using TILE03.Models.Repositories;

namespace TILE03.Services
{
    public class GroepSessionFilter : ActionFilterAttribute
    {
        private Groep groep;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            groep = ReadGroepFromSession(context.HttpContext);
            context.ActionArguments["groep"] = groep;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            WriteGroepToSession(groep, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private Groep ReadGroepFromSession(HttpContext context)
        {
            Groep groep = context.Session.GetString("groep") == null
                ? new Groep()
                : JsonConvert.DeserializeObject<Groep>(context.Session.GetString("groep") /*, deseralizeSettings*/);

            groep.ToState(groep.HuidigeState);

            return groep;
        }

        private void WriteGroepToSession(Groep groep, HttpContext context)
        {
            context.Session.SetString("groep", JsonConvert.SerializeObject(groep));
        }
    }
}