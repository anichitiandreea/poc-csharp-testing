using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTesting.Domains;
using UnitTesting.Services.Interfaces;

namespace UnitTesting.Controllers
{
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService answerService;
        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var answers = await answerService.GetAllAsync();

                if (answers is null)
                {
                    return NotFound();
                }

                return Ok(answers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var answer = await answerService.GetByIdAsync(id);

                if (answer is null)
                {
                    return NotFound();
                }

                return Ok(answer);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> CreateAsync(Answer answer)
        {
            try
            {
                await answerService.CreateAsync(answer);

                return StatusCode(201, answer);

            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> CreateBulkAsync(IList<Answer> answers)
        {
            try
            {
                await answerService.CreateBulkAsync(answers);

                return StatusCode(201, answers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> UpdateAsync(Answer answer)
        {
            try
            {
                await answerService.UpdateAsync(answer);

                return Ok(answer);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> UpdateBulkAsync(IList<Answer> answers)
        {
            try
            {
                await answerService.UpdateBulkAsync(answers);

                return Ok(answers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var answer = await answerService.GetByIdAsync(id);

                if (answer is null)
                {
                    return NotFound();
                }

                await answerService.DeleteAsync(answer);

                return Ok(answer);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> DeleteBulkAsync(IList<Answer> answers)
        {
            try
            {
                await answerService.DeleteBulkAsync(answers);

                return Ok(answers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
