﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<int> CreateProfile(ProfileWebRequest profileWebRequest)
        {
            Profile newProfile = new Profile();
            newProfile.ProfileId = Guid.NewGuid();
            newProfile.Name = profileWebRequest.Name;
            newProfile.Description = profileWebRequest.Description;
            newProfile.Pin = profileWebRequest.Pin;
            //For testing set to the default accountId(how to get from frontend?)
            newProfile.AccountId = Guid.Parse(profileWebRequest.AccountId);
            /*newProfile.Scenarios = new List<Scenario>();*/


            //previous method
            /*Profile newProfile = new Profile();
            newProfile.ProfileId = new Guid();
            newProfile.AccountId = profile.AccountId;
            newProfile.Account = profile.Account;
            newProfile.Scenarios = profile.Scenarios;
            newProfile.Devices = profile.Devices;*/

            /*Debug.WriteLine(profile.Account);*/

            bool response = await _profileRepository.AddAsync(newProfile);

            if (response)
            {
                int saveResponse = await _profileRepository.SaveAsync();

                if (saveResponse > 0)
                {
                    return 1;
                }
            }

            return 2;
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetAllAsync();

            if (!profiles.Any())
            {
                return Enumerable.Empty<Profile>();
            }

            return profiles;
        }

        public async Task<Profile?> GetProfileByProfileId(Guid id)
        {
            Profile? profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
            {
                return null;    
            }

            return profile;
            
        }

        public async Task<IEnumerable<Profile?>?> GetProfilesByAccountId(Guid id)
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetProfilesByAccountId(id);
            if (!profiles.Any())
            {
                return null;
            }

            return profiles;
        }

        public async Task<IEnumerable<Guid>?> GetDevicesByProfileId(Guid id)
        {

            IEnumerable<Guid> listOfDeviceIds = await _profileRepository.GetDevicesByProfileId(id);
            if (!listOfDeviceIds.Any())
            {
                return null;
            }

            return listOfDeviceIds;
        }

        public async Task<bool> UpdateProfile(Profile profile, UpdateProfileWebRequest updateProfileWebRequest)
        {
            profile.Name = updateProfileWebRequest.Name;
            profile.Description = updateProfileWebRequest.Description;
            profile.Pin = updateProfileWebRequest.Pin;
           

            int updateResponse = await _profileRepository.UpdateAsync(profile);
            if (updateResponse == 1)
            {
                return true;
            }

            return false;
            /* throw new NotImplementedException();*/
        }

        public async Task<bool> DeleteProfile(Profile profile)
        {
            bool deleteResponse = _profileRepository.Delete(profile);
            if (deleteResponse)
            {
                await _profileRepository.SaveAsync();
                return true;
            }

            return false;
            /*throw new NotImplementedException();*/
        }

    }
}
