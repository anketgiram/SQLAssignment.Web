using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignmnet.EntityLayer.Models;

public partial class Vendor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vendor Code is required.")]
    [StringLength(5, ErrorMessage = "Vendor Code must be exactly 5 characters long.", MinimumLength = 5)]
    public string Code { get; set; } = null!;
    [Required(ErrorMessage = "Vendor Name is required.")]
    [StringLength(100, ErrorMessage = "Vendor Name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Address Line 1 is required.")]
    [StringLength(250, ErrorMessage = "Address Line 1 cannot exceed 250 characters.")]
    public string? AddressLine1 { get; set; }
    [StringLength(250, ErrorMessage = "Address Line 2 cannot exceed 250 characters.")]
    public string? AddressLine2 { get; set; }
    [Required(ErrorMessage = "Contact Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email format.")]
    public string? ContactEmail { get; set; }
    [Required(ErrorMessage = "Contact Number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 digits.")]
    public string? ContactNo { get; set; }
    [Required(ErrorMessage = "Valid Till Date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid Date format.")]
    public DateOnly? ValidTillDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();
}
