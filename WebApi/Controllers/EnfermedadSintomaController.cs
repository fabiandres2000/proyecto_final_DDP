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
    public class EnfermedadSintomaController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;

        //Se Recomienda solo dejar la Unidad de Trabajo
        public EnfermedadSintomaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult<EnfermedadSintomaResponse> Post(EnfermedadSintomaRequest request)
        {
            CrearEnfermedadSintomaService _service = new CrearEnfermedadSintomaService(_unitOfWork);
            EnfermedadSintomaResponse response = _service.CrearEnfermedadSitoma(request);
            return Ok(response);
        }
    }
}