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
    public class EnfermedadTratamientoController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;

        //Se Recomienda solo dejar la Unidad de Trabajo
        public EnfermedadTratamientoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult<EnfermedadTratamientoResponse> Post(EnfermedadTratamientoRequest request)
        {
            CrearEnfermedadTratamientoService _service = new CrearEnfermedadTratamientoService(_unitOfWork);
            EnfermedadTratamientoResponse response = _service.CrearEnfermedadTratamiento(request);
            return Ok(response);
        }
    }
}