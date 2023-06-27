using Microsoft.AspNetCore.Mvc;
using practice_service.DTO;
using practice_service.Services;

namespace practice_service.Controllers
{
    [Route("api")]
    [ApiController]
    public class WorkPlaceInfoController : ControllerBase
    {
        private IWorkPlaceInfoService _workPlaceInfoService;

        public WorkPlaceInfoController(IWorkPlaceInfoService workPlaceInfoService)
        {
            _workPlaceInfoService = workPlaceInfoService;
        }

        [HttpPost]
        [Route("workPlaceInfo/create")]
        public async Task<ActionResult<WorkPlaceInfoDto>> CreateWorkPlaceInfo(WorkPlaceInfoCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _workPlaceInfoService.CreateUpdateWorkPlaceInfo(model);
                return await GetWorkPlaceInfo(model.UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("workPlaceInfo/info/{id}")]
        public async Task<ActionResult<WorkPlaceInfoDto>> GetWorkPlaceInfo(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return await _workPlaceInfoService.GetWorkPlaceInfo(id);
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

        [HttpPut]
        [Route("workPlaceInfo/edit/{id}")]
        public async Task<ActionResult<WorkPlaceInfoDto>> UpdateWorkPlaceInfo(WorkPlaceInfoCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _workPlaceInfoService.CreateUpdateWorkPlaceInfo(model);
                return await GetWorkPlaceInfo(model.UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("workPlaceInfo/delete/{id}")]
        public async Task<ActionResult> DeleteWorkPlaceInfo(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _workPlaceInfoService.DeleteWorkPlaceInfo(id);
                return Ok();
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
    }
}
