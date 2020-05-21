using Domain.Contracts;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class ConsultarDiagnosticoService
    {
        readonly IUnitOfWork _unitOfWork;


        public ConsultarDiagnosticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public List<Diagnostico> GetAll()
        {

            var res = _unitOfWork.DiagnosticoRepository.GetAll();
            _unitOfWork.Dispose();
            return res.ToList();
        }


        public List<Diagnostico> GetEstado(string Estado)
        {
            var ConsultarEstado = _unitOfWork.DiagnosticoRepository.FindBy(T => T.Estado == Estado);
            _unitOfWork.Dispose();
            return ConsultarEstado.ToList();
        }

        public Diagnostico Diagnostico(string Estado,int Id)
        {
            var ConsultarEstado = GetEstado(Estado);
            Diagnostico obtener = ConsultarEstado.Where(D => D.Id == Id).FirstOrDefault();
            return obtener;
        }
    }
}
