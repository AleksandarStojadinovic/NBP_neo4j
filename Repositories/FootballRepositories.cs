using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazeneo4j.Models;
using Neo4j.Driver;


namespace Bazeneo4j.Repositories
{
    public interface IFootballRepository
    {
        Task<List<Igrac>> Search(string search);
        Task<List<Igrac>> Search1(string search1, string search2);
        Task<List<Igrac>> Search2(string search1,string search2,string search3);
        Task<List<String>> SearchKlub();
        Task<List<String>> ComboIgraci();
        Task<List<String>> ComboLige();

        Task<List<String>> KluboviLiga(string search1);

        public void ObrisiIgracKlub(string naziv, string ime);

        public void Promeni(string ime);

        public void DodajIgraca(string k, string ime, int god, string nacionalnost);

        public void DodajIgracaKlub(string naziv, string ime, int od, int doo, int broj_dresa);

        public void Promenidres(string ime, string naziv,int br);
        Task<List<IgracKlub>> KluboviIgrac(string search1);
        Task<List<String>> IgraliZajedno(string search1, string search2);
    }
    public class FootballRepositories : IFootballRepository
    {
        private readonly IDriver _driver;

        public FootballRepositories(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<List<Igrac>> Search(string search)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (n)<-[r:igra_za]-(a) WHERE n.Naziv is not null and n.Naziv= "+search+" RETURN a.Ime as ime, a.Nacionalnost as nacionalnost, a.Godina_rodjenja as godina_rodjenja"
                    );

