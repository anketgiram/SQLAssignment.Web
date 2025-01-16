using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignmnet.EntityLayer.Models;

public partial class Purchaseorderdetail
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Purchase Order ID is required.")]
    public int PurchaseOrderId { get; set; }
    [Required(ErrorMessage = "Material is required.")]
    public int MaterialId { get; set; }
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
    [Required(ErrorMessage = "Rate is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0.")]
    public decimal Rate { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative number.")]
    public decimal Amount { get; set; }
    [Required(ErrorMessage = "Expected Date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid Date format.")]
    public DateOnly ExpectedDate { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Purchaseorder PurchaseOrder { get; set; } = null!;
}
