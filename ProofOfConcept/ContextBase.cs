using Ninject;

namespace ProofOfConcept
{
    public class ContextBase
    {
        public IKernel Kernel
        {
            get; set;
        }

        public ContextBase()
        {
            Kernel = new StandardKernel();
            //Kernel.Bind<IDriverDecorator>().To<Selenium>()
        }
         
    }
}