                    return await cursor.ToListAsync(record => new Igrac(
                        ime: record["ime"].As<string>(),
                        nacionalnost: record["nacionalnost"].As<string>(),
                        godina_rodjenja: record["godina_rodjenja"].As<int>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Igrac>> Search1(string search1, string search2)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"WITH ['"+search1+"','"+search2+ "'] as klubovi MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE n.Naziv is not null and n.Naziv in klubovi WITH a, size(klubovi) as inputCnt, count(a) as cnt WHERE inputCnt=cnt RETURN a.Ime as ime, a.Nacionalnost as nacionalnost, a.Godina_rodjenja as godina_rodjenja");
                    return await cursor.ToListAsync(record => new Igrac(
                        ime: record["ime"].As<string>(),
                        nacionalnost: record["nacionalnost"].As<string>(),
                        godina_rodjenja: record["godina_rodjenja"].As<int>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Igrac>> Search2(string search1, string search2, string search3)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"WITH ['" + search1 + "','" + search2 +"','" + search3 + "'] as klubovi MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE n.Naziv is not null and n.Naziv in klubovi WITH a, size(klubovi) as inputCnt, count(a) as cnt WHERE inputCnt=cnt RETURN a.Ime as ime, a.Nacionalnost as nacionalnost, a.Godina_rodjenja as godina_rodjenja");
                    return await cursor.ToListAsync(record => new Igrac(
                        ime: record["ime"].As<string>(),
                        nacionalnost: record["nacionalnost"].As<string>(),
                        godina_rodjenja: record["godina_rodjenja"].As<int>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<String>> SearchKlub()
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (n:Klub) RETURN n.Naziv as naziv"
                    );

                    return await cursor.ToListAsync(record => new String(
                        record["naziv"].As<string>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<String>> ComboIgraci()
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (n:Igrac) RETURN n.Ime as ime"
                    );

                    return await cursor.ToListAsync(record => new String(
                        record["ime"].As<string>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<String>> IgraliZajedno(string search1, string search2)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"WITH ['" + search1 + "','" + search2 + "'] as igraci MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE a.Ime is not null and a.Ime in igraci WITH n, size(igraci) as inputCnt, count(a) as cnt WHERE cnt=inputCnt RETURN n.Naziv as naziv");
                    
                    
                    return await cursor.ToListAsync(record => new String(
                        record["naziv"].As<string>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<String>> KluboviLiga(string search1)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"MATCH (n:Liga)<-[r1:igra_u]-(a:Klub) WHERE n.Naziv='"+search1 +"' RETURN a.Naziv as naziv");


                    return await cursor.ToListAsync(record => new String(
                        record["naziv"].As<string>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IgracKlub>> KluboviIgrac(string search1)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE a.Ime='"+search1+"' RETURN n.Naziv as naziv,r.Od as od,r.Do as do,r.Broj_dresa as broj_dresa");


                    return await cursor.ToListAsync(record => new IgracKlub(
                        klub: record["naziv"].As<string>(),
                        od: record["od"].As<int>(),
                        doo: record["do"].As<int>(),
                        broj_dresa: record["broj_dresa"].As<int>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<String>> ComboLige()
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (n:Liga) RETURN n.Naziv as naziv"
                    );

                    return await cursor.ToListAsync(record => new String(
                        record["naziv"].As<string>()
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public void DodajIgraca(string k,string ime,int god,string nacionalnost)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                session.WriteTransactionAsync(async transaction =>
                {
                        await transaction.RunAsync(@"
                        CREATE("+k+":Igrac {Ime: '"+ime+"', Godina_rodjenja:"+god.ToString()+ ", Nacionalnost:'"+nacionalnost+"'})"
                    );

                    
                });
            }
            finally
            {
                session.CloseAsync();
            }
        }

        public void DodajIgracaKlub(string naziv, string ime, int od, int doo,int broj_dresa)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                session.WriteTransactionAsync(async transaction =>
                {
                    await transaction.RunAsync(@"
                        MATCH (p:Igrac) MATCH (l:Klub) WHERE p.Ime='"+ ime +"' and l.Naziv='"+naziv+ "'CREATE (p)-[:igra_za {Od:"+od.ToString()+",Do:"+doo.ToString()+",Broj_dresa:"+broj_dresa.ToString()+"}]->(l)"
                );


                });
            }
            finally
            {
                session.CloseAsync();
            }
        }

        public void Promeni(string ime)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                session.WriteTransactionAsync(async transaction =>
                {
                    await transaction.RunAsync(@"
                        MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE r.Do=0 and a.Ime='"+ime+"' SET r.Do=2023"
                );


                });
            }
            finally
            {
                session.CloseAsync();
            }
        }

        public void Promenidres(string ime, string naziv,int br)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                session.WriteTransactionAsync(async transaction =>
                {
                    await transaction.RunAsync(@"
                        MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE a.Ime='"+ime+"' and n.Naziv='"+naziv+"' SET r.Broj_dresa="+br.ToString()+""
                );
                    int x=5;

                });
            }
            finally
            {
                session.CloseAsync();
            }
        }

        public async void ObrisiIgracKlub(string naziv, string ime)
        {

            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                session.WriteTransactionAsync(async transaction =>
                {
                    await transaction.RunAsync(@"
                        MATCH (n:Klub)<-[r:igra_za]-(a:Igrac) WHERE a.Ime='"+ime+"' and n.Naziv='"+naziv+"' DELETE r"
                );


                });
            }
            finally
            {
                session.CloseAsync();
            }
        }

        private static void WithDatabase(SessionConfigBuilder sessionConfigBuilder)
        {
            var neo4jVersion = System.Environment.GetEnvironmentVariable("NEO4J_VERSION") ?? "";
            if (!neo4jVersion.StartsWith("5"))
            {
                return;
            }

            sessionConfigBuilder.WithDatabase(Database());
        }

        private static string Database()
        {
            return System.Environment.GetEnvironmentVariable("NEO4J_DATABASE") ?? "neo4j";
        }
    }
}




  
    

        

        
        

        
/*
        private static IEnumerable<Person> MapCast(IEnumerable<IDictionary<string, object>> persons)
        {
            return persons
                .Select(dictionary => new Person(
                    dictionary["name"].As<string>(),
                    dictionary["job"].As<string>(),
                    dictionary["role"].As<string>()
                ))
                .ToList();
        }

      
}*/