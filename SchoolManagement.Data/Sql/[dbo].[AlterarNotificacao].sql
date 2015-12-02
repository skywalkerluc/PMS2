IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'AlterarNotificacao')
DROP PROCEDURE [AlterarNotificacao]
GO

CREATE PROCEDURE [dbo].[AlterarNotificacao]
	@NotificacaoId INT,
	@NotificacaoAssunto VARCHAR(100) = NULL,
	@NotificacaoDescricao VARCHAR(100) = NULL
AS
	UPDATE Notificacao
	SET Assunto = @NotificacaoAssunto,
		Descricao = @NotificacaoDescricao
	WHERE NotificacaoId = @NotificacaoId