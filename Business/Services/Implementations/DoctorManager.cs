using AutoMapper;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Core.Utilities;
using Core.Utilities.Extentions;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{

    public class DoctorManager : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IPositionRepository _position;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<AppUser> _userManager;

        public DoctorManager(IDoctorRepository repository, IPositionRepository position, IMapper mapper, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> role, IActionContextAccessor actionContextAccessor)
        {
            _repository = repository;
            _position = position;
            _mapper = mapper;
            _env = env;
            _userManager = userManager;
            _role = role;
            _actionContextAccessor = actionContextAccessor;
        }

        public async Task AddHistory(ResHistory history,int id)
        {
            Doctor doc=  await _repository.Get(d => d.Id == id,"history");
            doc.history.Add( history);
        }

        public async Task CreateAsync(DoctorPostDto postDto)
        {
            Doctor doctor = _mapper.Map<Doctor>(postDto);
            doctor.Image = postDto.formFile.FileCreate(_env.WebRootPath, "assets/img"); ;
            List<Icon> icons = new List<Icon>();
            for (int i = 0; i < postDto.Tags.Count; i++)
            {
                icons.Add(new Icon { IconTag = postDto.Tags[i], IconUrl = postDto.Urls[i] });
            };
            foreach (var item in icons)
            {
                doctor.Icons.Add(item);
            }
            List<Rezerv> rezervs = new List<Rezerv>();
            for (int i = 0; i < postDto.Times.Count; i++)
            {
                rezervs.Add(new Rezerv { Busy = false, Time = postDto.Times[i] });
            }
            foreach (var item in rezervs)
            {
                doctor.rezervs.Add(item);
            }
            Position position = await _position.Get(p => p.Id == postDto.PositionId, "Doctors");

            doctor.Position = position;
            _position.Update(position);
            await _repository.CreateAsync(doctor);
            AppUser user = new AppUser
            {
                Email = postDto.Email,
                UserName = postDto.Email
            };
            var result = await _userManager.CreateAsync(user, $"{postDto.Name}123@");
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, token);
            var result2 = await _userManager.AddToRoleAsync(user, "doctor");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Doctor doctor = await _repository.Get(a => a.Id == id, "Icons", "rezervs", "Position");
                  if (doctor == null) { return false; };
            AppUser user = await _userManager.FindByNameAsync(doctor.Email);
            await _userManager.DeleteAsync(user);
             Helper.FileDelete(_env.WebRootPath, "assets/img", doctor.Image);
            doctor.IsDeleted = true;
            _repository.Update(doctor);
            return true;
        }

        public async Task<DoctorGetDto> Get(Expression<Func<Doctor, bool>> exp, params string[] includes)
        {
            Doctor doc=  await _repository.Get(exp, includes);
            DoctorGetDto getDto=_mapper.Map<DoctorGetDto>(doc);
            return getDto;
        }

        public async Task<List<DoctorGetDto>> GetAllAsync(Expression<Func<Doctor, bool>> exp)
        {
            List<Doctor> doctors = await _repository.GetAllAsnyc(exp,"Icons", "rezervs", "Position");
            List<DoctorGetDto> getDtos = _mapper.Map<List<DoctorGetDto>>(doctors);
            return getDtos;
        }

        public async Task<List<DoctorGetDto>> GetAllAsync(Expression<Func<Doctor, bool>> exp, params string[] includes)
        {
            List<Doctor> docs = await _repository.GetAllAsnyc(exp, includes);
            List<DoctorGetDto> getDtos=_mapper.Map<List<DoctorGetDto>>(docs);
            return getDtos;
        }

        public async Task<DoctorGetDto> GetbyId(int id)
        {
            Doctor doctor = await _repository.Get(d => d.Id == id, "Icons", "rezervs", "Position");
            DoctorGetDto getDto = _mapper.Map<DoctorGetDto>(doctor);
            return getDto;
        }

        public async Task<bool> UpdateAsync(DoctorUpdateDto updateDto)
        {
            Doctor doctor = await _repository.Get(e => e.Id == updateDto.getDto.Id, "Icons", "rezervs", "Position");
            if (updateDto.postDto == null)
            {
                doctor.rezervs = updateDto.getDto.rezervs;
                 _repository.Update(doctor);
                return true;
            };

            updateDto.getDto = _mapper.Map<DoctorGetDto>(doctor);
            if (!_actionContextAccessor.ActionContext.ModelState.IsValid)
            {
                return false;
            }
            if (updateDto.postDto.formFile != null)
            {
                if (!updateDto.postDto.formFile.ContentType.Contains("image"))
                {
                    _actionContextAccessor.ActionContext.ModelState.AddModelError("", "Please send image");
                    return false;
                }
                string imagename = updateDto.postDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
               Helper.FileDelete(_env.WebRootPath, "assets/img", doctor.Image);
                doctor.Image = imagename;
            }
            doctor.Name = updateDto.postDto.Name;
            doctor.About = updateDto.postDto.About;
            doctor.Email = updateDto.postDto.Email;
            doctor.Surname = updateDto.postDto.Surname;
            doctor.Phone = updateDto.postDto.Phone;
            doctor.PositionId = updateDto.postDto.PositionId;
            Position position = await _position.Get(p => p.Id == updateDto.postDto.PositionId);
            doctor.Position = position;
            if (updateDto.postDto.Tags != null)
            {
                List<Icon> icons = new List<Icon>();
                for (int i = 0; i < updateDto.postDto.Tags.Count; i++)
                {
                    icons.Add(new Icon { IconTag = updateDto.postDto.Tags[i], IconUrl = updateDto.postDto.Urls[i] });
                };
                for (int i = 0; i < doctor.Icons.Count; i++)
                {
                    if ((i + 1) <= icons.Count)
                    {
                        doctor.Icons[i] = icons[i];
                    }
                    else
                    {
                        doctor.Icons.Remove(doctor.Icons[i]);
                    }
                }
                if (icons.Count > doctor.Icons.Count)
                {
                    for (int i = doctor.Icons.Count; i <icons.Count; i++)
                    {
                        doctor.Icons.Add(icons[i]);

                    }
                }
                if (icons.Count < doctor.Icons.Count)
                {
                    for (int i = icons.Count - 1; i >= doctor.Icons.Count; i--)
                    {
                        doctor.Icons.Remove(icons[i]);

                    }
                }
            }
            else
            {
                for (int i = 0; i < doctor.Icons.Count; i++)
                {
                    doctor.Icons[i] = new Icon { IconTag = "", IconUrl = "" };
                }
            }
            if (updateDto.postDto.Times != null)
            {
                List<Rezerv> rezervs = new List<Rezerv>();
                for (int i = 0; i < updateDto.postDto.Times.Count; i++)
                {
                    rezervs.Add(new Rezerv { Time = updateDto.postDto.Times[i], Busy = false });
                };
                for (int i = 0; i < doctor.rezervs.Count; i++)
                {
                    if ((i + 1) <= rezervs.Count)
                    {
                        doctor.rezervs[i] = rezervs[i];
                    }
                    else
                    {
                        doctor.rezervs.Remove(doctor.rezervs[i]);
                    }
                }
                if (rezervs.Count > doctor.rezervs.Count)
                {
                    for (int i = doctor.rezervs.Count; i < rezervs.Count; i++)
                    {
                        doctor.rezervs.Add(rezervs[i]);

                    }
                }
                if (rezervs.Count < doctor.rezervs.Count)
                {
                    for (int i = 0; i < doctor.rezervs.Count; i++)
                    {
                        if (rezervs[i] != doctor.rezervs[i])
                        {
                            doctor.rezervs.Remove(doctor.rezervs[i]);
                        }
                    }
                    //for (int i = rezervs.Count - 1; i >= doctor.rezervs.Count; i--)
                    //{
                    //    doctor.rezervs.Remove(rezervs[i]);

                    //}
                }
            }
            else
            {
                doctor.rezervs = null;
            }
            _repository.Update(doctor);
            return true;
        }
    }
}
