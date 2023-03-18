﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.ProfileDataSource;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;

namespace SmartHomeManager.API.Controllers.ProfileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfilesController(IProfileRepository profileRepository)
        {
            _profileService = new(profileRepository);
        }

        /* 
         * GET: api/Profiles
         * Return: 
         * Ok(profiles) - IEnumerable of Profiles
         * NotFound(1) - No Profiles in DB
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            IEnumerable<Profile> profiles = await _profileService.GetProfiles();

            if (!profiles.Any())
                return NotFound();
           
            return Ok(profiles);
        }

        /* 
         * GET: api/Profiles/22222222-2222-2222-2222-222222222222
         * Return: 
         * Ok(profile) - Profile as requested
         * NotFound(1) - Profile does not exist
        */
        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetProfileByProfileId(Guid profileId)
        {
            Profile? profile = await _profileService.GetProfileByProfileId(profileId);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        /* 
         * GET: api/Profiles/get-profiles/11111111-1111-1111-1111-111111111111
         * Return: 
         * Ok(profiles) - IEnumerable of Profiles
         * NotFound(1) - No Profiles in DB
        */
        [HttpGet("get-profiles/{accountId}")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfilesByAccountId(Guid accountId)
        {
            /*var realAccountId = Guid.Parse(accountId);*/
            IEnumerable<Profile> profiles = (await _profileService.GetProfilesByAccountId(accountId))!;

            if (profiles == null)
                return NotFound(1);

            return Ok(profiles);
        }

        /* 
         * GET: api/Profiles/get-device-ids/22222222-2222-2222-2222-222222222222
         * Return: 
         * Ok(listOfDeviceIds) - IEnumerable of Device Ids
         * NotFound(1) - No Device/Profile in DeviceProfile r/s table in DB
        */
        [HttpGet("get-device-ids/{profileId}")]
        public async Task<ActionResult<IEnumerable<Guid>>> GetDevicesByProfileId(Guid profileId)
        {
            IEnumerable<Guid> listOfDeviceIds = (await _profileService.GetDevicesByProfileId(profileId))!;

            if (listOfDeviceIds == null)
            {
                return NotFound(1);
            }

            return Ok(listOfDeviceIds);
        }

        /*
                * POST: api/Profiles
                * Return:
                * Ok(1) - Profile created successfully
                * BadRequest(1) - Profile failed to create
                */
        [HttpPost]
        public async Task<ActionResult> PostProfile([FromBody] ProfileWebRequest profileWebRequest)
        {
            int response = await _profileService.CreateProfile(profileWebRequest);

            if (response == 1)
            {
                return Ok(1);
            }

            return BadRequest(1);
        }

        /* 
         * PUT: api/Profiles/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Profile successfully updated
         * BadRequest(1) - Profile failed to update
         * NotFound(1) - Profile does not exist
         * 
        */
        [HttpPut("{profileId}")]
        public async Task<IActionResult> PutProfile(Guid profileId, [FromBody] UpdateProfileWebRequest updateProfileWebRequest)
        {
            Profile? profile = await _profileService.GetProfileByProfileId(profileId);
            if (profile == null)
            {
                return NotFound(1);
            }

            if (await _profileService.UpdateProfile(profile, updateProfileWebRequest))
            {
                return Ok(1);
            }

            return BadRequest(1);
        }
        /* 
         * DELETE: api/Profiles/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Profile successfully deleted
         * BadRequest(1) - Profile failed to delete
         * NotFound(1) - Profile does not exist
         * 
        */
        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfile(Guid profileId)
        {

            Profile? profile = await _profileService.GetProfileByProfileId(profileId);
            if (profile == null)
            {
                return NotFound(1);
            }

            if (await _profileService.DeleteProfile(profile))
            {
                return Ok(1);
            }

            return BadRequest(1);
        }
    }
}
