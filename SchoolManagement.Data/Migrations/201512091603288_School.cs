namespace SchoolManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class School : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataNascimento = c.DateTime(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        Rg = c.String(maxLength: 100, unicode: false),
                        Cpf = c.String(maxLength: 100, unicode: false),
                        Nacionalidade = c.String(maxLength: 100, unicode: false),
                        Naturalidade = c.String(maxLength: 100, unicode: false),
                        Foto = c.Byte(),
                        Sexo = c.Int(nullable: false),
                        Endereco_NomeRua = c.String(maxLength: 100, unicode: false),
                        Endereco_Numero = c.Int(),
                        Endereco_Cep = c.String(maxLength: 100, unicode: false),
                        Endereco_Bairro = c.String(maxLength: 100, unicode: false),
                        Endereco_Complemento = c.String(maxLength: 100, unicode: false),
                        Endereco_Cidade = c.String(maxLength: 100, unicode: false),
                        Endereco_Estado = c.String(maxLength: 100, unicode: false),
                        Endereco_Pais = c.String(maxLength: 100, unicode: false),
                        Contato_Email = c.String(maxLength: 100, unicode: false),
                        Contato_Telefone = c.String(maxLength: 100, unicode: false),
                        Contato_Celular = c.String(maxLength: 100, unicode: false),
                        UserLogin = c.String(nullable: false, maxLength: 50, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 20, unicode: false),
                        indicadorAcesso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Turma",
                c => new
                    {
                        TurmaId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        HorariosTurmaId = c.Int(nullable: false),
                        Vagas = c.Int(nullable: false),
                        AnoLetivo_AnoLetivoId = c.Int(),
                    })
                .PrimaryKey(t => t.TurmaId)
                .ForeignKey("dbo.AnoLetivo", t => t.AnoLetivo_AnoLetivoId)
                .Index(t => t.AnoLetivo_AnoLetivoId);
            
            CreateTable(
                "dbo.AnoLetivo",
                c => new
                    {
                        AnoLetivoId = c.Int(nullable: false, identity: true),
                        QntUnidades = c.Int(nullable: false),
                        Ano = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnoLetivoId);
            
            CreateTable(
                "dbo.Disciplina",
                c => new
                    {
                        DisciplinaId = c.Int(nullable: false, identity: true),
                        NomeDisciplina = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.DisciplinaId);
            
            CreateTable(
                "dbo.Livro",
                c => new
                    {
                        LivroId = c.Int(nullable: false, identity: true),
                        NomeLivro = c.String(maxLength: 100, unicode: false),
                        Autor = c.String(maxLength: 100, unicode: false),
                        Editora = c.String(maxLength: 100, unicode: false),
                        Disciplina_DisciplinaId = c.Int(),
                        Turma_TurmaId = c.Int(),
                    })
                .PrimaryKey(t => t.LivroId)
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_DisciplinaId)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Disciplina_DisciplinaId)
                .Index(t => t.Turma_TurmaId);
            
            CreateTable(
                "dbo.Experiencia",
                c => new
                    {
                        ExperienciaId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Funcao = c.String(maxLength: 100, unicode: false),
                        AnoEntrada = c.DateTime(nullable: false),
                        AnoSaida = c.DateTime(nullable: false),
                        Funcionario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ExperienciaId)
                .ForeignKey("dbo.Funcionario", t => t.Funcionario_Id)
                .Index(t => t.Funcionario_Id);
            
            CreateTable(
                "dbo.Boleto",
                c => new
                    {
                        BoletoId = c.Int(nullable: false, identity: true),
                        DataVencimento = c.DateTime(nullable: false),
                        DataHoraCriacao = c.DateTime(nullable: false),
                        Anexo = c.String(maxLength: 100, unicode: false),
                        DataPagamento = c.DateTime(nullable: false),
                        StatusPagamento = c.Int(nullable: false),
                        Aluno_Id = c.Int(),
                    })
                .PrimaryKey(t => t.BoletoId)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .Index(t => t.Aluno_Id);
            
            CreateTable(
                "dbo.FaleConosco",
                c => new
                    {
                        ContatoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        Mensagem = c.String(maxLength: 100, unicode: false),
                        TipoContato = c.Int(nullable: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        DataHoraEnvio = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContatoId);
            
            CreateTable(
                "dbo.ConteudosExtras",
                c => new
                    {
                        ConteudoId = c.Int(nullable: false, identity: true),
                        DataHoraCriacao = c.DateTime(nullable: false),
                        Anexo = c.String(maxLength: 100, unicode: false),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Professor_Id = c.Int(),
                        TurmaPublicoAlvo_TurmaId = c.Int(),
                    })
                .PrimaryKey(t => t.ConteudoId)
                .ForeignKey("dbo.Professor", t => t.Professor_Id)
                .ForeignKey("dbo.Turma", t => t.TurmaPublicoAlvo_TurmaId)
                .Index(t => t.Professor_Id)
                .Index(t => t.TurmaPublicoAlvo_TurmaId);
            
            CreateTable(
                "dbo.Contrato",
                c => new
                    {
                        ContratoId = c.Int(nullable: false, identity: true),
                        DataInicioContrato = c.DateTime(nullable: false),
                        ValorDesconto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataValidadeMensal = c.DateTime(nullable: false),
                        DataPgtoMensalidade = c.DateTime(nullable: false),
                        ValorMensalidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aluno_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ContratoId)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .Index(t => t.Aluno_Id);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        EventoId = c.Int(nullable: false, identity: true),
                        Local = c.String(maxLength: 100, unicode: false),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataEvento = c.DateTime(nullable: false),
                        NecessidadeAprovacao = c.Boolean(nullable: false),
                        PrecoEvento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NomeAcompanhante = c.String(maxLength: 100, unicode: false),
                        FuncionarioResponsavel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EventoId)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioResponsavel_Id)
                .Index(t => t.FuncionarioResponsavel_Id);
            
            CreateTable(
                "dbo.Frequencia",
                c => new
                    {
                        FrequenciaId = c.Int(nullable: false, identity: true),
                        DataReferencia = c.DateTime(nullable: false),
                        Presente = c.Boolean(nullable: false),
                        Aluno_Id = c.Int(),
                        Disciplina_DisciplinaId = c.Int(),
                    })
                .PrimaryKey(t => t.FrequenciaId)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_DisciplinaId)
                .Index(t => t.Aluno_Id)
                .Index(t => t.Disciplina_DisciplinaId);
            
            CreateTable(
                "dbo.LojasRecomendadas",
                c => new
                    {
                        LojaId = c.Int(nullable: false),
                        NomeLoja = c.String(maxLength: 100, unicode: false),
                        Endereco_NomeRua = c.String(maxLength: 100, unicode: false),
                        Endereco_Numero = c.Int(),
                        Endereco_Cep = c.String(maxLength: 100, unicode: false),
                        Endereco_Bairro = c.String(maxLength: 100, unicode: false),
                        Endereco_Complemento = c.String(maxLength: 100, unicode: false),
                        Endereco_Cidade = c.String(maxLength: 100, unicode: false),
                        Endereco_Estado = c.String(maxLength: 100, unicode: false),
                        Endereco_Pais = c.String(maxLength: 100, unicode: false),
                        Contato_Email = c.String(maxLength: 100, unicode: false),
                        Contato_Telefone = c.String(maxLength: 100, unicode: false),
                        Contato_Celular = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.LojaId);
            
            CreateTable(
                "dbo.Notificacao",
                c => new
                    {
                        NotificacaoId = c.Int(nullable: false, identity: true),
                        Assunto = c.String(maxLength: 1000, unicode: false),
                        Descricao = c.String(maxLength: 1000, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        TurmaPublicoAlvo_TurmaId = c.Int(),
                        UsuarioCriacao_Id = c.Int(),
                    })
                .PrimaryKey(t => t.NotificacaoId)
                .ForeignKey("dbo.Turma", t => t.TurmaPublicoAlvo_TurmaId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioCriacao_Id)
                .Index(t => t.TurmaPublicoAlvo_TurmaId)
                .Index(t => t.UsuarioCriacao_Id);
            
            CreateTable(
                "dbo.Prova",
                c => new
                    {
                        ProvaId = c.Int(nullable: false, identity: true),
                        DataProva = c.DateTime(nullable: false),
                        Unidade = c.Int(nullable: false),
                        StatusProva = c.Int(nullable: false),
                        TipoProva = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(),
                        Professores_Id = c.Int(),
                        Turma_TurmaId = c.Int(),
                    })
                .PrimaryKey(t => t.ProvaId)
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_DisciplinaId)
                .ForeignKey("dbo.Professor", t => t.Professores_Id)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Disciplina_DisciplinaId)
                .Index(t => t.Professores_Id)
                .Index(t => t.Turma_TurmaId);
            
            CreateTable(
                "dbo.Rematricula",
                c => new
                    {
                        OperacaoId = c.Int(nullable: false, identity: true),
                        Aluno_Id = c.Int(),
                        Turma_TurmaId = c.Int(),
                    })
                .PrimaryKey(t => t.OperacaoId)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Aluno_Id)
                .Index(t => t.Turma_TurmaId);
            
            CreateTable(
                "dbo.ResultadosProvas",
                c => new
                    {
                        ResultadoId = c.Int(nullable: false, identity: true),
                        Observacao = c.String(maxLength: 100, unicode: false),
                        Nota = c.Int(nullable: false),
                        Gabarito = c.String(maxLength: 100, unicode: false),
                        Aluno_Id = c.Int(),
                        Prova_ProvaId = c.Int(),
                    })
                .PrimaryKey(t => t.ResultadoId)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .ForeignKey("dbo.Prova", t => t.Prova_ProvaId)
                .Index(t => t.Aluno_Id)
                .Index(t => t.Prova_ProvaId);
            
            CreateTable(
                "dbo.TrabalhosExtras",
                c => new
                    {
                        TrabalhoId = c.Int(nullable: false, identity: true),
                        Nota = c.Int(nullable: false),
                        DataProposta = c.DateTime(nullable: false),
                        DataConclusao = c.DateTime(nullable: false),
                        Aluno_Id = c.Int(),
                        Professor_Id = c.Int(),
                        TurmaSelecionada_TurmaId = c.Int(),
                    })
                .PrimaryKey(t => t.TrabalhoId)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .ForeignKey("dbo.Professor", t => t.Professor_Id)
                .ForeignKey("dbo.Turma", t => t.TurmaSelecionada_TurmaId)
                .Index(t => t.Aluno_Id)
                .Index(t => t.Professor_Id)
                .Index(t => t.TurmaSelecionada_TurmaId);
            
            CreateTable(
                "dbo.ResponsavelAluno",
                c => new
                    {
                        Responsavel_Id = c.Int(nullable: false),
                        Aluno_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Responsavel_Id, t.Aluno_Id })
                .ForeignKey("dbo.Responsavel", t => t.Responsavel_Id)
                .ForeignKey("dbo.Aluno", t => t.Aluno_Id)
                .Index(t => t.Responsavel_Id)
                .Index(t => t.Aluno_Id);
            
            CreateTable(
                "dbo.ProfessorDisciplina",
                c => new
                    {
                        Professor_Id = c.Int(nullable: false),
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.Disciplina_DisciplinaId })
                .ForeignKey("dbo.Professor", t => t.Professor_Id)
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_DisciplinaId)
                .Index(t => t.Professor_Id)
                .Index(t => t.Disciplina_DisciplinaId);
            
            CreateTable(
                "dbo.ProfessorTurma",
                c => new
                    {
                        Professor_Id = c.Int(nullable: false),
                        Turma_TurmaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.Turma_TurmaId })
                .ForeignKey("dbo.Professor", t => t.Professor_Id)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Professor_Id)
                .Index(t => t.Turma_TurmaId);
            
            CreateTable(
                "dbo.DisciplinaTurma",
                c => new
                    {
                        Disciplina_DisciplinaId = c.Int(nullable: false),
                        Turma_TurmaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Disciplina_DisciplinaId, t.Turma_TurmaId })
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_DisciplinaId)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Disciplina_DisciplinaId)
                .Index(t => t.Turma_TurmaId);
            
            CreateTable(
                "dbo.Aluno",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Turma_TurmaId = c.Int(),
                        Observacoes = c.String(maxLength: 100, unicode: false),
                        StatusCadastro = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Id)
                .ForeignKey("dbo.Turma", t => t.Turma_TurmaId)
                .Index(t => t.Id)
                .Index(t => t.Turma_TurmaId);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Funcao = c.String(maxLength: 100, unicode: false),
                        PoderAdministrativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Professor",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Notificacao_NotificacaoId = c.Int(),
                        Especialidade = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionario", t => t.Id)
                .ForeignKey("dbo.Notificacao", t => t.Notificacao_NotificacaoId)
                .Index(t => t.Id)
                .Index(t => t.Notificacao_NotificacaoId);
            
            CreateTable(
                "dbo.Responsavel",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FuncaoTrabalhista = c.String(maxLength: 100, unicode: false),
                        Renda = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responsavel", "Id", "dbo.Usuario");
            DropForeignKey("dbo.Professor", "Notificacao_NotificacaoId", "dbo.Notificacao");
            DropForeignKey("dbo.Professor", "Id", "dbo.Funcionario");
            DropForeignKey("dbo.Funcionario", "Id", "dbo.Usuario");
            DropForeignKey("dbo.Aluno", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Aluno", "Id", "dbo.Usuario");
            DropForeignKey("dbo.TrabalhosExtras", "TurmaSelecionada_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.TrabalhosExtras", "Professor_Id", "dbo.Professor");
            DropForeignKey("dbo.TrabalhosExtras", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.ResultadosProvas", "Prova_ProvaId", "dbo.Prova");
            DropForeignKey("dbo.ResultadosProvas", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.Rematricula", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Rematricula", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.Prova", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Prova", "Professores_Id", "dbo.Professor");
            DropForeignKey("dbo.Prova", "Disciplina_DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.Notificacao", "UsuarioCriacao_Id", "dbo.Usuario");
            DropForeignKey("dbo.Notificacao", "TurmaPublicoAlvo_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.Frequencia", "Disciplina_DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.Frequencia", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.Evento", "FuncionarioResponsavel_Id", "dbo.Funcionario");
            DropForeignKey("dbo.Contrato", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.ConteudosExtras", "TurmaPublicoAlvo_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.ConteudosExtras", "Professor_Id", "dbo.Professor");
            DropForeignKey("dbo.Boleto", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.Livro", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.DisciplinaTurma", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.DisciplinaTurma", "Disciplina_DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.ProfessorTurma", "Turma_TurmaId", "dbo.Turma");
            DropForeignKey("dbo.ProfessorTurma", "Professor_Id", "dbo.Professor");
            DropForeignKey("dbo.Experiencia", "Funcionario_Id", "dbo.Funcionario");
            DropForeignKey("dbo.ProfessorDisciplina", "Disciplina_DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.ProfessorDisciplina", "Professor_Id", "dbo.Professor");
            DropForeignKey("dbo.Livro", "Disciplina_DisciplinaId", "dbo.Disciplina");
            DropForeignKey("dbo.Turma", "AnoLetivo_AnoLetivoId", "dbo.AnoLetivo");
            DropForeignKey("dbo.ResponsavelAluno", "Aluno_Id", "dbo.Aluno");
            DropForeignKey("dbo.ResponsavelAluno", "Responsavel_Id", "dbo.Responsavel");
            DropIndex("dbo.Responsavel", new[] { "Id" });
            DropIndex("dbo.Professor", new[] { "Notificacao_NotificacaoId" });
            DropIndex("dbo.Professor", new[] { "Id" });
            DropIndex("dbo.Funcionario", new[] { "Id" });
            DropIndex("dbo.Aluno", new[] { "Turma_TurmaId" });
            DropIndex("dbo.Aluno", new[] { "Id" });
            DropIndex("dbo.DisciplinaTurma", new[] { "Turma_TurmaId" });
            DropIndex("dbo.DisciplinaTurma", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.ProfessorTurma", new[] { "Turma_TurmaId" });
            DropIndex("dbo.ProfessorTurma", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorDisciplina", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.ProfessorDisciplina", new[] { "Professor_Id" });
            DropIndex("dbo.ResponsavelAluno", new[] { "Aluno_Id" });
            DropIndex("dbo.ResponsavelAluno", new[] { "Responsavel_Id" });
            DropIndex("dbo.TrabalhosExtras", new[] { "TurmaSelecionada_TurmaId" });
            DropIndex("dbo.TrabalhosExtras", new[] { "Professor_Id" });
            DropIndex("dbo.TrabalhosExtras", new[] { "Aluno_Id" });
            DropIndex("dbo.ResultadosProvas", new[] { "Prova_ProvaId" });
            DropIndex("dbo.ResultadosProvas", new[] { "Aluno_Id" });
            DropIndex("dbo.Rematricula", new[] { "Turma_TurmaId" });
            DropIndex("dbo.Rematricula", new[] { "Aluno_Id" });
            DropIndex("dbo.Prova", new[] { "Turma_TurmaId" });
            DropIndex("dbo.Prova", new[] { "Professores_Id" });
            DropIndex("dbo.Prova", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.Notificacao", new[] { "UsuarioCriacao_Id" });
            DropIndex("dbo.Notificacao", new[] { "TurmaPublicoAlvo_TurmaId" });
            DropIndex("dbo.Frequencia", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.Frequencia", new[] { "Aluno_Id" });
            DropIndex("dbo.Evento", new[] { "FuncionarioResponsavel_Id" });
            DropIndex("dbo.Contrato", new[] { "Aluno_Id" });
            DropIndex("dbo.ConteudosExtras", new[] { "TurmaPublicoAlvo_TurmaId" });
            DropIndex("dbo.ConteudosExtras", new[] { "Professor_Id" });
            DropIndex("dbo.Boleto", new[] { "Aluno_Id" });
            DropIndex("dbo.Experiencia", new[] { "Funcionario_Id" });
            DropIndex("dbo.Livro", new[] { "Turma_TurmaId" });
            DropIndex("dbo.Livro", new[] { "Disciplina_DisciplinaId" });
            DropIndex("dbo.Turma", new[] { "AnoLetivo_AnoLetivoId" });
            DropTable("dbo.Responsavel");
            DropTable("dbo.Professor");
            DropTable("dbo.Funcionario");
            DropTable("dbo.Aluno");
            DropTable("dbo.DisciplinaTurma");
            DropTable("dbo.ProfessorTurma");
            DropTable("dbo.ProfessorDisciplina");
            DropTable("dbo.ResponsavelAluno");
            DropTable("dbo.TrabalhosExtras");
            DropTable("dbo.ResultadosProvas");
            DropTable("dbo.Rematricula");
            DropTable("dbo.Prova");
            DropTable("dbo.Notificacao");
            DropTable("dbo.LojasRecomendadas");
            DropTable("dbo.Frequencia");
            DropTable("dbo.Evento");
            DropTable("dbo.Contrato");
            DropTable("dbo.ConteudosExtras");
            DropTable("dbo.FaleConosco");
            DropTable("dbo.Boleto");
            DropTable("dbo.Experiencia");
            DropTable("dbo.Livro");
            DropTable("dbo.Disciplina");
            DropTable("dbo.AnoLetivo");
            DropTable("dbo.Turma");
            DropTable("dbo.Usuario");
        }
    }
}
