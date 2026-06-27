using System;
using System.Collections.Generic;

namespace HospitalWardManagement
{
    public interface IDisplayable
    {
        void Display();
    }

public abstract class Person : IDisplayable
    {
        public string PersonID { get; set; }
        public string Name { get; set; }

        public Person(string personID, string name)
        {
            PersonID = personID;
            Name = name;
        }

        public abstract string GetRole();

        public virtual void Display()
        {
            Console.WriteLine($"[{GetRole()}] ID: {PersonID} | Name: {Name}");
        }
    }

    public class Patient : Person
    {
        public string Diagnosis { get; set; }

        public Patient(string personID, string name, string diagnosis)
            : base(personID, name)
        {
            Diagnosis = diagnosis;
        }

        public override string GetRole()
        {
            return "Patient";
        }

        public override void Display()
        {
            Console.WriteLine($"[{GetRole()}] ID: {PersonID} | Name: {Name} | Diagnosis: {Diagnosis}");
        }
    }

    public class Doctor : Person
    {
        public string Specialisation { get; set; }

        public Doctor(string personID, string name, string specialisation)
            : base(personID, name)
        {
            Specialisation = specialisation;
        }

        public override string GetRole()
        {
            return "Doctor";
        }

        public override void Display()
        {
            Console.WriteLine($"[{GetRole()}] ID: {PersonID} | Name: {Name} | Specialisation: {Specialisation}");
        }
    }

    public class Ward
    {
        private Dictionary<string, List<Patient>> wards;
        private List<Doctor> doctors;

        public Ward()
        {
            wards = new Dictionary<string, List<Patient>>();
            doctors = new List<Doctor>();

            wards["General"] = new List<Patient>();
            wards["Paediatric"] = new List<Patient>();
            wards["Intensive Care"] = new List<Patient>();
        }

        public void RegisterDoctor(Doctor doctor)
        {
            doctors.Add(doctor);
            Console.WriteLine("Doctor is registered successfully.");
        }

        public void AdmitPatient(string wardName, Patient patient)
        {
            if (wards.ContainsKey(wardName))
            {
                if (wards[wardName].Count < 4)
                {
                    wards[wardName].Add(patient);
                    Console.WriteLine("Patient admitted successfully.");
                }
                else
                {
                    Console.WriteLine("The ward is full.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ward.");
            }
        }

        public void DisplayAll()
        {
            List<Person> allPersons = new List<Person>();

            allPersons.AddRange(doctors);

            foreach (var ward in wards.Values)
            {
                allPersons.AddRange(ward);
            }

            Console.WriteLine("\n===== All Hospital Persons =====");

            foreach (Person person in allPersons)
            {
                person.Display();
            }
        }
    }

    internal class Program
    {
        static void Main()
        {
            Ward hospital = new Ward();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== MediCore Hospital Management =====");
                Console.WriteLine("1. Admit Patient");
                Console.WriteLine("2. Register Doctor");
                Console.WriteLine("3. Display All Persons");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Patient ID: ");
                        string patientID = Console.ReadLine() ?? "";

                        Console.Write("Enter Patient Name: ");
                        string patientName = Console.ReadLine() ?? "";

                        Console.Write("Enter Diagnosis: ");
                        string diagnosis = Console.ReadLine() ?? "";

                        Console.Write("Enter Ward (General, Paediatric, Intensive Care): ");
                        string wardName = Console.ReadLine() ?? "";

                        Patient patient = new Patient(patientID, patientName, diagnosis);
                        hospital.AdmitPatient(wardName, patient);
                        break;

                    case "2":
                        Console.Write("Enter Doctor ID: ");
                        string doctorID = Console.ReadLine() ?? "";

                        Console.Write("Enter Doctor Name: ");
                        string doctorName = Console.ReadLine() ?? "";

                        Console.Write("Enter Specialisation: ");
                        string specialisation = Console.ReadLine() ?? "";

                        Doctor doctor = new Doctor(doctorID, doctorName, specialisation);
                        hospital.RegisterDoctor(doctor);
                        break;

                    case "3":
                        hospital.DisplayAll();
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
