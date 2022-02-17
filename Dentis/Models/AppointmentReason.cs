namespace Dentis.Models
{
    public class AppointmentReason
    {
        public int AppointmentReasonId { get; set; }
        public string? AppointmentReasonName { get; set; }
    }
}

public class Rootobject
{
    public string ModelId { get; set; }
    public Policy[] Policy { get; set; }
}

public class Policy
{
    public string Prefix { get; set; }
    public string BranchOffice { get; set; }
    public int PolicyNumberThird { get; set; }
    public int Sufix { get; set; }
    public int Endorsement { get; set; }
    public string SalePoint { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime CurrentFrom { get; set; }
    public DateTime CurrentTo { get; set; }
    public string PolicyType { get; set; }
    public string Movement { get; set; }
    public string Movementtype { get; set; }
    public string BusinessType { get; set; }
    public string Currency { get; set; }
    public int ExchangeRate { get; set; }
    public string Billingtype { get; set; }
    public Agent[] Agent { get; set; }
    public string OperactionType { get; set; }
    public Clause[] Clause { get; set; }
    public string Texts { get; set; }
    public string Observations { get; set; }
    public Holder[] Holder { get; set; }
    public Payer[] Payers { get; set; }
    public Amountdetail[] AmountDetail { get; set; }
    public Paymentplan[] PaymentPlan { get; set; }
    public string AccountName { get; set; }
}

public class Agent
{
    public int AgentIndex { get; set; }
    public string TypeAgent { get; set; }
    public string Code { get; set; }
    public string IsPrincipal { get; set; }
    public int ParticipationPercentage { get; set; }
    public int Art41Percentage { get; set; }
    public Extensionagent[] ExtensionAgent { get; set; }
}

public class Extensionagent
{
    public int ComissIndex { get; set; }
    public string TechnicalPrefix { get; set; }
    public string SubPrefix { get; set; }
    public int NormalCommissionPercentage { get; set; }
    public int ExtraCommissionPercentage { get; set; }
}

public class Clause
{
    public int Description { get; set; }
}

public class Holder
{
    public string DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public Naturalperson[] NaturalPerson { get; set; }
}

public class Naturalperson
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string SecondSurname { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public DateTime BirthDate { get; set; }
    public string NIT { get; set; }
    public string CURP { get; set; }
    public string Persontype { get; set; }
    public string BirthPlace { get; set; }
    //public Email[] Email { get; set; }
    public Phone[] Phone { get; set; }
    public Address[] Address { get; set; }
}

//public class Email
//{
//    public string Type { get; set; }
//    public string? Email { get; set; }
//}

public class Phone
{
    public string Type { get; set; }
    public string PhoneNumber { get; set; }
}

public class Address
{
    public string Type { get; set; }
    public int Number { get; set; }
    public string Cologne { get; set; }
    public string Municipality { get; set; }
    public string ZipCode { get; set; }
    public string Addess { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}

public class Payer
{
    public int PayerItem { get; set; }
    public int PayerIndex { get; set; }
    public int Payerpercentage { get; set; }
    public string PayerPaymentPlan { get; set; }
    public string PayerPaymentMethod { get; set; }
    public int IndexPaymentMethod { get; set; }
    public string AccountCardNumber { get; set; }
    public Naturalperson1[] NaturalPerson { get; set; }
}

public class Naturalperson1
{
    public string DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string SecondSurname { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public DateTime BirthDate { get; set; }
    public string NIT { get; set; }
    public string CURP { get; set; }
    public string Persontype { get; set; }
    public string BirthPlace { get; set; }
    //public Email1[] Email { get; set; }
    public Phone1[] Phone { get; set; }
    public Address1[] Address { get; set; }
}

//public class Email1
//{
//    public string Type { get; set; }
//    public string Email { get; set; }
//}

public class Phone1
{
    public string Type { get; set; }
    public string PhoneNumber { get; set; }
}

public class Address1
{
    public string Type { get; set; }
    public int Number { get; set; }
    public string Cologne { get; set; }
    public string Municipality { get; set; }
    public string ZipCode { get; set; }
    public string Addess { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}

public class Amountdetail
{
    public int InsuredAmount { get; set; }
    public int PremiumAmount { get; set; }
    public int ExpensesAmount { get; set; }
    public Extensionamount[] ExtensionAmount { get; set; }
    public Tax[] Tax { get; set; }
    public float TotalPremiumAmount { get; set; }
}

public class Extensionamount
{
    public int DiscountWithIVA { get; set; }
    public int SurchargeFinancial { get; set; }
}

public class Tax
{
    public int Percentaje { get; set; }
    public float Amount { get; set; }
}

public class Paymentplan
{
    public string Way { get; set; }
}
