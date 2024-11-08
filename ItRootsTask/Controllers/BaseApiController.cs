using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
 
    public class BaseApiController : Controller
{

            public BaseApiController()
            {

            }

            private IMediator _mediator;
            protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
        }
    }

