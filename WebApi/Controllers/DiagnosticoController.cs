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
    public class DiagnosticoController : ControllerBase
    {

        readonly IUnitOfWork _unitOfWork;

        //Se Recomienda solo dejar la Unidad de Trabajo
        public DiagnosticoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult<DiagnosticoResponse> Post(DiagnosticoRequest request)
        {
            CrearDiagnosticoService _service = new CrearDiagnosticoService(_unitOfWork);
            DiagnosticoResponse response = _service.CrearDiagnostico(request);
            return Ok(response);
        }
    }
}