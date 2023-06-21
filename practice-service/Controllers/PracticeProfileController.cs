using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practice_service.DTO;
using practice_service.Models;
using practice_service.Services;

namespace practice_service.Controllers
{
    [Route("api")]
    [ApiController]
    public class PracticeProfileController : ControllerBase
    {
        private IPracticeProfileService _practiceProfileService;

        public PracticeProfileController(IPracticeProfileService practiceProfileService)
        {
            _practiceProfileService = practiceProfileService;
        }

        [HttpPost]
        [Route("practiceProfile/create")]
        public async Task<ActionResult<PracticeProfilePageDto>> CreatePracticeProfile(PracticeProfileCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var practiceProfileId = await _practiceProfileService.CreatePracticeProfile(model);
                return GetPracticeProfileInfo(practiceProfileId);
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
        [Route("practiceProfile/info/{id}")]
        public ActionResult<PracticeProfilePageDto> GetPracticeProfileInfo(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _practiceProfileService.GetPracticeProfileById(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice profile does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("user/{id}/practiceProfiles")]
        public ActionResult<StudentPracticeProfiles> GetStudentPracticeProfiles(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _practiceProfileService.GetStudentPracticeProfiles(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        [Route("practiceProfile/edit/{id}")]
        public async Task<ActionResult<PracticeProfilePageDto>> EditPracticeProfile(Guid id, PracticeProfileUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _practiceProfileService.EditPracticeProfiles(id, model);
                return GetPracticeProfileInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice profile does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("practiceProfile/delete/{id}")]
        public async Task<ActionResult> DeletePracticeProfile(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _practiceProfileService.DeletePracticeProfile(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This practice profile does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
