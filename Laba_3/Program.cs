using System;
using System.Collections.Generic;

namespace Task_3
{

    abstract class Country_part
    {

        private string name;
        private int population;
        private double squere;
        private List<Country_part> parts;
        public Country_part(string name, int population, double squere, List<Country_part> parts)
        {
            this.name = name;
            this.population = population;
            this.squere = squere;
            this.parts = parts;
        }
        public int Get_population()
        {
            return population;
        }
        public double Get_squere()
        {
            return squere;
        }
        public string Get_name()
        {
            return name;
        }
        public List<Country_part> Get_parts()
        {
            return parts;
        }
        public void Set_population(int value)
        {
            if (value > 0)
            {
                population = value;
            }
            else
            {
                throw new ValueException("Only Positive Numbers and no Letters");
            }
        }
        public void Set_squere(double value)
        {
            if (value > 0)
            {
                squere = value;
            }
            else
            {
                throw new ValueException("Only Positive Numbers and no Letters");
            }
        }
    }

    class ValueException : Exception
    {
        public ValueException(string message) : base(message)
        { }
    }
    class Country
    {
        private string name;
        private City capital;
        private List<Region> regions;

        public Country(string name, City capital, params Region[] regions)
        {
            this.name = name;
            this.capital = capital;
            this.regions = new List<Region>(regions);
        }


        string Get_name()
        {
            return name;
        }
        City Get_capital()
        {
            return capital;
        }

        int Get_amount_of_regions()
        {
            return regions.Count; ;
        }
        double Get_squere()
        {
            List<Country_part> rg = new List<Country_part>(regions);
            return Count_squere(rg);
        }

        List<City> Get_regional_centers()
        {
            List<City> regional_centers = new List<City>();
            foreach (Region region in regions)
            {
                regional_centers.Add(region.Get_center());
            }

            return regional_centers;
        }

        private static double Count_squere(List<Country_part> parts)
        {
            double sum_of_squeres = 0;
            foreach (Country_part part in parts)
            {
                sum_of_squeres += part.Get_squere();
            }

            return sum_of_squeres;
        }

        public static int Count_population(List<Country_part> parts)
        {
            int population = 0;
            foreach (Country_part part in parts)
            {
                population += part.Get_population();
            }

            return population;
        }

        public class City : Country_part
        {

            public City(string name, params District[] districts) : base(name, Count_population(new List<Country_part>(districts)),
                Count_squere(new List<Country_part>(districts)), new List<Country_part>(districts))
            { }

        }

        public class Region : Country_part
        {
            private int region_code;
            private City center;

            public Region(string name, City center, int region_code, params City[] cities) : base(name, Count_population(new List<Country_part>(cities)),
                Count_squere(new List<Country_part>(cities)), new List<Country_part>(cities))
            {
                this.region_code = region_code;
                this.center = center;
            }
            public City Get_center()
            {
                return center;
            }

        }
        public class District : Country_part
        {

            public District(string name, int population, double squere) : base(name, population, squere, null)
            {
                Set_population(population);
                Set_squere(squere);

            }

        }

        class Program
        {
            static void Main(string[] args)
            {
                District north = new District("Северный", 1000, 30);
                District south = new District("Южный", 1000, 40);
                District soviet = new District("Советский", 1000, 50);
                City voronezh = new City("Воронеж", north, south);
                Region a = new Region("Воронежская обл", voronezh, 36, voronezh);
                District center = new District("Красная площадь", 20, 30.5);
                City moscov = new City("Москва", center);
                City lipetsk = new City("Липетск", soviet);
                Region b = new Region("Московская область", moscov, 99, moscov);
                Region c = new Region("Липецкая область", lipetsk, 47, lipetsk);
                Country russia = new Country("Россия", moscov, a, b, c);


                Console.WriteLine(russia.Get_name());
                Console.WriteLine("Столица: " + russia.Get_capital().Get_name());
                Console.WriteLine("Количество областей: " + russia.Get_amount_of_regions());
                Console.WriteLine("Площадь: " + russia.Get_squere());

                foreach (City city in russia.Get_regional_centers())
                {
                    Console.WriteLine("Региональный центр: " + city.Get_name());
                }
            }
        }
    }
}
