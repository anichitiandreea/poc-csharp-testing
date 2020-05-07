using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTesting.Domain;
using UnitTesting.Services.Interfaces;

namespace UnitTesting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Question question)
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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

        [HttpDelete]
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
