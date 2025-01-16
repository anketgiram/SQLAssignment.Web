using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignmnet.EntityLayer.Models;

public partial class Purchaseorder
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Order Number is required.")]
    [StringLength(20, ErrorMessage = "Order Number cannot exceed 20 characters.")]
    public string OrderNo { get; set; } = null!;

    [Required(ErrorMessage = "Order Date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid Date format.")]
    public DateOnly OrderDate { get; set; }
    [Required(ErrorMessage = "Vendor is required.")]
    public int VendorId { get; set; }
    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Order Value must be a positive number.")]
    public decimal OrderValue { get; set; }

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    public virtual Vendor Vendor { get; set; } = null!;
}
