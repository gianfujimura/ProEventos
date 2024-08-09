using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistance.Contexts;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro {e.Message}");
            }
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Nenhum evento encontrado com este id");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar evento. Erro {e.Message}");
            }    
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null) return NotFound("Nenhum evento encontrado com este tema");

                return Ok(eventos);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar evento. Erro {e.Message}");
            }    
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar inserir evento. Erro {e.Message}");
            }    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar evento. Erro {e.Message}");
            }    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ?
                    Ok("Deletado") :
                    BadRequest("Evento não deletado");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar evento. Erro {e.Message}");
            }    
        }
    }
} 
