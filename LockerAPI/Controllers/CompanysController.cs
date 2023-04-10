using LockerAPI.DTOs;
using LockerAPI.DTOs.CreateUpdateObjects;
using LockerAPI.DTOs.PatchObject;
using LockerAPI.Helpers;
using LockerAPI.Models;
using LockerAPI.Services;
using FSharp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Net;

namespace LockerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanysController : ControllerBase
    {
        private readonly ICompanysService _companysService;
        private readonly ILogger<CompanysController> _logger;

        public CompanysController(ICompanysService companysService, ILogger<CompanysController> logger)
        {
            _companysService = companysService;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("GetCompanys started");
                var companys = await _companysService.GetCompanysAsync();
                if (companys == null || !companys.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, "No element");
                }
                return Ok(companys);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllCompanys error: {ex.Message}");
                return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [HttpGet("{id}")]   //idul setat aici ca nume trebui sa fie 1 la 1 cu id-ul de mai jos
        public async Task<IActionResult> GetCompanyAsync([FromRoute] Guid id)  // id-ul de aici
        {
            try
            {
                _logger.LogInformation("GetCompanybyID started");
                var company = await _companysService.GetCompanysByIdAsync(id);
                if (company == null)
                {
                    //return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCompanyByID error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> PostCompanyAsync([FromBody] Company company)
        {
            try
            {
                _logger.LogInformation("CreatecompanyAsync started");
                if (company == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _companysService.CreateCompanyAsync(company);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("Delete Announcement Started");
                bool result = await _companysService.DeleteCompanyAsync(id);
                if (result)
                {
                    return Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted);
                }
                return BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany([FromRoute] Guid id, [FromBody] CreateUpdateCompany company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                CreateUpdateCompany updatedCompany = await _companysService.UpdateCompanyAsync(id, company);
                if (updatedCompany == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCompany([FromRoute] Guid id, [FromBody] PatchCompany company)
        {
            try
            {
                _logger.LogInformation("UpdateStarted");
                if (company == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                PatchCompany updatedCompany = await _companysService.UpdatePartiallyCompanyAsync(id, company);
                if (updatedCompany == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
