using BankingApp.Models;
using BankingApp.Models.DTOs;
using BankingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Transaction>>> GetAll()
        {
            try
            {
                var result = await _transactionService.ViewAll();
                if (result == null) return NotFound("No Transactions Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            try
            {
                var result = await _transactionService.View(id);
                if (result == null) return NotFound("No Transaction Found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Add([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                Transaction result = await _transactionService.Add(transactionDTO);
                if (result == null) return BadRequest("Transaction cannot be created");
                return Created("",result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
