using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisualStudio2017.Domain.DataAccess
{
    public class HttpSessionImplementation
    {
		private readonly HttpContext _context;

		public HttpSessionImplementation(IHttpContextAccessor ctx)
		{
			_context = ctx.HttpContext;
		}


    }
}
