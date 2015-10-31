namespace ProofOfConcept.Services
{
    public class DriverService
    {
        //TODO: driver must be private. Provide a list of methods to manipulate driver instead.
        public readonly IDriverDecorator Driver;

        public DriverService(IDriverDecorator driver)
        {
            Driver = driver;
        }
    }
}