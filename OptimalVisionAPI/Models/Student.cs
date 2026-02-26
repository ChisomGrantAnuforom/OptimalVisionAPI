namespace OptimalVisionAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }

    [NotMapped]
    public double Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public string PreferredAcademicIntake { get; set; }
    public string MarritalStatus { get; set; }
    public bool HappyToTravelFirst { get; set; }
    public int YearOfLastAcademicStudies { get; set; }

    public string QualificationObtained { get; set; }

    public string ProgramOfStudy { get; set; }

    public string Grades { get; set; }
         
    public int YearOfCompletion { get; set; }

    public int Sponsor { get; set; }

    public decimal AvailableDeposit { get; set; }

    public bool AnyAgent { get; set; }

    public bool CanYouStopAgent { get; set; }

    public bool AnyVisaRefusal { get; set; }

    public bool AnyBan { get; set; }

    public bool AvailabilityOfMaintenanceFunds { get; set; }

    public bool ReadyToProceedNow { get; set; }

    public decimal TotalArriveAbroadBudget { get; set; }

    public string AreFundsAvailableNow { get; set; }

    public string TryYourLuckWithChosenCountryOrNot { get; set; }

    public DateTime DateApplied { get; set; }
}