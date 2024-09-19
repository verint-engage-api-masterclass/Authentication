namespace Authentication1;


public class Employees
{
    public Datum[] data { get; set; }
}

public class Datum
{
    public string id { get; set; }
    public string type { get; set; }
    public Attributes attributes { get; set; }
    public Relationships relationships { get; set; }
    public Links1 links { get; set; }
}

public class Attributes
{
    public int qmAnalyticsId { get; set; }
    public Person person { get; set; }
    public User user { get; set; }
    public string employeeType { get; set; }
    public string employeeNumber { get; set; }
    public int organizationId { get; set; }
    public DateTime startTime { get; set; }
    public DateTime? endTime { get; set; }
    public bool isSupervisor { get; set; }
    public bool isTeamLead { get; set; }
}

public class Person
{
    public Address address { get; set; }
    public Contact contact { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string middleInitial { get; set; }
    public string ssn { get; set; }
    public DateTime? birthDate { get; set; }
}

public class Address
{
    public string addressLine1 { get; set; }
    public string addressLine2 { get; set; }
    public string addressLine3 { get; set; }
    public string city { get; set; }
    public string stateName { get; set; }
    public string zipCode { get; set; }
    public string country { get; set; }
}

public class Contact
{
    public string email { get; set; }
    public string desktopMessagingUsername { get; set; }
    public string homePhone { get; set; }
    public string workPhone { get; set; }
    public string cellPhone { get; set; }
}

public class User
{
    public string username { get; set; }
    public string status { get; set; }
}

public class Relationships
{
    public Organization organization { get; set; }
}

public class Organization
{
    public Links links { get; set; }
    public Data data { get; set; }
}

public class Links
{
    public string related { get; set; }
}

public class Data
{
    public string id { get; set; }
    public string type { get; set; }
    public Meta meta { get; set; }
}

public class Meta
{
    public string name { get; set; }
}

public class Links1
{
    public string self { get; set; }
}
