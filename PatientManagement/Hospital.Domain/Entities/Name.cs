﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Entities;

public class Name
{
    public Guid Id { get; set; }
    public string Use { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Family { get; set; }

    [NotMapped]
    public List<string> Given
    {
        get
        {
            var givenNames = new List<string>();
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                givenNames.Add(FirstName);
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                givenNames.Add(LastName);
            }
            return givenNames;
        }
    }
}
