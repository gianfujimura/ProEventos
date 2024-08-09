using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistance;
using ProEventos.Persistance.Interface;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private IEventoPersistance _eventoPersistance;
        private IGeralPersistance _geralPersistance;

        public EventoService(IEventoPersistance eventoPersistance, IGeralPersistance geralPersistance)
        {
            _eventoPersistance = eventoPersistance;
            _geralPersistance = geralPersistance;
        }

        public async Task<Evento> AddEvento(Evento evento)
        {
            try
            {
                _geralPersistance.add<Evento>(evento);

                if(await _geralPersistance.SaveChangesAsync())
                {
                    return await _eventoPersistance.GetEventoByIdAsync(evento.Id, false);
                }

                return null;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistance.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geralPersistance.Update<Evento>(model);

                if(await _geralPersistance.SaveChangesAsync())
                {
                    return await _eventoPersistance.GetEventoByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistance.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não foi encontrado!");

                _geralPersistance.Delete<Evento>(evento);
                return await _geralPersistance.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistance.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {       
            try
            {
                var eventos = await _eventoPersistance.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistance.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}