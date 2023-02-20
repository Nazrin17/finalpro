using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;

namespace Business.Services.Implementations
{
    public class ReservationManager : IReservationService
    {
        private readonly IDoctorService _doctorService;

        public ReservationManager(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Okei()
        {
            List<DoctorGetDto> docs = await _doctorService.GetAllAsync(d=>!d.IsDeleted);
            foreach (DoctorGetDto doc in docs)
            {
                foreach (var item in doc.rezervs)
                {
                    if (item.Busy)
                    {
                        Mail.SendMessage(item.UserEmail, "Rezervasiya", "Rezervasiyaniz qebul olundu,zehmet olmasa gecikmeyin.Tesekkurler;)");
                    }
                }

            }
        }

        public async Task Reserv(int id, string time, string user)
        {
            DoctorGetDto doc=await _doctorService.GetbyId(id);
            foreach (var item in doc.rezervs)
            {
                if (item.Time == time)
                {
                    item.Busy = !item.Busy;
                    if (!item.Busy)
                    {
                        Mail.SendMessage(user, "Rezervasiya", "Teessufle bildiririk ki, rezervasiyaniz qebul olunmadi");
                    }
                }
            };
            DoctorUpdateDto updateDto = new DoctorUpdateDto() { getDto = doc };
            await _doctorService.UpdateAsync(updateDto);
        }
    }
}
