using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SintomaController : ControllerBase
    {

        readonly IUnitOfWork _unitOfWork;

        //Se Recomienda solo dejar la Unidad de Trabajo
        public SintomaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult<SintomaResponse> Post(SintomaRequest request)
        {
            CrearSintomaService _service = new CrearSintomaService(_unitOfWork);
            SintomaResponse response = _service.CrearSitoma(request);
            return Ok(response);
        }

    }
}