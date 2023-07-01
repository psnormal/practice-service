﻿using Microsoft.AspNetCore.Mvc;
using practice_service.DTO;
using practice_service.Services;
using System.Net;

namespace practice_service.Controllers
{
    [Route("api")]
    [ApiController]
    public class PracticePeriodController : ControllerBase
    {
        private IPracticePeriodService _practicePeriodService;

        public PracticePeriodController(IPracticePeriodService practicePeriodService)
        {
            _practicePeriodService = practicePeriodService;
        }

        [HttpPost]
        [Route("practicePeriod/create")]
        public async Task<ActionResult<PracticePeriodPageDto>> CreatePracticePeriod([FromHeader(Name = "Authorization")] string Authorization, PracticePeriodCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var practicePeriodId = await _practicePeriodService.CreatePracticePeriod(Authorization, model);
                return GetPracticePeriodInfo(practicePeriodId);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This info does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("practicePeriod/info/{id}")]
        public ActionResult<PracticePeriodPageDto> GetPracticePeriodInfo(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _practicePeriodService.GetPracticePeriodById(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice period does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("practicePeriods")]
        public ActionResult<PracticePeriodsDto> GetAllPracticePeriods()
        {
            try
            {
                return _practicePeriodService.GetPracticePeriods();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("practicePeriod/{id}/students")]
        public async Task<ActionResult<StudentsInPeriodInfoDto>> GetStudentsInPeriod([FromHeader(Name = "Authorization")] string Authorization,  Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return await _practicePeriodService.GetStudentsInPeriod(Authorization, id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice period does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                if (ex.Message == "This info does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        [Route("practicePeriod/edit/{id}")]
        public async Task<ActionResult<PracticePeriodPageDto>> EditPracticePeriod([FromHeader(Name = "Authorization")] string Authorization, Guid id, PracticePeriodCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _practicePeriodService.EditPracticePeriod(Authorization, id, model);
                return GetPracticePeriodInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice period does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                if (ex.Message == "This info does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("practicePeriod/delete/{id}")]
        public async Task<ActionResult> DeletePracticePeriod(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _practicePeriodService.DeletePracticePeriod(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice period does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
