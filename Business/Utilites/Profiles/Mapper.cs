
using AutoMapper;
using Core.Entities.Concrete;
using Entities.Concrete.Models;
using Entities.Dtos.ResHistory;

public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Home, HomeGetDto>();
            CreateMap<HomePostDto, Home>();
            CreateMap<About, AboutGetDto>();
            CreateMap<AboutPostDto, About>();
            CreateMap<DoctorPostDto, Doctor>();
            CreateMap<Doctor, DoctorGetDto>().ReverseMap();
            CreateMap<Service, ServiceGetDto>();
            CreateMap<Position, PositionGetDto>();
            CreateMap<PositionPostDto, Position>();
            CreateMap<ServicePostDto, Service>();
            CreateMap<SettingPostDto, Setting>();
            CreateMap<Setting, SettingGetDto>();
            CreateMap<Message, MessageGetDto>();
            CreateMap<ResHistory, ResGetDto>();
            CreateMap<ResPostDto, ResHistory>();
            CreateMap<MessagePostDto, Message>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    }

