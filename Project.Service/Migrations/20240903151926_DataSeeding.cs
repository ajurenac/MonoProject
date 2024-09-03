using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Seed(migrationBuilder);
        }

        private void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleMake",
                columns: new[] { "Name", "Abrv" },
                values: new object[,]
                {
                    { "Volkswagen", "VW" },
                    { "Audi", "AUDI" },
                    { "Mercedes-Benz", "MB" },
                    { "Renault", "REN" },
                    { "Peugeot", "PEU" },
                    { "Fiat", "FIAT" },
                    { "Volvo", "VOL" },
                    { "Skoda", "SKD" },
                    { "SEAT", "SEAT" },
                    { "Opel", "OPL" }
                });

            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Name", "Abrv", "MakeId" },
                values: new object[,]
                {
                    { "Golf", "GLF", 1 },
                    { "Passat", "PAS", 1 },
                    { "Polo", "POL", 1 },
                    { "Tiguan", "TIG", 1 },
                    { "Touareg", "TOU", 1 },
                    { "T-Cross", "TC", 1 },
                    { "Arteon", "ART", 1 },
                    { "ID.3", "ID3", 1 },
                    { "ID.4", "ID4", 1 },
                    { "Touran", "TOU", 1 },

                    { "A3", "A3", 2 },
                    { "A4", "A4", 2 },
                    { "A6", "A6", 2 },
                    { "A7", "A7", 2 },
                    { "Q2", "Q2", 2 },
                    { "Q3", "Q3", 2 },
                    { "Q5", "Q5", 2 },
                    { "Q7", "Q7", 2 },
                    { "Q8", "Q8", 2 },

                    { "A-Class", "ACL", 3 },
                    { "C-Class", "CCL", 3 },
                    { "E-Class", "ECL", 3 },
                    { "S-Class", "SCL", 3 },
                    { "GLA", "GLA", 3 },
                    { "GLC", "GLC", 3 },
                    { "GLE", "GLE", 3 },
                    { "GLS", "GLS", 3 },
                    { "CLA", "CLA", 3 },
                    { "EQC", "EQC", 3 },

                    { "Clio", "CLI", 4 },
                    { "Megane", "MEG", 4 },
                    { "Captur", "CAP", 4 },
                    { "Kadjar", "KAD", 4 },
                    { "Koleos", "KOL", 4 },
                    { "Zoe", "ZOE", 4 },
                    { "Twingo", "TWI", 4 },
                    { "Scenic", "SCE", 4 },
                    { "Espace", "ESP", 4 },
                    { "Arkana", "ARK", 4 },

                    { "208", "208", 5 },
                    { "308", "308", 5 },
                    { "2008", "2008", 5 },
                    { "3008", "3008", 5 },
                    { "5008", "5008", 5 },
                    { "508", "508", 5 },
                    { "Rifter", "RIF", 5 },
                    { "Traveller", "TRA", 5 },
                    { "Partner", "PAR", 5 },
                    { "Expert", "EXP", 5 },

                    { "500", "500", 6 },
                    { "Panda", "PAN", 6 },
                    { "Tipo", "TIP", 6 },
                    { "500X", "500X", 6 },
                    { "500L", "500L", 6 },
                    { "Punto", "PUN", 6 },
                    { "Doblo", "DOB", 6 },
                    { "Qubo", "QUB", 6 },
                    { "Toro", "TOR", 6 },
                    { "Fiorino", "FIO", 6 },

                    { "XC40", "XC40", 7 },
                    { "XC60", "XC60", 7 },
                    { "XC90", "XC90", 7 },
                    { "S60", "S60", 7 },
                    { "S90", "S90", 7 },
                    { "V40", "V40", 7 },
                    { "V60", "V60", 7 },
                    { "V90", "V90", 7 },
                    { "C40", "C40", 7 },
                    { "Polestar", "POL", 7 },

                    { "Fabia", "FAB", 8 },
                    { "Octavia", "OCT", 8 },
                    { "Superb", "SUP", 8 },
                    { "Kamiq", "KAM", 8 },
                    { "Karoq", "KAR", 8 },
                    { "Kodiaq", "KOD", 8 },
                    { "Scala", "SCA", 8 },
                    { "Enyaq", "ENY", 8 },
                    { "Citigo", "CIT", 8 },
                    { "Rapid", "RAP", 8 },

                    { "Ibiza", "IBI", 9 },
                    { "Leon", "LEO", 9 },
                    { "Ateca", "ATE", 9 },
                    { "Tarraco", "TAR", 9 },
                    { "Arona", "ARO", 9 },
                    { "Alhambra", "ALH", 9 },
                    { "Toledo", "TOL", 9 },
                    { "Mii", "MII", 9 },
                    { "Exeo", "EXE", 9 },
                    { "Cupra Formentor", "CPF", 9 },

                    { "Astra", "AST", 10 },
                    { "Corsa", "COR", 10 },
                    { "Mokka", "MOK", 10 },
                    { "Insignia", "INS", 10 },
                    { "Grandland X", "GLX", 10 },
                    { "Crossland X", "CLX", 10 },
                    { "Zafira", "ZAF", 10 },
                    { "Combo", "COM", 10 },
                    { "Vivaro", "VIV", 10 },
                    { "Movano", "MOV", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

