using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace SeedingInitialData;

public class InitialHospitalData
{
    public static List<Name> GetInitialNames(int count)
    {
        var names = GenerateNames(count);
        return names;
    }

    public static List<Patient> GetInitialPatients(List<Name> names, int count)
    {
        var patients = GeneratePatients(names, count);
        return patients;
    }

    private static List<Patient> GeneratePatients(List<Name> names, int count)
    {
        var patients = new List<Patient>();
        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            Gender gender = (Gender)random.Next(0, 3);

            DateTime birthDate = new DateTime(
                year: random.Next(1990, 2024),
                month: random.Next(1, 13),
                day: random.Next(1, 29),
                hour: random.Next(1, 24),
                minute: random.Next(0, 59),
                second: random.Next(0, 59),
                kind: DateTimeKind.Utc);

            patients.Add(new Patient
            {
                Id = Guid.NewGuid(),
                Gender = gender,
                BirthDate = birthDate,
                Active = true,
                NameId = names[i].Id
            });
        }

        return patients;
    }

    private static List<Name> GenerateNames(int count)
    {
        var names = new List<Name>();
        var uses = new[] { "Official", "Nickname", "Alias" };
        var firstNames = new[]
        {
            "John", "Jane", "Alice", "Bob", "Charlie", "David", "Eva", "Fiona", "George", "Hannah",
            "Ian", "Jack", "Kathy", "Liam", "Mia", "Noah", "Olivia", "Paul", "Quinn", "Rachel",
            "Sam", "Tina", "Uma", "Victor", "Wendy", "Xander", "Yara", "Zoe", "Aiden", "Bella",
            "Carter", "Daisy", "Ethan", "Freya", "Gavin", "Holly", "Isaac", "Jasmine", "Kevin", "Lily"
        };
        var lastNames = new[]
        {
            "Doe", "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore",
            "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez",
            "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez"
        };
        var families = new[]
        {
            "Soros", "Kidani", "Stone", "Johnson", "Smith", "Brown", "Davis", "Miller", "Wilson", "Moore",
            "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez"
        };

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            string use = uses[random.Next(uses.Length)];
            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];
            string family = families[random.Next(families.Length)];

            names.Add(new Name(use, firstName, lastName, family));
        }

        return names;
    }
}
