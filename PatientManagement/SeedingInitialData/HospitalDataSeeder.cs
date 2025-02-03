namespace SeedingInitialData;

public class HospitalDataSeeder
{
    public static async Task SeedHospitalDataAsync()
    {
        using (var context = new HospitalDbContext())
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Patients.Any())
            {
                int recordsNumber = 100;

                Console.WriteLine("Creating names...");
                var names = InitialHospitalData.GetInitialNames(recordsNumber);

                context.PatientNames.AddRange(names);
                await context.SaveChangesAsync();

                Console.WriteLine("Creating patients...");
                var patients = InitialHospitalData.GetInitialPatients(names, recordsNumber);

                context.Patients.AddRange(patients);
                await context.SaveChangesAsync();

                Console.WriteLine("Test data added successfully.");
            }
            else
            {
                Console.WriteLine("Test data already exists.");
            }
        }
    }
}