using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestApi.Domain;
using RestApi.Services.Interfaces;

namespace RestApi.Controllers
{
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var questions = await questionService.GetAllAsync();

                if (questions is null)
                {
                    return NotFound();
                }

                return Ok(questions);
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet("questions/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var question = await questionService.GetByIdAsync(id);

                if (question is null)
                {
                    return NotFound();
                }

                return Ok(question);
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost("questions")]
        public async Task<IActionResult> CreateAsync([FromBody]Question question)
        {
            try
            {
                await questionService.CreateAsync(question);

                return StatusCode(201, question);
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost("questions/bulk")]
        public async Task<IActionResult> CreateBulkAsync(IList<Question> questions)
        {
            try
            {
                await questionService.CreateBulkAsync(questions);

                return StatusCode(201, questions);
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPut("questions")]
        public async Task<IActionResult> UpdateAsync(Question question)
        {
            try
            {
                await questionService.UpdateAsync(question);

                return Ok(question);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPut("questions/bulk")]
        public async Task<IActionResult> UpdateBulkAsync(IList<Question> questions)
        {
            try
            {
                await questionService.UpdateBulkAsync(questions);

                return Ok(questions);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpDelete("questions/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var question = await questionService.GetByIdAsync(id);

                if (question is null)
                {
                    return NotFound();
                }

                await questionService.DeleteAsync(question);

                return Ok(question);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpDelete("questions/bulk")]
        public async Task<IActionResult> DeleteBulkAsync(IList<Question> questions)
        {
            try
            {
                await questionService.DeleteBulkAsync(questions);

                return Ok(questions);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
