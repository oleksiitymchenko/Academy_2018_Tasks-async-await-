using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;

namespace homework_5_bsa2018.BLL.Services
{
    public class TicketService:IService<TicketDTO>
    {
        private IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TicketDTO> GetAll()
            => Mapper.Map<List<TicketDTO>>
            (_unitOfWork.Tickets.GetAll());

        public TicketDTO Get(int id) =>
            Mapper.Map<TicketDTO>(_unitOfWork.Tickets.Get(id));

        public void Create(TicketDTO ticket)
        {
            _unitOfWork.Tickets.Create(Mapper.Map<Ticket>(ticket));
            _unitOfWork.Save();
        }

        public void Update(int id, TicketDTO ticket)
        {
            _unitOfWork.Tickets.Update(id, Mapper.Map<Ticket>(ticket));
            _unitOfWork.Save();
        }
        
        public void Delete(int id)
        {
            _unitOfWork.Tickets.Delete(id);
            _unitOfWork.Save();
        }
    }
}
