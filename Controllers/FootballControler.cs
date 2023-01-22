using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bazeneo4j.Models;
using Bazeneo4j.Repositories;

namespace Bazeneo4j.Controllers
{
    [ApiController]
    [Route("/")]
    public class FootballControler : ControllerBase
    {
        private readonly IFootballRepository _footballRepository;
        public FootballControler(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
        }

        [Route("/search/{naziv}")]
        [HttpGet]
        public async Task<List<Igrac>> SearchIgraci([FromRoute(Name = "naziv")] string search)
        {
            return await _footballRepository.Search(search);
        }

        [Route("/search1/{naziv1}/{naziv2}")]
        [HttpGet]
        public async Task<List<Igrac>> SearchIgraci1([FromRoute(Name = "naziv1")] string search1, [FromRoute(Name = "naziv2")] string search2)
        {
            return await _footballRepository.Search1(search1,search2);
        }

        [Route("/search2/{naziv1}/{naziv2}/{naziv3}")]
        [HttpGet]
        public async Task<List<Igrac>> SearchIgraci2([FromRoute(Name = "naziv1")] string search1, [FromRoute(Name = "naziv2")] string search2, [FromRoute(Name = "naziv3")] string search3)
        {
            return await _footballRepository.Search2(search1, search2, search3);
        }

        [Route("/searchKlub")]
        [HttpGet]
        public async Task<List<String>> SearchKlubovi()
        {
            return await _footballRepository.SearchKlub();
        }

        [Route("/comboIgraci")]
        [HttpGet]
        public async Task<List<String>> comboIgraci()
        {
            return await _footballRepository.ComboIgraci();
        }

        [Route("/igraliZajedno/{ime1}/{ime2}")]
        [HttpGet]
        public async Task<List<String>> comboIgraci([FromRoute(Name = "ime1")] string search1, [FromRoute(Name = "ime2")] string search2)
        {
            return await _footballRepository.IgraliZajedno(search1,search2);
        }

        [Route("/comboLige")]
        [HttpGet]
        public async Task<List<String>> comboLige()
        {
            return await _footballRepository.ComboLige();
        }

        [Route("/kluboviLiga/{naziv}")]
        [HttpGet]
        public async Task<List<String>> KluboviLiga([FromRoute(Name = "naziv")] string search1)
        {
            return await _footballRepository.KluboviLiga(search1);
        }

        [Route("/IgracKlubovi/{ime}")]
        [HttpGet]
        public async Task<List<IgracKlub>> IgracKlubovi([FromRoute(Name = "ime")] string search1)
        {
            return await _footballRepository.KluboviIgrac(search1);
        }

        [Route("/DodajIgraca/{k}/{ime}/{god}/{nacionalnost}")]
        [HttpPost]
        public void DodajIgraca([FromRoute(Name = "k")] string k, [FromRoute(Name = "ime")] string ime, [FromRoute(Name = "god")] int god, [FromRoute(Name = "nacionalnost")] string nacionalnost)
        {
            _footballRepository.DodajIgraca(k,ime,god,nacionalnost);
        }

        [Route("/DodajIgracaKlub/{naziv}/{ime}/{od}/{doo}/{broj_dresa}")]
        [HttpPost]
        public void DodajIgracaKlub([FromRoute(Name = "naziv")] string naziv, [FromRoute(Name = "ime")] string ime, [FromRoute(Name = "od")] int od, [FromRoute(Name = "doo")] int doo, [FromRoute(Name = "broj_dresa")] int broj_dresa)
        {
            _footballRepository.DodajIgracaKlub(naziv, ime, od, doo, broj_dresa); 
                }

        [Route("/ObrisiIgracKlub/{naziv}/{ime}")]
        [HttpDelete]
        public void ObrisiIgracKlub([FromRoute(Name = "naziv")] string naziv, [FromRoute(Name = "ime")] string ime)
        {
            _footballRepository.ObrisiIgracKlub(naziv, ime);
        }

        [Route("/Promeni/{ime}")]
        [HttpPut]
        public void Promeni([FromRoute(Name = "ime")] string ime)
        {
            _footballRepository.Promeni(ime);
        }

        [Route("/Promenidres/{ime}/{naziv}/{br}")]
        [HttpPut]
        public void Promenidres([FromRoute(Name = "ime")] string ime, [FromRoute(Name = "naziv")] string naziv, [FromRoute(Name = "br")] int br)
        {
            _footballRepository.Promenidres(ime,naziv,br);
        }
    }
}


