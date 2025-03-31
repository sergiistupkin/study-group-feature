using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestAppAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyGroupController : ControllerBase
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudyGroup([FromBody] StudyGroup studyGroup)
        {
            await _studyGroupRepository.CreateStudyGroup(studyGroup);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return Ok(studyGroups);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchStudyGroups([FromQuery] string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return Ok(studyGroups);
        }

        [HttpPost("{studyGroupId}/join")]
        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, [FromQuery] int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return Ok();
        }

        [HttpPost("{studyGroupId}/leave")]
        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, [FromQuery] int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return Ok();
        }
    }
}
