using System;
using System.Collections.Generic;
using System.Linq;
using DataService;
using DataService.Objects;
using DataService.Services;
using DataService.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic.CompilerServices;
using Npgsql;

namespace TestQueryConsole
{
    class Program
    {


        static void Main(string[] args)
        {
            using var ctx = new ImdbContext();
            
            var result = ctx.Movies.FromSqlInterpolated($"select * from titlesformoviepage()");

            foreach (var x in result)
            {
                Console.WriteLine($"{x.title_id}, {x.type}");
            }

            
        }

        private static void Top10Poster()
        {
            PostgresSQL_Connect_String connectionString = new PostgresSQL_Connect_String();
            var connection = new NpgsqlConnection(connectionString.ToString());
            connection.Open();
            var command = new NpgsqlCommand("select * from Top10HomePoster();", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0)}{reader.GetString(1)}{reader.GetString(2)}");
            }
        }
    }
}