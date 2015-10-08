namespace ProofOfConcept.Tests.TestObjects
{
    class LoginPage : IPage
    {

        //URL: https://roadshowaccess.qx.ipreo.com/
        //Login: 

        public IElement LoginField;
        public IElement PasswordField;

        public string Login = "qainvestor@ipreo.com";
        public string Password = "Password1";
    }
}
