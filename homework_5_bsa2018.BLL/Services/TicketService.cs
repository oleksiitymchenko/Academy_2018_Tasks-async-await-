using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class TicketService:IService<TicketDTO>
    {
        private IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TicketDTO>> GetAllAsync()
            => Mapper.Map<List<TicketDTO>>
            (await _unitOfWork.Tickets.GetAllAsync());

        public async Task<TicketDTO> GetAsync(int id) =>
            Mapper.Map<TicketDTO>(await _unitOfWork.Tickets.GetAsync(id));

        public async Task CreateAsync(TicketDTO ticket)
        {
            await _unitOfWork.Tickets.Create(Mapper.Map<Ticket>(ticket));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, TicketDTO ticket)
        {
            await _unitOfWork.Tickets.Update(id, Mapper.Map<Ticket>(ticket));
            await _unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Tickets.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
