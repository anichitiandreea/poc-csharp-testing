using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTesting.Domain;
using UnitTesting.Services.Interfaces;

namespace UnitTesting.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await userService.GetAllAsync();

                if (users is null)
                {
                    return NotFound();
                }

                return Ok(users);
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
                var user = await userService.GetByIdAsync(id);

                if (user is null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> CreateAsync(User user)
        {
            try
            {
                await userService.CreateAsync(user);

                return StatusCode(201, user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> CreateBulkAsync(IList<User> users)
        {
            try
            {
                await userService.CreateBulkAsync(users);

                return StatusCode(201, users);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> UpdateAsync(User user)
        {
            try
            {
                await userService.UpdateAsync(user);

                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> UpdateBulkAsync(IList<User> users)
        {
            try
            {
                await userService.UpdateBulkAsync(users);

                return Ok(users);
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
                var user = await userService.GetByIdAsync(id);

                if (user is null)
                {
                    return NotFound();
                }

                await userService.DeleteAsync(user);

                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        public async Task<IActionResult> DeleteBulkAsync(IList<User> users)
        {
            try
            {
                await userService.DeleteBulkAsync(users);

                return Ok(users);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
