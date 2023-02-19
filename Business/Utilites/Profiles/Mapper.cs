
using AutoMapper;
using Core.Entities.Concrete;

public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Home, HomeGetDto>();
            CreateMap<HomePostDto, Home>();
            CreateMap<About, AboutGetDto>();
            CreateMap<AboutPostDto, About>();
            CreateMap<DoctorPostDto, Doctor>();
            CreateMap<Doctor, DoctorGetDto>();
            CreateMap<Service, ServiceGetDto>();
            CreateMap<Position, PositionGetDto>();
            CreateMap<PositionPostDto, Position>();
            CreateMap<ServicePostDto, Service>();
            CreateMap<SettingPostDto, Setting>();
            CreateMap<Setting, SettingGetDto>();
            CreateMap<Message, MessageGetDto>();
            CreateMap<MessagePostDto, Message>();
            CreateMap<UserRegisterDto, AppUser>();
        }
    }

