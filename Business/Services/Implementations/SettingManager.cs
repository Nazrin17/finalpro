using AutoMapper;
using Business.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class SettingManager : ISettingService
    {
        private readonly ISettingRepository _repository;
        private readonly IMapper _mapper;

        public SettingManager(ISettingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public  async Task<SettingGetDto> Get()
        {
            Setting setting = await _repository.Get("Icons");
            SettingGetDto getDto = _mapper.Map<SettingGetDto>(setting);
            return getDto;
        }

        public async Task UpdateAsync(SettingUpdateDto updateDto)
        {
            Setting setting = await _repository.Get(e => e.Id == updateDto.getDto.Id, "Icons");
            updateDto.getDto = _mapper.Map<SettingGetDto>(setting);
            setting.Email = updateDto.postDto.Email;
            setting.Phone = updateDto.postDto.Phone;
            setting.Address = updateDto.postDto.Address;
            setting.Logo = updateDto.postDto.Logo;
            if (updateDto.postDto.LogoIcon != null) setting.LogoIcon = updateDto.postDto.LogoIcon;
            if (updateDto.postDto.Tags != null)
            {
                List<SettingIcon> icons = new List<SettingIcon>();
                for (int i = 0; i < updateDto.postDto.Tags.Count; i++)
                {
                    icons.Add(new SettingIcon { IconTag = updateDto.postDto.Tags[i], IconUrl = updateDto.postDto.Urls[i] });
                };
                for (int i = 0; i < setting.Icons.Count; i++)
                {
                    if ((i + 1) <= icons.Count)
                    {
                        setting.Icons[i] = icons[i];
                    }
                    else
                    {
                        setting.Icons.Remove(setting.Icons[i]);
                    }
                }
            }
            _repository.Update(setting);
        }
        public async Task Create()
        {
            Setting setting = new Setting { Email = "As@gmail.com", Address = "asdd", Logo = "asd", Phone = "asdasd" };
            await _repository.CreateAsync(setting);
        }
    }
}
