[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SchoolManagement.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SchoolManagement.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace SchoolManagement.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using SchoolManagement.Domain.Interfaces.Servicos;
    using SchoolManagement.Domain.Interfaces.Repositorios;
    using SchoolManagement.Data.Repositorios;
    using SchoolManagement.Domain.Servicos;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IServicoBase<>)).To(typeof(ServicoBase<>));
            kernel.Bind<IUsuarioServico>().To<UsuarioServico>();
            kernel.Bind<IAnoLetivoServico>().To<AnoLetivoServico>();
            kernel.Bind<IAlunoServico>().To<AlunoServico>();
            kernel.Bind<IFrequenciaServico>().To<FrequenciaServico>();
            kernel.Bind<IFuncionarioServico>().To<FuncionarioServico>();
            kernel.Bind<IProfessorServico>().To<ProfessorServico>();
            kernel.Bind<IResponsavelServico>().To<ResponsavelServico>();
            kernel.Bind<ITurmaServico>().To<TurmaServico>();
            kernel.Bind<IEventoServico>().To<EventoServico>();
            kernel.Bind<INotificacaoServico>().To<NotificacaoServico>();
            kernel.Bind<IDisciplinaServico>().To<DisciplinaServico>();
            kernel.Bind<ITrabalhosExtrasServico>().To<TrabalhosExtrasServico>();
            kernel.Bind<IProvaServico>().To<ProvaServico>();
            kernel.Bind<ILojasRecomendadasServico>().To<LojasRecomendadasServico>();
            kernel.Bind<ILivroServico>().To<LivroServico>();
            kernel.Bind<IResultadosProvasServico>().To<ResultadosProvasServico>();
            kernel.Bind<IConteudoExtraServico>().To<ConteudoExtraServico>();


            kernel.Bind(typeof(IRepositorioBase<>)).To(typeof(RepositorioBase<>));
            kernel.Bind<IRepositorioUsuario>().To<UsuarioRepositorio>();
            kernel.Bind<IAnoLetivoRepositorio>().To<AnoLetivoRepositorio>();
            kernel.Bind<IAlunoRepositorio>().To<AlunoRepositorio>();
            kernel.Bind<IFrequenciaRepositorio>().To<FrequenciaRepositorio>();
            kernel.Bind<IFuncionarioRepositorio>().To<FuncionarioRepositorio>();
            kernel.Bind<IProfessorRepositorio>().To<ProfessorRepositorio>();
            kernel.Bind<IResponsavelRepositorio>().To<ResponsavelRepositorio>();
            kernel.Bind<ITurmaRepositorio>().To<TurmaRepositorio>();
            kernel.Bind<IEventoRepositorio>().To<EventoRepositorio>();
            kernel.Bind<INotificacaoRepositorio>().To<NotificacaoRepositorio>();
            kernel.Bind<IDisciplinaRepositorio>().To<DisciplinaRepositorio>();
            kernel.Bind<ITrabalhosExtrasRepositorio>().To<TrabalhosExtrasRepositorio>();
            kernel.Bind<IProvaRepositorio>().To<ProvaRepositorio>();
            kernel.Bind<ILojasRecomendadasRepositorio>().To<LojasRecomendadasRepositorio>();
            kernel.Bind<ILivroRepositorio>().To<LivroRepositorio>();
            kernel.Bind<IResultadosProvasRepositorio>().To<ResultadosProvasRepositorio>();
            kernel.Bind<IConteudoExtraRepositorio>().To<ConteudosExtrasRepositorio>();
        }        
    }
}
