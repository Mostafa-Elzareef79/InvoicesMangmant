using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Enums
{
    public enum HttpStatuses
    {
        Status200OK = 200,
        Status201Created = 201,
        Status400BadRequest = 400,
        Status401Unauthorized = 401,
        Status404NotFound = 404,
        Status500InternalServerError = 500,
        Status503ServiceUnavailable = 503
    }
}
